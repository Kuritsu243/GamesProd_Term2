using System;
using System.Collections;
using System.Linq;
using Player;
using Player.Health;
using Player.Input;
using PostProcessing;
using Projectiles;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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

        private enum EnemyType
        {
            Heavy,
            Tall,
            TVHead
        }


        private enum DebuffType
        {
            Blindness,
            JumpHeight,
            MovementSpeed
        }
        [Header("Enemy Types")] [SerializeField]
        private EnemyType enemyType;
        
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
        [SerializeField] private float meleeAttackCooldown;
        [SerializeField] private AttackType attackType;
        [SerializeField] private GameObject magicWeaponProj;
        [SerializeField] private GameObject enemyProjSpawnPos;
        [SerializeField] private float tvHeadDebuffLength = 5f;
        [SerializeField] private float tvHeadDebuffCooldown = 8f;
        
        

        [Header("Enemy AI Config")] [SerializeField]
        private int playerDetectionRadius;


        private float _currentHealth;
        private bool _canMove;
        private NavMeshAgent _navMeshAgent;
        private GameObject _player;
        private HeavyEnemyHand _enemyHandScript;
        private GameObject _spawnedProjectile;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private MagicProjectile _magicProjectile;
        private Animator _enemyAnimator;
        private bool _isLookingAtPlayer;
        private Vector3 _previousPos;
        private float _velocity;
        private float _attackAnimLength;
        private AnimationClip[] animClips;
        private PostProcessingController _postProcessingController;
        private DebuffType _debuffType;
        private float _playerDefaultSpeed;
        private float _playerDefaultJumpHeight;
        private bool _canDebuff;
        private bool _canAttack;

        // Start is called before the first frame update
        private void Start()
        {
            _canAttack = true;
            _canDebuff = true;
            _currentHealth = maxHealth;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _postProcessingController = GameObject.FindGameObjectWithTag("postProcessingController")
                .GetComponent<PostProcessingController>();
            enemyProjSpawnPos = gameObject.FindGameObjectInChildWithTag("enemyProjSpawn");

            _playerDefaultSpeed = _playerMovement.MoveSpeed;
            _playerDefaultJumpHeight = _playerMovement.JumpHeight;
            _enemyAnimator = GetComponent<Animator>();

            animClips = _enemyAnimator.runtimeAnimatorController.animationClips;

            switch (enemyType)
            {
                case EnemyType.Heavy:
                    _enemyHandScript = GetComponentInChildren<HeavyEnemyHand>();
                    _enemyHandScript.DamageAmount = meleeDamage;
                    foreach (var animClip in animClips)
                        if (animClip.name == "HeavyHit")
                            _attackAnimLength = animClip.length;
                    _enemyAnimator.Play("HeavyIdle");
                    break;
                case EnemyType.Tall:
                    _enemyAnimator.Play("Tall_Idle");
                    break;
                case EnemyType.TVHead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            meleeAttackCooldown = _attackAnimLength - _attackAnimLength / 5;

        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < playerDetectionRadius && _canMove)
                _navMeshAgent.SetDestination(_player.transform.position);

            switch (_velocity)
            {
                case > 0.01f when _canMove && enemyType == EnemyType.Heavy:
                    _enemyAnimator.Play("HeavyRun");
                    break;
                case < 0.01f when _canMove && enemyType == EnemyType.Heavy:
                    _enemyAnimator.Play("HeavyIdle");
                    break;
                case < 0.01f when _canMove && enemyType == EnemyType.Tall:
                    _enemyAnimator.Play("Tall_Idle");
                    break;
            }


            switch (enemyType)
            {
                case EnemyType.Heavy:
                    if (Vector3.Distance(transform.position, _player.transform.position) > meleeAttackRange) return;
                    StartCoroutine(AttackPlayerMelee());
                    break;
                case EnemyType.Tall:
                    if (Vector3.Distance(transform.position, _player.transform.position) > rangedRange) return;
                    if (Vector3.Distance(transform.position, _player.transform.position) > playerDetectionRadius) return;
                    _isLookingAtPlayer = CheckIfLookingAtPlayer();
                    if (!_isLookingAtPlayer) return;
                    StartCoroutine(PauseBeforeShoot(pauseTimeBeforeShoot));
                    break;
                case EnemyType.TVHead:
                    if (!_canDebuff) return;
                    if (Vector3.Distance(transform.position, _player.transform.position) > playerDetectionRadius) return;
                    StartCoroutine(DebuffPlayer());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void LateUpdate()
        {
            _velocity = (transform.position - _previousPos).magnitude / Time.deltaTime;
            _previousPos = transform.position;
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

        private IEnumerator AttackPlayerMelee()
        {
            if (!_canAttack) yield break;
            _canMove = false;
            _enemyAnimator.Play("HeavyHit");
            StartCoroutine(AttackCooldown(meleeAttackCooldown));
            yield return new WaitForSeconds(0.15f);
            _enemyHandScript.CanHit = true;
            yield return new WaitForSeconds(_attackAnimLength);
            _enemyHandScript.CanHit = false;


        }

        private void AttackPlayerRanged()
        {
            if (!_canAttack) return;
            _spawnedProjectile =
                Instantiate(magicWeaponProj, enemyProjSpawnPos.transform.position, transform.rotation);
            _magicProjectile = _spawnedProjectile.GetComponent<MagicProjectile>();
            _magicProjectile.Initialize(rangedAttackDamage, rangedAttackProjectileSpeed,
                rangedAttackProjectileDespawnTime, true);
            StartCoroutine(AttackCooldown(rangedAttackCooldown));
        }

        private IEnumerator DebuffPlayer()
        {
            if (!_canDebuff) yield break;
            var chosenBuff = (DebuffType) Random.Range(0, Enum.GetValues(typeof(DebuffType)).Length);
            switch (chosenBuff)
            {
                case DebuffType.Blindness:
                    _postProcessingController.EnableBlindness();
                    yield return new WaitForSeconds(tvHeadDebuffLength);
                    _postProcessingController.DisableBlindness();
                    break;
                case DebuffType.JumpHeight:
                    _playerMovement.JumpHeight /= 2;
                    yield return new WaitForSeconds(tvHeadDebuffLength);
                    _playerMovement.JumpHeight = _playerDefaultJumpHeight;
                    break;
                case DebuffType.MovementSpeed:
                    _playerMovement.MoveSpeed /= 2;
                    yield return new WaitForSeconds(tvHeadDebuffLength);
                    _playerMovement.MoveSpeed = _playerDefaultSpeed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            StartCoroutine(DebuffCooldown());
        }

        private IEnumerator DebuffCooldown()
        {
            _canDebuff = false;
            yield return new WaitForSeconds(tvHeadDebuffCooldown);
            _canDebuff = true;
        }

        private IEnumerator AttackCooldown(float cooldownTime)
        {
            _canAttack = false;
            yield return new WaitForSeconds(cooldownTime);
            _canAttack = true;
            _canMove = true;
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

        private void OnDestroy()
        {
            if (enemyType != EnemyType.TVHead) return;
            if (_canDebuff)
                _canDebuff = false;

            _playerMovement.MoveSpeed = _playerDefaultSpeed;
            _playerMovement.JumpHeight = _playerDefaultJumpHeight;
            _postProcessingController.DisableBlindness();
            
        }

        private IEnumerator PauseBeforeShoot(float pauseTime)
        {
            yield return new WaitForSeconds(pauseTime);
            AttackPlayerRanged();
        }
    }
}