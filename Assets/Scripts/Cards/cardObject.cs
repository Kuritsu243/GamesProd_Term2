using Player.Inventory;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
    public class cardObject : ScriptableObject
    {
    
        public enum BuffType
        {
            Dash,
            Damage,
            JumpHeight,
            Speed
        
        }
    
        public enum WeaponType
        {
            DualWield,
            Pistol,
            Shotgun,
            Magic,
            RocketLauncher
        }
        [Header("3D Space")]
        [SerializeField] public GameObject cardModel;
        [SerializeField] public MeshFilter cardMesh;
        [SerializeField] public Material[] cardMaterials;
        [Header("2D Space")]
        [SerializeField] public Sprite cardSprite;
        [SerializeField] public string cardInfo;
        [SerializeField] public Sprite crosshairSprite;
        [Header("Damage, and Ammo")]
        [SerializeField] private float weaponDamage;
        [SerializeField] private float weaponAmmo;
        [Header("Weapon and Buff Type")]
        [SerializeField] public BuffType buffType;
        [SerializeField] public WeaponType weaponType;


        [HideInInspector] public MeshRenderer meshRenderer;
        [HideInInspector] public PlayerInventory playerInventory;

        public bool IsInInventory { get; set; }
        public bool IsCurrentlyActive { get; set; }
    
    }
}
