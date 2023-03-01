using System;
using Cards;
using Player.Inventory;
using UnityEngine;

namespace Player.Shooting
{
    public class PlayerActiveItem : MonoBehaviour
    {
        [SerializeField] private GameObject dualWield;
        [SerializeField] private GameObject pistol;
        [SerializeField] private GameObject magicWeapon;
        [SerializeField] private GameObject shotgun;
        [SerializeField] private GameObject rocketLauncher;
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
            rocketLauncher.SetActive(false);
            shotgun.SetActive(false);
            pistol.SetActive(false);
            magicWeapon.SetActive(false);


        }

        public void ChangeActiveModel(cardObject activeCard)
        {
            switch (activeCard.weaponType)
            {
                case cardObject.WeaponType.Magic:
                    rocketLauncher.SetActive(false);
                    dualWield.SetActive(false);
                    shotgun.SetActive(false);
                    pistol.SetActive(false);
                    magicWeapon.SetActive(true);

                    break;
                case cardObject.WeaponType.Shotgun:
                    rocketLauncher.SetActive(false);
                    dualWield.SetActive(false);
                    shotgun.SetActive(true);
                    pistol.SetActive(false);
                    magicWeapon.SetActive(false);

                    break;
                case cardObject.WeaponType.DualWield:
                    rocketLauncher.SetActive(false);
                    dualWield.SetActive(true);
                    shotgun.SetActive(false);
                    pistol.SetActive(false);
                    magicWeapon.SetActive(false);

                    break;
                case cardObject.WeaponType.Pistol:
                    rocketLauncher.SetActive(false);
                    dualWield.SetActive(false);
                    shotgun.SetActive(false);
                    pistol.SetActive(true);
                    magicWeapon.SetActive(false);

                    break;
                case cardObject.WeaponType.RocketLauncher:
                    rocketLauncher.SetActive(true);
                    dualWield.SetActive(false);
                    shotgun.SetActive(false);
                    pistol.SetActive(false);
                    magicWeapon.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    
    


    }
}
