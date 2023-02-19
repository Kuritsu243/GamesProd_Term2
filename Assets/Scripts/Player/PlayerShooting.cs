using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private int maxDualWieldAmmo;
    [SerializeField] private int maxPistolAmmo;
    [SerializeField] private int maxShotgunAmmo;
    [SerializeField] private int dualWieldDamage;
    [SerializeField] private int pistolDamage;
    [SerializeField] private int shotgunDamage;
    [SerializeField] private GameObject weaponProjectile;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int maxInteractDistance;
    public int CurrentAmmo { get; set; }
    private PlayerInventory _playerInventory;
    private Camera _playerCamera;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _playerCamera = GetComponentInChildren<Camera>();
        CurrentAmmo = 10;
    }

    // Update is called once per frame
    public void Fire()
    {
        if (CurrentAmmo <= 0) return;
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
                collidedEnemy.GetComponent<enemyController>().TakeDamage(dualWieldDamage);
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
}
