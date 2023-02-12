using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class cardObject : ScriptableObject
{
    [SerializeField] private Mesh cardModel;
    [SerializeField] private MeshFilter cardMesh;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponAmmo;
    [SerializeField] private GameObject weaponProjectile;
    [SerializeField] public Sprite cardSprite;
    public MeshRenderer meshRenderer;
    public PlayerInventory playerInventory;

    public bool IsInInventory { get; set; }
    public bool IsCurrentlyActive { get; set; }
    
}
