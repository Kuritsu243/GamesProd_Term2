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

    private PlayerActiveItem _playerActiveItem;
    //encapsulated
    public List<cardObject> CurrentCards { get => _currentCards; }
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
        var currentItemIndex = _currentCards.IndexOf(_playerActiveItem.CurrentCard);
    }
    

    private void Start()
    {
        _canvasScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<canvasScript>();
        _playerActiveItem = GetComponent<PlayerActiveItem>();
    }
}
