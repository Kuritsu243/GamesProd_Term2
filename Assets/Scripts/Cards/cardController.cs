using System.Collections;
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
        _maxIndex = cards.Count;
        Debug.Log(RandomizeCard());
    }

    public void CollectCard()
    {
        _playerInventory.AddToInventory(RandomizeCard());
        Destroy(this.gameObject);
        
    }
}
