using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItem : MonoBehaviour
{

    private PlayerInventory _playerInventory;


    // Start is called before the first frame update
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }
    


}
