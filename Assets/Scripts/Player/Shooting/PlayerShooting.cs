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
        [SerializeField] private Animator magicAnimator;
        [SerializeField] private AudioClip magicAudio;
        
        [Header("Rocket Specific Settings")]
        [SerializeField] private GameObject rocketProjectile;
        [SerializeField] private int rocketProjectileSpeed;
        [SerializeField] private int rocketDespawnTime;
        [SerializeField] private int rocketExplosionRadius;
        [SerializeField] private Animator rocketAnimator;
        [SerializeField] private AudioClip rocketAudio;
        
        [Header("Shotgun Specific Settings")] 
        [SerializeField] private GameObject pelletProjectile;
        [SerializeField] private int shotgunPelletCount;
        [SerializeField] private Vector3 shotgunSpreadSizeVector3;
        [SerializeField] private int shotgunSpreadSizeInt;
        [SerializeField] private int shotgunPelletSpeed;
        [SerializeField] private int shotgunPelletDespawnTime;
        [SerializeField] private Animator shotgunAnimator;
        [SerializeField] private AudioClip shotgunAudio;
        
        [Header("Dual Wield Specific Settings")] 
        [SerializeField] private Animator dualLAnimator;
        [SerializeField] private Animator dualRAnimator;
        [SerializeField] private AudioClip dualWieldAudio;
        
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
        [SerializeField] private GameObject magicSpawnPos;
        [SerializeField] private float trailSpeed;
        [SerializeField] private Vector3 trailSpreadVariance;
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
        private AnimationClip[] _dualLAnimClips;
        private AnimationClip[] _dualRAnimClips;
        private AnimationClip[] _magicAnimClips;
        private AnimationClip _leftAnimClip;
        private AnimationClip _rightAnimClip;
        private AudioSource _audioSource;
        
        // Start is called before the first frame update
        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
            _playerBuff = GetComponent<PlayerBuff>();
            _playerCamera = GetComponentInChildren<Camera>();
            _audioSource = GetComponent<AudioSource>();
            _projectileSpawnPoint = GameObject.FindGameObjectWithTag("projSpawnPoint");
            CurrentAmmo = 50;

            _shotgunAnimClips = shotgunAnimator.runtimeAnimatorController.animationClips;
            _rocketAnimClips = rocketAnimator.runtimeAnimatorController.animationClips;
            _dualLAnimClips = dualLAnimator.runtimeAnimatorController.animationClips;
            _dualRAnimClips = dualRAnimator.runtimeAnimatorController.animationClips;
            _magicAnimClips = magicAnimator.runtimeAnimatorController.animationClips;

            foreach (var shotgunAnimClip in _shotgunAnimClips)
                if (shotgunAnimClip.name == "ShotgunShootAnim")
                    shotgunCooldown = shotgunAnimClip.length - shotgunAnimClip.length / 6;
            
            foreach (var rocketAnimClip in _rocketAnimClips)
                if (rocketAnimClip.name == "RocketShootAnim")
                    rocketCooldown = rocketAnimClip.length - rocketAnimClip.length / 5;

            foreach (var dualAnimClip in _dualLAnimClips)
            {
                if (dualAnimClip.name != "RevolverLAnim") continue;
                dualWieldCooldown = dualAnimClip.length - dualAnimClip.length / 4;
                _leftAnimClip = dualAnimClip;
            }

            foreach (var dualAnimClip in _dualRAnimClips)
            {
                if (dualAnimClip.name != "RevolverRAnim") continue;
                _rightAnimClip = dualAnimClip;
            }

            foreach (var magicAnimClip in _magicAnimClips)
            {
                if (magicAnimClip.name == "WandCastAnim")
                    magicCooldown = magicAnimClip.length - magicAnimClip.length / 4;
            }

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
                    // PistolFire();
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

        // src: https://github.com/llamacademy/raycast-bullet-trails/blob/main/Assets/Scripts/Gun.cs
        private void DualWieldFire()
        {
            // var rayOrigin = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            // if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;
            // TrailRenderer trail = Instantiate(bulletTrail, AlternativeDualWieldTrails(), Quaternion.identity);
            // StartCoroutine(SpawnTrail(trail, hit));
            var animClipIndicator = _traceAtLeftDualWield;
            var spawnPos = AlternativeDualWieldTrails();
            var direction = GetTrailSpread(spawnPos);

            if (Physics.Raycast(spawnPos.position, direction, out RaycastHit hit, 25f))
            {
                var trail = Instantiate(bulletTrail, spawnPos.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
                
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
            }
            else
            {
                var trail = Instantiate(bulletTrail, spawnPos.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, spawnPos.position + direction * 25, Vector3.zero, false));
            }


            switch (animClipIndicator)
            {
                case true:
                    dualLAnimator.Play(_leftAnimClip.name);
                    break;
                case false:
                    dualRAnimator.Play(_rightAnimClip.name);
                    break;
            }
            StartCoroutine(WeaponFiringCooldown(dualWieldCooldown));
            _audioSource.PlayOneShot(dualWieldAudio);
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        }

        // private void PistolFire()
        // {
        //     var rayOrigin = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //     if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;
        //     TrailRenderer trail = Instantiate(bulletTrail, pistolSpawnPos.transform.position, Quaternion.identity);
        //     // StartCoroutine(SpawnTrail(trail, hit));
        //     switch (hit.transform.tag)
        //     {
        //         case "Enemy":
        //             var collidedEnemy = hit.transform.gameObject;
        //             collidedEnemy.GetComponent<EnemyController>().TakeDamage(pistolDamage);
        //
        //             break;
        //     }
        //
        //     StartCoroutine(WeaponFiringCooldown(pistolCooldown));
        //     CurrentAmmo--;
        //     if (CurrentAmmo > 0) return;
        //     _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        // }

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
            _audioSource.PlayOneShot(shotgunAudio);
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
            var targetPoint = Physics.Raycast(rayOrigin, out var hit) && !hit.transform.CompareTag("Player")
                ? hit.point
                : rocketSpawnPos.transform.position + rocketSpawnPos.transform.forward * 25; // raycast
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
            _audioSource.PlayOneShot(rocketAudio);
            StartCoroutine(WeaponFiringCooldown(rocketCooldown));
            CurrentAmmo--; // deplete ammo
            if (CurrentAmmo > 0) return; // if less than 0 ammo
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard); // expire card
            
        }

        private void MagicFire()
        {
            _spawnedProjectile =
                Instantiate(magicProjectile, magicSpawnPos.transform.position, transform.rotation);
            _magicProjectile = _spawnedProjectile.GetComponent<MagicProjectile>();
            _magicProjectile.Initialize(magicDamage, magicProjectileSpeed, magicDespawnTime, false);
            magicAnimator.Play("WandCastAnim");
            StartCoroutine(WeaponFiringCooldown(magicCooldown));
            _audioSource.PlayOneShot(magicAudio);
            CurrentAmmo--;
            if (CurrentAmmo > 0) return;
            _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
        }

        private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 point, Vector3 pointNormal, bool madeImpact)
        {
            var startPos = trail.transform.position;
            var distance = Vector3.Distance(trail.transform.position, point);
            var remainingDistance = distance;

            while (remainingDistance > 0)
            {
                trail.transform.position = Vector3.Lerp(startPos, point, 1 - (remainingDistance / distance));
                remainingDistance -= trailSpeed * Time.deltaTime;
                yield return null;
            }

            trail.transform.position = point;
            Destroy(trail.gameObject, trail.time);
            // test
            // float time = 0;
            // var startPos = trail.transform.position;
            //
            // while (time < 1)
            // {
            //     trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            //     time += Time.deltaTime / trail.time;
            //     yield return null;
            // }
            //
            // trail.transform.position = hit.point;
            // Destroy(trail.gameObject, trail.time);
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

        private Transform AlternativeDualWieldTrails()
        {
            Transform posToReturn = null;
            switch (_traceAtLeftDualWield)
            {
                case true when !_traceAtRightDualWield:
                    _traceAtLeftDualWield = false;
                    _traceAtRightDualWield = true;
                    posToReturn = leftDualWieldSpawnPos.transform;
                    break;
                case false when _traceAtRightDualWield:
                    _traceAtLeftDualWield = true;
                    _traceAtRightDualWield = false;
                    posToReturn = rightDualWieldSpawnPos.transform;
                    break;
            }
            return posToReturn;
        }

        private AnimationClip GetDualWieldClip()
        {
            AnimationClip clipToReturn = null;
            return _traceAtLeftDualWield switch
            {
                true => _leftAnimClip,
                false => _rightAnimClip
            };
        }

        private IEnumerator WeaponFiringCooldown(float cooldownLength)
        {
            _canFire = false;
            yield return new WaitForSeconds(cooldownLength);
            _canFire = true;
        }

        private Vector3 GetTrailSpread(Transform weaponSpawnPos)
        {
            Vector3 direction = weaponSpawnPos.forward;

            direction += new Vector3(
                Random.Range(-trailSpreadVariance.x, trailSpreadVariance.x),
                Random.Range(-trailSpreadVariance.y, trailSpreadVariance.y),
                Random.Range(-trailSpreadVariance.z, trailSpreadVariance.z)
            );

            return direction;
        }

        private float CalculateBuffedDamage(int originalDamageAmount)
        {
            return originalDamageAmount * _playerBuff.DamageBuffModifier;
        }
        





    }
}
