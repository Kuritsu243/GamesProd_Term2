using System.Collections;
using Enemies;
using Player.Inventory;
using Projectiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Shooting
{

    public class PlayerShooting : MonoBehaviour
    {
        [Header("Ammo Configs")] [SerializeField]
        private int maxDualWieldAmmo;

        [SerializeField] private int maxPistolAmmo;
        [SerializeField] private int maxShotgunAmmo;
        [SerializeField] private int maxMagicAmmo;

        [Header("Damage Configs")] [SerializeField]
        private int dualWieldDamage;

        [SerializeField] private int pistolDamage;
        [SerializeField] private int shotgunDamage;
        [SerializeField] private int magicDamage;

        [Header("Magic Specific Settings")] [SerializeField]
        private GameObject magicProjectile;

        [SerializeField] private int magicProjectileSpeed;
        [SerializeField] private int magicDespawnTime;

        [Header("Raycast Settings")] [SerializeField]
        private LayerMask layerMask;

        [SerializeField] private int maxInteractDistance;

        [Header("Trail Settings")] [SerializeField]
        private TrailRenderer bulletTrail;

        [Header("Projectile + Trail Spawn Points")] [SerializeField]
        private GameObject leftDualWieldSpawnPos;

        [SerializeField] private GameObject rightDualWieldSpawnPos;

        [Header("Firing Cooldown Lengths")] [SerializeField]
        private float dualWieldCooldown;

        [SerializeField] private float pistolCooldown;
        [SerializeField] private float magicCooldown;
        [SerializeField] private float shotgunCooldown;

        public int CurrentAmmo { get; set; }
        private PlayerInventory _playerInventory;
        private Camera _playerCamera;
        private GameObject _projectileSpawnPoint;
        private GameObject _traceSpawnPoint;
        private GameObject _spawnedProjectile;
        private MagicProjectile _magicProjectile;
        private LineRenderer _weaponTrace;
        private bool _traceAtLeftDualWield = true;
        private bool _traceAtRightDualWield;
        private bool _canFire;

        // Start is called before the first frame update
        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerCamera = GetComponentInChildren<Camera>();
            _projectileSpawnPoint = GameObject.FindGameObjectWithTag("projSpawnPoint");
            CurrentAmmo = 10;
        }

        // Update is called once per frame
        public void Fire()
        {
            if (CurrentAmmo <= 0) return;
            if (_playerInventory.CurrentCards.Count < 1) return;
            switch (_playerInventory.IsInWeaponMode)
            {
                case false:
                    return;
                case true when _playerInventory.CurrentCard.weaponType == cardObject.WeaponType.Pistol:
                    PistolFire();
                    break;
                case true when _playerInventory.CurrentCard.weaponType == cardObject.WeaponType.DualWield:
                    DualWieldFire();
                    break;
                case true when _playerInventory.CurrentCard.weaponType == cardObject.WeaponType.Shotgun:
                    ShotgunFire();
                    break;
                case true when _playerInventory.CurrentCard.weaponType == cardObject.WeaponType.Magic:
                    MagicFire();
                    break;
            }
        }

        private void DualWieldFire()
        {
            var rayOrigin = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;
            TrailRenderer trail = Instantiate(bulletTrail, AlternativeDualWieldTrails(), Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            switch (hit.transform.tag)
            {
                case "Enemy":
                    var collidedEnemy = hit.transform.gameObject;
                    collidedEnemy.GetComponent<EnemyController>().TakeDamage(dualWieldDamage);

                    break;
            }

            StartCoroutine(WeaponFiringCooldown(dualWieldCooldown));
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        }

        private void PistolFire()
        {

        }

        private void ShotgunFire()
        {

        }

        private void MagicFire()
        {
            _spawnedProjectile =
                Instantiate(magicProjectile, _projectileSpawnPoint.transform.position, transform.rotation);
            _magicProjectile = _spawnedProjectile.GetComponent<MagicProjectile>();
            _magicProjectile.Initialize(magicDamage, magicProjectileSpeed, magicDespawnTime, false);
            StartCoroutine(WeaponFiringCooldown(magicCooldown));
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        }

        private static IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
        {
            float time = 0;
            var startPos = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }

            trail.transform.position = hit.point;
            Destroy(trail.gameObject, trail.time);
        }

        private Vector3 AlternativeDualWieldTrails()
        {
            var posToReturn = Vector3.zero;
            switch (_traceAtLeftDualWield)
            {
                case true when !_traceAtRightDualWield:
                    _traceAtLeftDualWield = false;
                    _traceAtRightDualWield = true;
                    posToReturn = leftDualWieldSpawnPos.transform.position;
                    break;
                case false when _traceAtRightDualWield:
                    _traceAtLeftDualWield = true;
                    _traceAtRightDualWield = false;
                    posToReturn = rightDualWieldSpawnPos.transform.position;
                    break;
            }

            return posToReturn;
        }

        private IEnumerator WeaponFiringCooldown(float cooldownLength)
        {
            _canFire = false;
            yield return new WaitForSeconds(cooldownLength);
            _canFire = true;
        }



    }
}
