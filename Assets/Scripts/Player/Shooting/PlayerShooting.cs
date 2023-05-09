using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using Enemies;
using Player.Inventory;
using Player.Buff;
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
        [SerializeField] private Animator rocketAnimator;
        [Header("Shotgun Specific Settings")] 
        [SerializeField] private GameObject pelletProjectile;
        [SerializeField] private int shotgunPelletCount;
        [SerializeField] private Vector3 shotgunSpreadSizeVector3;
        [SerializeField] private int shotgunSpreadSizeInt;
        [SerializeField] private int shotgunPelletSpeed;
        [SerializeField] private int shotgunPelletDespawnTime;
        [SerializeField] private Animator shotgunAnimator;
        [Header("Raycast Settings")] 
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private int maxInteractDistance;
        [SerializeField] private GameObject emptyObj;
        [Header("Trail Settings")] 
        [SerializeField] private TrailRenderer bulletTrail;
        [Header("Projectile + Trail Spawn Points")] 
        [SerializeField] private GameObject leftDualWieldSpawnPos;
        [SerializeField] private GameObject rightDualWieldSpawnPos;
        [SerializeField] private GameObject rocketSpawnPos;
        [SerializeField] private GameObject shotgunSpawnPos;
        [SerializeField] private GameObject pistolSpawnPos;
        [Header("Firing Cooldown Lengths")] 
        [SerializeField] private float dualWieldCooldown;
        [SerializeField] private float pistolCooldown;
        [SerializeField] private float magicCooldown;
        [SerializeField] private float shotgunCooldown;
        [SerializeField] private float rocketCooldown;



        public int CurrentAmmo { get; set; }
        public bool IsDamageBuffActive { get; set; }
        public bool IsJumpBuffActive { get; set; }
   
        private PlayerInventory _playerInventory;
        private PlayerBuff _playerBuff;
        private Camera _playerCamera;
        private GameObject _projectileSpawnPoint;
        private GameObject _traceSpawnPoint;
        private GameObject _spawnedProjectile;
        private MagicProjectile _magicProjectile;
        private LineRenderer _weaponTrace;
        private bool _traceAtLeftDualWield = true;
        private bool _traceAtRightDualWield;
        private bool _canFire;
        private float buffRemaining;
        private AnimationClip[] _shotgunAnimClips;
        private AnimationClip[] _rocketAnimClips;
        
        // Start is called before the first frame update
        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerBuff = GetComponent<PlayerBuff>();
            _playerCamera = GetComponentInChildren<Camera>();
            _projectileSpawnPoint = GameObject.FindGameObjectWithTag("projSpawnPoint");
            CurrentAmmo = 50;

            _shotgunAnimClips = shotgunAnimator.runtimeAnimatorController.animationClips;
            _rocketAnimClips = rocketAnimator.runtimeAnimatorController.animationClips;

            foreach (var shotgunAnimClip in _shotgunAnimClips)
                if (shotgunAnimClip.name == "ShotgunShootAnim")
                    shotgunCooldown = shotgunAnimClip.length - shotgunAnimClip.length / 6;
            
            foreach (var rocketAnimClip in _rocketAnimClips)
                if (rocketAnimClip.name == "RocketShootAnim")
                    rocketCooldown = rocketAnimClip.length - rocketAnimClip.length / 5;

            _canFire = true;
        }

        // Update is called once per frame
        public void Fire()
        {
            if (!_canFire) return;
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
                    if (_playerBuff.IsDamageBuffActive)
                        collidedEnemy.GetComponent<EnemyController>().TakeDamage(CalculateBuffedDamage(dualWieldDamage));
                    else
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
            var rayOrigin = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;
            TrailRenderer trail = Instantiate(bulletTrail, pistolSpawnPos.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            switch (hit.transform.tag)
            {
                case "Enemy":
                    var collidedEnemy = hit.transform.gameObject;
                    collidedEnemy.GetComponent<EnemyController>().TakeDamage(pistolDamage);

                    break;
            }

            StartCoroutine(WeaponFiringCooldown(pistolCooldown));
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        }

        private void ShotgunFire()
        {
            var pellets = new List<Quaternion>(shotgunPelletCount);
            for (var i = 0; i < shotgunPelletCount; i++) pellets.Add(Quaternion.Euler(Vector3.zero));
            for (var h = 0; h < shotgunPelletCount; h++)
            {
                pellets[h] = Random.rotation;
                var pellet = Instantiate(pelletProjectile, shotgunSpawnPos.transform.position, shotgunSpawnPos.transform.rotation);
                pellet.transform.rotation = Quaternion.RotateTowards(pellet.transform.rotation, pellets[h], shotgunSpreadSizeInt);
                var pelletScript = pellet.GetComponent<ShotgunProjectile>();
                if (_playerBuff.IsDamageBuffActive)
                    pelletScript.Initialize(CalculateBuffedDamage(shotgunDamage), shotgunPelletSpeed, shotgunPelletDespawnTime, shotgunSpawnPos.transform.forward);
                else
                    pelletScript.Initialize(shotgunDamage, shotgunPelletSpeed, shotgunPelletDespawnTime, shotgunSpawnPos.transform.forward);
                
                var pelletRigidbody = pellet.GetComponent<Rigidbody>();
                // pelletRigidbody.velocity = shotgunSpawnPos.transform.forward * shotgunPelletSpeed;
            }
            
            shotgunAnimator.Play("ShotgunShootAnim");
            StartCoroutine(WeaponFiringCooldown(shotgunCooldown));
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);

        }

        private Vector3 GetDirection()
        {
            var direction = transform.forward;
            direction = Random.insideUnitCircle * shotgunSpreadSizeInt;
            direction = new Vector3(Mathf.Clamp(direction.x, 0, shotgunSpreadSizeInt / 100),
                Mathf.Clamp(direction.y, 0, shotgunSpreadSizeInt / 100), 0f);
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
            if (_playerBuff.IsDamageBuffActive)
                rocketScript.Initialize(CalculateBuffedDamage(rocketDamage), rocketProjectileSpeed, rocketDespawnTime, rocketExplosionRadius); // passthrough values
            else
                rocketScript.Initialize(rocketDamage, rocketProjectileSpeed, rocketDespawnTime, rocketExplosionRadius); // passthrough values
            rocketRigidbody.AddForce(direction.normalized * rocketProjectileSpeed, ForceMode.Impulse); // add the force
            
            rocketAnimator.Play("RocketShootAnim");
            StartCoroutine(WeaponFiringCooldown(rocketCooldown));
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

        private static IEnumerator SpawnTrailAlternative(TrailRenderer trail, Vector3 hit, GameObject objToDestroy)
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
            Destroy(objToDestroy);
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



        private float CalculateBuffedDamage(int originalDamageAmount)
        {
            return originalDamageAmount * _playerBuff.DamageBuffModifier;
        }
        





    }
}
