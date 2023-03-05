using System.Collections;
using System.Collections.Generic;
using Cards;
using Enemies;
using Player.Inventory;
using Projectiles;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Player.Shooting
{

    public class PlayerShooting : MonoBehaviour
    {
        [Header("Ammo Configs")] 
        [SerializeField] private int maxDualWieldAmmo;
        [SerializeField] private int maxPistolAmmo;
        [SerializeField] private int maxShotgunAmmo;
        [SerializeField] private int maxMagicAmmo;
        [SerializeField] private int maxRocketAmmo;
        [Header("Damage Configs")] 
        [SerializeField] private int dualWieldDamage;
        [SerializeField] private int pistolDamage;
        [SerializeField] private int shotgunDamage;
        [SerializeField] private int magicDamage;
        [SerializeField] private int rocketDamage;
        [Header("Magic Specific Settings")] 
        [SerializeField] private GameObject magicProjectile;
        [SerializeField] private int magicProjectileSpeed;
        [SerializeField] private int magicDespawnTime;
        [Header("Rocket Specific Settings")]
        [SerializeField] private GameObject rocketProjectile;
        [SerializeField] private int rocketProjectileSpeed;
        [SerializeField] private int rocketDespawnTime;
        [SerializeField] private int rocketExplosionRadius;
        [Header("Shotgun Specific Settings")] 
        [SerializeField] private GameObject pelletProjectile;
        [SerializeField] private int shotgunPelletCount;
        [SerializeField] private Vector3 shotgunSpreadSizeVector3;
        [SerializeField] private int shotgunSpreadSizeInt;
        [SerializeField] private int shotgunPelletSpeed;
        [SerializeField] private int shotgunPelletDespawnTime;
        [Header("Raycast Settings")] 
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private int maxInteractDistance;
        [Header("Trail Settings")] 
        [SerializeField] private TrailRenderer bulletTrail;
        [Header("Projectile + Trail Spawn Points")] 
        [SerializeField] private GameObject leftDualWieldSpawnPos;
        [SerializeField] private GameObject rightDualWieldSpawnPos;
        [SerializeField] private GameObject rocketSpawnPos;
        [SerializeField] private GameObject shotgunSpawnPos;
        [Header("Firing Cooldown Lengths")] 
        [SerializeField] private float dualWieldCooldown;
        [SerializeField] private float pistolCooldown;
        [SerializeField] private float magicCooldown;
        [SerializeField] private float shotgunCooldown;
        [SerializeField] private float rocketCooldown;

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
            CurrentAmmo = 50;
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
                case true when _playerInventory.CurrentCard.weaponType == cardObject.WeaponType.RocketLauncher:
                    RocketFire();
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
            // for (var i = 0; i < shotgunPelletCount; i++)
            // {
            //     var accuracy = (Random.insideUnitCircle * shotgunSpreadSizeInt);
            //     var ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue() + accuracy);
            //     if (Physics.Raycast(ray, out var hit, float.MaxValue))
            //     {
            //         var trail = Instantiate(bulletTrail, shotgunSpawnPos.transform.position, Quaternion.identity);
            //         StartCoroutine(SpawnTrail(trail, hit));
            //         switch (hit.transform.tag)
            //         {
            //             case "Enemy":
            //                 var enemyScript = hit.transform.gameObject.GetComponent<EnemyController>();
            //                 enemyScript.TakeDamage(shotgunDamage);
            //                 break;
            //             case null:
            //                 break;
            //         }
            //     }
            //     else
            //     {
            //         var trail = Instantiate(bulletTrail, shotgunSpawnPos.transform.position, Quaternion.identity);
            //         StartCoroutine(SpawnTrailAlternative(trail, transform.position + GetDirection() * 100, Vector3.zero));
            //     }
            //     
                
                
                
                
                
                // var accuracy = Random.insideUnitCircle * shotgunSpreadSize;
                // var dir = new Vector3(accuracy.x, accuracy.y, 1f);
                // var sprayDir = transform.TransformVector(dir);
                // Debug.ClearDeveloperConsole();
                // Debug.Log(accuracy);
                // var ray = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue() + accuracy);
                // var targetPoint = Physics.Raycast(ray, out var hit)
                //     ? hit.point
                //     : shotgunSpawnPos.transform.position + GetDirection(accuracy) * 100f;
                // TrailRenderer trail = Instantiate(bulletTrail, shotgunSpawnPos.transform.position, Quaternion.identity);
                // StartCoroutine(SpawnTrailAlternative(trail, targetPoint));
                // switch (hit.transform.tag)
                // {
                //     case "Enemy":
                //         var collidedEnemy = hit.transform.gameObject;
                //         collidedEnemy.GetComponent<EnemyController>().TakeDamage(shotgunDamage);
                //         break;
                //     case null:
                //         break;
                // }
            // }
            // CurrentAmmo--;
            // if (CurrentAmmo > 0) return;
            // _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
            // if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;



            var pellets = new List<Quaternion>(shotgunPelletCount);
            for (var i = 0; i < shotgunPelletCount; i++) pellets.Add(Quaternion.Euler(Vector3.zero));
            for (var h = 0; h < shotgunPelletCount; h++)
            {
                pellets[h] = Random.rotation;
                var pellet = Instantiate(pelletProjectile, shotgunSpawnPos.transform.position, Quaternion.identity);
                pellet.transform.rotation = Quaternion.RotateTowards(pellet.transform.rotation, pellets[h], shotgunSpreadSizeInt);
                var pelletScript = pellet.GetComponent<ShotgunProjectile>();
                pelletScript.Initialize(shotgunDamage, shotgunPelletSpeed, shotgunPelletDespawnTime);
            }
            
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);

        }

        private Vector3 GetDirection()
        {
            var direction = transform.forward;
            direction = Random.insideUnitCircle * shotgunSpreadSizeInt;
            direction = new Vector3(Mathf.Clamp(direction.x, 0, shotgunSpreadSizeInt / 100), Mathf.Clamp(direction.y, 0, shotgunSpreadSizeInt / 100), 0f);
            direction.Normalize();
            return direction;
        }

        private void RocketFire()
        {
            var rayOrigin = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // get center of screen
            var targetPoint = Physics.Raycast(rayOrigin, out var hit) ? hit.point : rayOrigin.GetPoint(75); // raycast
            var direction = targetPoint - rocketSpawnPos.transform.position; // calc direction
            _spawnedProjectile = Instantiate(rocketProjectile, rocketSpawnPos.transform.position, Quaternion.identity); // spawn rocket
            _spawnedProjectile.transform.forward = direction.normalized; // set forward to the normalized direction
            var rocketRigidbody = _spawnedProjectile.GetComponent<Rigidbody>(); // get rb
            var rocketScript = _spawnedProjectile.GetComponent<RocketProjectile>(); // get script
            rocketScript.Initialize(rocketDamage, rocketProjectileSpeed, rocketDespawnTime, rocketExplosionRadius); // passthrough values
            rocketRigidbody.AddForce(direction.normalized * rocketProjectileSpeed, ForceMode.Impulse); // add the force
            CurrentAmmo--; // deplete ammo
            if (CurrentAmmo > 0) return; // if less than 0 ammo
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard); // expire card
            
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

        private static IEnumerator SpawnTrailAlternative(TrailRenderer trail, Vector3 hit, Vector3 hitNormal)
        {
            float time = 0;
            var startPos = trail.transform.position;
            while (time < 1)
            {
                trail.transform.position = Vector3.Lerp(startPos, hit, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }

            trail.transform.position = hit;
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
