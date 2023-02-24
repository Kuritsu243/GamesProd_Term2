using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Enemies;

public class PlayerShooting : MonoBehaviour
{
    [Header("Ammo Configs")]
    [SerializeField] private int maxDualWieldAmmo;
    [SerializeField] private int maxPistolAmmo;
    [SerializeField] private int maxShotgunAmmo;
    [SerializeField] private int maxMagicAmmo;
    [Header("Damage Configs")]
    [SerializeField] private int dualWieldDamage;
    [SerializeField] private int pistolDamage;
    [SerializeField] private int shotgunDamage;
    [SerializeField] private int magicDamage;
    [Header("Magic Specific Settings")]
    [SerializeField] private GameObject magicProjectile;
    [SerializeField] private int magicProjectileSpeed;
    [SerializeField] private int magicDespawnTime;
    [Header("Raycast Settings")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int maxInteractDistance;

    public int CurrentAmmo { get; set; }
    private PlayerInventory _playerInventory;
    private Camera _playerCamera;
    private GameObject _projectileSpawnPoint;
    private GameObject _spawnedProjectile;
    private MagicProjectile _magicProjectile;
    
    
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
        switch (hit.transform.tag)
        {
            case "Enemy":
                var collidedEnemy = hit.transform.gameObject;
                collidedEnemy.GetComponent<EnemyController>().TakeDamage(dualWieldDamage);
                break;
        }

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
        _spawnedProjectile = Instantiate(magicProjectile, _projectileSpawnPoint.transform.position, transform.rotation);
        _magicProjectile = _spawnedProjectile.GetComponent<MagicProjectile>();
        _magicProjectile.Initialize(magicDamage, magicProjectileSpeed, magicDespawnTime, false);
        CurrentAmmo--;
        if (CurrentAmmo > 0) return;
        _playerInventory.ExpireWeapon(_playerInventory.CurrentCard);
    }
}
