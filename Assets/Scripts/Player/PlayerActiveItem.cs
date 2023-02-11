using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItem : MonoBehaviour
{

    private PlayerInventory _playerInventory;
    private int _activeItemIndex;
    public cardObject CurrentCard { get; set; }
    // Start is called before the first frame update
    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    public void SetActiveCard(int cardIndex)
    {
        CurrentCard = _playerInventory.CurrentCards[cardIndex];
    }


}
