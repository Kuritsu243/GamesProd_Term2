using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class canvasScript : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySlots;
    private GameObject _player;
    private PlayerInventory _playerInventory;
    private PlayerActiveItem _playerActiveItem;
    private GameObject _inventoryUI;
    private TextMeshProUGUI _activeItemText;

    private int _previousInvSize = 0;
    private int _slotIndex = 0;

    private bool _isInvOpen;
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInventory = _player.GetComponent<PlayerInventory>();
        _playerActiveItem = _player.GetComponent<PlayerActiveItem>();
        _inventoryUI = GameObject.FindGameObjectWithTag("inventoryUI");
        _inventoryUI.SetActive(false);
        _activeItemText = GameObject.FindGameObjectWithTag("activeItem").GetComponent<TextMeshProUGUI>();
        foreach (var inventorySlot in inventorySlots)
        {
            inventorySlot.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _activeItemText.text = _playerInventory.CurrentCard.name;
    }

    public void ToggleInventory(int cardIndex)
    {
        Debug.Log(cardIndex);
        _inventoryUI.SetActive(!_isInvOpen);
        _isInvOpen = !_isInvOpen;
        if (!_isInvOpen) return;
        if (cardIndex == _previousInvSize) return;
        _slotIndex = 0;
        foreach (var inventorySlot in inventorySlots)
        {
            Debug.Log(cardIndex);
            Debug.Log(_slotIndex);
            switch (cardIndex <= _slotIndex && cardIndex > -1)
            {
                case true:
                    inventorySlot.SetActive(false);
                    _slotIndex++;
                    break;
                case false:
                    inventorySlot.SetActive(true);
                    var imageRenderer = inventorySlot.GetComponentInChildren<Image>();
                    imageRenderer.sprite = _playerInventory.CurrentCards[_slotIndex].cardSprite;
                    _slotIndex++;
                    break;
            }
        }
        _previousInvSize = cardIndex;
    }
    
}
