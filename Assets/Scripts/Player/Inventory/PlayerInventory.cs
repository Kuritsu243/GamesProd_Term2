using System.Collections.Generic;
using System.Linq;
using Cards;
using Player.Input;
using Player.Shooting;
using UI;
using UnityEngine;

namespace Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int invSize;

        private List<cardObject> _currentCards = new List<cardObject>();
        private List<cardObject> _previouslyCollectedCards = new List<cardObject>();
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
            if (_previouslyCollectedCards.Contains(cardToAdd)) return;
            _previouslyCollectedCards.Add(cardToAdd);
            _canvasScript.ShowNewCardUI(cardToAdd);

        }

        public void OpenInventory()
        {
            var cardIndex = _currentCards.Count();
            _canvasScript.ToggleInventory(cardIndex);
        }


        public void ChangeActiveItem()
        {
            if (_currentCards.Count == 1)
            {
                SetActiveCard(0);
                _playerActiveItem.ChangeActiveModel(CurrentCard);
            }
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
            _playerActiveItem.ChangeActiveModel(CurrentCard);
        }

        private void FixedUpdate()
        {
        
        }
    
        private void Start()
        {
            _canvasScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<canvasScript>();
            _playerActiveItem = GetComponentInChildren<PlayerActiveItem>();
            _inputSystem = GetComponent<InputSystem>();
            IsInWeaponMode = true;
            IsInBuffMode = false;
        }
    
        public void SetActiveCard(int cardIndex)
        {
            CurrentCard = _currentCards[cardIndex];
            ActiveItemIndex = cardIndex;
            _playerActiveItem.ChangeActiveModel(CurrentCard);
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

        public void ExpireWeapon(cardObject card)
        {
            var cardToExpireIndex = _currentCards.IndexOf(card);
            _currentCards.RemoveAt(cardToExpireIndex);
            SetActiveCard(_currentCards.LastIndexOf(_currentCards.Last()));
        }
    }
}
