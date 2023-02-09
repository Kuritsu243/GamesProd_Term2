using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class cardObject : ScriptableObject
{
    [SerializeField] private Mesh cardModel;
    [SerializeField] private MeshFilter cardMesh;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponAmmo;
    [SerializeField] private GameObject weaponProjectile;
    public MeshRenderer MeshRenderer;
    public PlayerInventory PlayerInventory;
    
    public bool IsInInventory { get; set; }
    public bool IsCurrentlyActive { get; set; }
    
}
