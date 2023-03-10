using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Cards;
using Player;
using Player.Inventory;
using Player.Shooting;
using UnityEngine;

public class cardController : MonoBehaviour
{

    //cards
    private cardObject _chosenCard;
    public List<cardObject> cards;
    
    //player
    private GameObject _player;
    private PlayerInventory _playerInventory;
    private PlayerActiveItem _playerActiveItem;

    //randomisation vars
    private int _maxIndex;
    private int _chosenIndex;
    
    private cardObject RandomizeCard()
    {
        _chosenIndex = Random.Range(0, _maxIndex);
        _chosenCard = cards[_chosenIndex];
        return _chosenCard;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerInventory = _player.GetComponent<PlayerInventory>();
        _playerActiveItem = _player.GetComponentInChildren<PlayerActiveItem>();
        _maxIndex = cards.Count;
        Debug.Log(RandomizeCard());
    }

    public void CollectCard()
    {
        
        if (_playerInventory.CurrentCards.Count == 0)
        {
            _playerInventory.AddToInventory(RandomizeCard());
            _playerInventory.SetActiveCard(_playerInventory.CurrentCards.LastIndexOf(_playerInventory.CurrentCards.Last()));
            
        }
        else
        {
            do { RandomizeCard(); } while (_playerInventory.CurrentCards.Contains(_chosenCard));
            _playerInventory.AddToInventory(_chosenCard);
        }
        Destroy(this.gameObject);
        
    }
}
