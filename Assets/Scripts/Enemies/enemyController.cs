using System;
using System.Collections;
using System.Linq;
using Player;
using Player.Health;
using Projectiles;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public static class Helper
    {
        public static GameObject FindGameObjectInChildWithTag(this GameObject gameObject, string tag) // creates function to allow to search for a gameobject with a tag
        {
            var t = gameObject.transform;
            return (from Transform transform in t where transform.CompareTag(tag) select transform.gameObject)
                .FirstOrDefault();
        }
    }

    public class EnemyController : MonoBehaviour
    {
        private enum AttackType
        {
            Melee,
            Ranged
        }


        [Header("Enemy Health")] [SerializeField]
        private float maxHealth;

        [Header("Enemy Attack Settings")] [SerializeField]
        private int meleeAttackRange;

        [SerializeField] private int meleeDamage;
        [SerializeField] private int rangedAttackDamage;
        [SerializeField] private int rangedRange;
        [SerializeField] private int rangedAttackProjectileSpeed;
        [SerializeField] private int rangedAttackProjectileDespawnTime;
        [SerializeField] private int rangedAttackCooldown;
        [SerializeField] private float pauseTimeBeforeShoot;
        [SerializeField] private int meleeAttackCooldown;
        [SerializeField] private AttackType attackType;
        [SerializeField] private GameObject magicWeaponProj;

        [Header("Enemy AI Config")] [SerializeField]
        private int playerDetectionRadius;


        private float _currentHealth;
        private NavMeshAgent _navMeshAgent;
        private GameObject _player;
        private GameObject _enemyProjSpawnPos;
        private GameObject _spawnedProjectile;
        private PlayerHealth _playerHealth;
        private MagicProjectile _magicProjectile;
        private bool _isLookingAtPlayer;

        private bool _canAttack;

        // Start is called before the first frame update
        private void Start()
        {
            _canAttack = true;
            _currentHealth = maxHealth;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _enemyProjSpawnPos = gameObject.FindGameObjectInChildWithTag("enemyProjSpawn");
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < playerDetectionRadius)
                _navMeshAgent.SetDestination(_player.transform.position);


            switch (attackType)
            {
                case AttackType.Melee:
                    if (Vector3.Distance(transform.position, _player.transform.position) > meleeAttackRange) return;
                    AttackPlayerMelee();
                    break;
                case AttackType.Ranged:
                    if (Vector3.Distance(transform.position, _player.transform.position) > rangedRange) return;
                    if (Vector3.Distance(transform.position, _player.transform.position) >
                        playerDetectionRadius) return;
                    _isLookingAtPlayer = CheckIfLookingAtPlayer();
                    if (!_isLookingAtPlayer) return;
                    StartCoroutine(PauseBeforeShoot(pauseTimeBeforeShoot));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void TakeDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;
            if (_currentHealth <= 0) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void AttackPlayerMelee()
        {
            if (!_canAttack) return;
            _playerHealth.Damage(meleeDamage);
            StartCoroutine(AttackCooldown(meleeAttackCooldown));
        }

        private void AttackPlayerRanged()
        {
            if (!_canAttack) return;
            _spawnedProjectile =
                Instantiate(magicWeaponProj, _enemyProjSpawnPos.transform.position, transform.rotation);
            _magicProjectile = _spawnedProjectile.GetComponent<MagicProjectile>();
            _magicProjectile.Initialize(rangedAttackDamage, rangedAttackProjectileSpeed,
                rangedAttackProjectileDespawnTime, true);
            StartCoroutine(AttackCooldown(rangedAttackCooldown));
        }

        private IEnumerator AttackCooldown(int cooldownTime)
        {
            _canAttack = false;
            yield return new WaitForSeconds(cooldownTime);
            _canAttack = true;
        }

        private bool CheckIfLookingAtPlayer()
        {
            RaycastHit hit;
            if (!Physics.Linecast(transform.position, _player.transform.position, out hit)) return false;
            var hitObject = hit.transform.root.gameObject;
            if (hitObject != _player) return false;
            var relativePos = _player.transform.position - transform.position;
            var eulerAngles = Quaternion.LookRotation(relativePos.normalized).eulerAngles;
            eulerAngles.x = 0;
            transform.eulerAngles = eulerAngles;
            return true;
        }

        private IEnumerator PauseBeforeShoot(float pauseTime)
        {
            yield return new WaitForSeconds(pauseTime);
            AttackPlayerRanged();
        }
    }
}