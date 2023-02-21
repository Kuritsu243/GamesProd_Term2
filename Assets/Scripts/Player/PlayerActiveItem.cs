using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerActiveItem : MonoBehaviour
{
    [SerializeField] private GameObject dualWield;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject magicWeapon;
    [SerializeField] private GameObject shotgun;
    private PlayerInventory _playerInventory;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private GameObject _activeModel;

    // Start is called before the first frame update
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _activeModel = gameObject;
        dualWield.SetActive(false);
        pistol.SetActive(false);
        magicWeapon.SetActive(false);
        shotgun.SetActive(false);
    }

    public void ChangeActiveModel(cardObject activeCard)
    {
        switch (activeCard.weaponType)
        {
            case cardObject.WeaponType.Magic:
                dualWield.SetActive(false);
                pistol.SetActive(false);
                magicWeapon.SetActive(true);
                shotgun.SetActive(false);
                break;
            case cardObject.WeaponType.Shotgun:
                dualWield.SetActive(false);
                pistol.SetActive(false);
                magicWeapon.SetActive(false);
                shotgun.SetActive(true);
                break;
            case cardObject.WeaponType.DualWield:
                dualWield.SetActive(true);
                pistol.SetActive(false);
                magicWeapon.SetActive(false);
                shotgun.SetActive(false);
                break;
            case cardObject.WeaponType.Pistol:
                dualWield.SetActive(false);
                pistol.SetActive(true);
                magicWeapon.SetActive(false);
                shotgun.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
    
    


}
