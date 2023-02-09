using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class canvasScript : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySlots;
    private GameObject _player;
    private PlayerInventory _playerInventory;
    private GameObject _inventoryUI;

    private int _previousInvSize = 0;
    private int _slotIndex = 0;

    private bool _isInvOpen;
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInventory = _player.GetComponent<PlayerInventory>();
        _inventoryUI = GameObject.FindGameObjectWithTag("inventoryUI");
        _inventoryUI.SetActive(false);
        foreach (var inventorySlot in inventorySlots)
        {
            inventorySlot.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
    }

    public void ToggleInventory(int cardIndex)
    {
        Debug.Log(cardIndex);
        
        _isInvOpen = !_isInvOpen;
        _inventoryUI.SetActive(!_isInvOpen);
        
        if (!_isInvOpen) return;
        if (cardIndex != _previousInvSize) return;
        _slotIndex = 0;
        foreach (var inventorySlot in inventorySlots)
        {
            switch (cardIndex <= _slotIndex && cardIndex > -1)
            {
                case true:
                    inventorySlot.SetActive(true);
                    _slotIndex++;
                    break;
                case false:
                    inventorySlot.SetActive(false);
                    _slotIndex++;
                    break;
            }
        }
        _previousInvSize = cardIndex;
    }
    
}
