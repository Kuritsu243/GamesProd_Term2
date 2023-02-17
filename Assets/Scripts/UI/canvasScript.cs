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
    private TextMeshProUGUI _activeItemNoText;
    private TextMeshProUGUI _activeItemMode;

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
        _activeItemNoText = GameObject.FindGameObjectWithTag("activeItemSlot").GetComponent<TextMeshProUGUI>();
        _activeItemMode = GameObject.FindGameObjectWithTag("activeItemMode").GetComponent<TextMeshProUGUI>();
        foreach (var inventorySlot in inventorySlots)
        {
            inventorySlot.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _activeItemText.text = _playerInventory.CurrentCard.name;
        _activeItemNoText.text = _playerInventory.ActiveItemIndex.ToString();

        _activeItemMode.text = _playerInventory.IsInBuffMode switch
        {
            
            true when !_playerInventory.IsInWeaponMode => _playerInventory.CurrentCard.buffType.ToString(),
            false and false => _playerInventory.CurrentCard.weaponType.ToString(),
            _ => "",
        };
        // if (_playerInventory.CurrentCard.isInBuffMode && !_playerInventory.CurrentCard.isInWeaponMode)
        // {
        //     _activeItemMode.text = "Buff Mode";
        // }
        // else if (_playerInventory.CurrentCard.isInWeaponMode && !_playerInventory.CurrentCard.isInBuffMode)
        // {
        //     _activeItemMode.text = "Weapon Mode";
        // }
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

    public void RotateCard()
    {
        
    }
}
