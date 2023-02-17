using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Shotgun
    }
    
    [SerializeField] private Mesh cardModel;
    [SerializeField] private MeshFilter cardMesh;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponAmmo;
    [SerializeField] public Sprite cardSprite;
    [SerializeField] public BuffType buffType;
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public Sprite crosshairSprite;
    public MeshRenderer meshRenderer;
    public PlayerInventory playerInventory;

    public bool IsInInventory { get; set; }
    public bool IsCurrentlyActive { get; set; }
    
}
