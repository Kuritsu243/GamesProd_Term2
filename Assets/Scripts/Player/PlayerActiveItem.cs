using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItem : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeActiveModel(Mesh newMesh, Material[] newItemMaterials)
    {
        _meshFilter.mesh = newMesh;
        _meshRenderer.materials = newItemMaterials;
    }
    
    


}
