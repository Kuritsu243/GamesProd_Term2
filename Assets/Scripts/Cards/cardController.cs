using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class cardController : MonoBehaviour
{
    private GameObject _player;
    private int _maxIndex;
    private int _chosenIndex;
    private cardObject _chosenCard;
    public List<cardObject> cards;
    private PlayerInventory _playerInventory;
    private PlayerActiveItem _playerActiveItem;


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
