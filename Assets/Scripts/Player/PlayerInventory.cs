using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int invSize;

    private List<cardObject> _currentCards = new List<cardObject>();
    private canvasScript _canvasScript;
    private int _nextItemIndex;
    private PlayerActiveItem _playerActiveItem;
    private InputSystem _inputSystem;
    
    
    //encapsulated
    public List<cardObject> CurrentCards => _currentCards;
    public cardObject CurrentCard { get; set; }
    
    public bool IsInBuffMode { get; set; }
    public bool IsInWeaponMode { get; set; }
    public int ActiveItemIndex { get; set; }
    public void AddToInventory(cardObject cardToAdd)
    {
        if (_currentCards.Count >= invSize) return;
        _currentCards.Add(cardToAdd);
    }

    public void OpenInventory()
    {
        var cardIndex = _currentCards.Count();
        _canvasScript.ToggleInventory(cardIndex);
    }

    public void ChangeActiveItem()
    {
        _nextItemIndex = ActiveItemIndex;
        switch (_inputSystem.ScrollUp)
        {
            case true when !_inputSystem.ScrollDown && _currentCards.Count()-1 == ActiveItemIndex: // if at top end of inv
                _nextItemIndex = 0; // set to zero
                break;
            case true when !_inputSystem.ScrollDown && _currentCards.Count != ActiveItemIndex: // if not at top end of inv
                _nextItemIndex++; // increment
                break;
            case false when _inputSystem.ScrollDown && ActiveItemIndex == 0: // if scrolling down and at bottom of inv
                _nextItemIndex = _currentCards.Count-1; // set to highest num in array
                break;
            case false when _inputSystem.ScrollDown && ActiveItemIndex != 0: // if not at bottom of inv
                _nextItemIndex--; // decrement
                break;
        }
        SetActiveCard(_nextItemIndex);
    }

    private void FixedUpdate()
    {
        Debug.Log(_currentCards.Count);
    }

    private void Start()
    {
        _canvasScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<canvasScript>();
        _playerActiveItem = GetComponent<PlayerActiveItem>();
        _inputSystem = GetComponent<InputSystem>();
        IsInWeaponMode = true;
        IsInBuffMode = false;
    }
    
    public void SetActiveCard(int cardIndex)
    {
        CurrentCard = _currentCards[cardIndex];
        ActiveItemIndex = cardIndex;
    }

    public void ChangeActiveCardMode()
    {
        switch (IsInBuffMode)
        {
            case false when IsInWeaponMode:
                IsInBuffMode = true;
                IsInWeaponMode = false;
                _canvasScript.RotateCard();
                break;
            case true when !IsInWeaponMode:
                IsInWeaponMode = true;
                IsInBuffMode = false;
                _canvasScript.RotateCard();
                break;
            case true when IsInWeaponMode:
                Debug.Log("you have somehow glitched this");
                break;
            case false when !IsInWeaponMode:
                Debug.Log("you have somehow glitched this");
                break;
        }
    }
}
