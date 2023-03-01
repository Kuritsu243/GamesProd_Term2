using System.Collections;
using Cards;
using Player;
using Player.Health;
using Player.Inventory;
using Player.Shooting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class canvasScript : MonoBehaviour
    {
        [SerializeField] private GameObject[] inventorySlots;
        [SerializeField] private int timeToShowNewCard;
        private GameObject _player;
        private PlayerInventory _playerInventory;
        private PlayerActiveItem _playerActiveItem;
        private GameObject _inventoryUI;
        private GameObject _cardSprite;
        private TextMeshProUGUI _activeItemText;
        private TextMeshProUGUI _activeItemNoText;
        private TextMeshProUGUI _activeItemMode;
        private GameObject _cardInfoUI;
        private TextMeshProUGUI _cardInfoName;
        private TextMeshProUGUI _cardInfoDesc;
        private PlayerHealth _playerHealth;
        private TextMeshProUGUI _currentHealthText;
        private TextMeshProUGUI _currentAmmoText;
        private PlayerShooting _playerShooting;
        private int _previousInvSize = 0;
        private int _slotIndex = 0;
        private bool _currentlyRotating;

        private bool _isInvOpen;
        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerShooting = _player.GetComponent<PlayerShooting>();
            _playerHealth = _player.GetComponent<PlayerHealth>();
            _currentHealthText = GameObject.FindGameObjectWithTag("currentHealth").GetComponent<TextMeshProUGUI>();
            _playerInventory = _player.GetComponent<PlayerInventory>();
            _playerActiveItem = _player.GetComponent<PlayerActiveItem>();
            _inventoryUI = GameObject.FindGameObjectWithTag("inventoryUI");
            _inventoryUI.SetActive(false);
            _activeItemText = GameObject.FindGameObjectWithTag("activeItem").GetComponent<TextMeshProUGUI>();
            _activeItemNoText = GameObject.FindGameObjectWithTag("activeItemSlot").GetComponent<TextMeshProUGUI>();
            _activeItemMode = GameObject.FindGameObjectWithTag("activeItemMode").GetComponent<TextMeshProUGUI>();
            _cardInfoUI = GameObject.FindGameObjectWithTag("cardInfo");
            _cardInfoName = GameObject.FindGameObjectWithTag("newCardName").GetComponent<TextMeshProUGUI>();
            _cardInfoDesc = GameObject.FindGameObjectWithTag("newCardDesc").GetComponent<TextMeshProUGUI>();
            _currentAmmoText = GameObject.FindGameObjectWithTag("ammoText").GetComponent<TextMeshProUGUI>();
            _cardSprite = GameObject.FindGameObjectWithTag("cardSprite");
            _cardInfoUI.SetActive(false);
            foreach (var inventorySlot in inventorySlots)
            {
                inventorySlot.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            _currentHealthText.text = _playerHealth.CurrentHealth.ToString();
            if (_playerInventory.CurrentCards.Count < 1) return;
            _activeItemText.text = _playerInventory.CurrentCard.name;
            _activeItemNoText.text = _playerInventory.ActiveItemIndex.ToString();
            _currentAmmoText.text = _playerShooting.CurrentAmmo.ToString();
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
            StartCoroutine(CardRotation(180f, 0.33f));
        }

        private IEnumerator CardRotation(float rotateAmount, float timeToTake)
        {
            if (_currentlyRotating) yield break;
            _currentlyRotating = true;
            var startRotation = _cardSprite.transform.eulerAngles.z;
            var endRotation = startRotation + rotateAmount;
            var t = 0f;
            while (t < timeToTake)
            {
                t += Time.deltaTime;
                var zRotation = Mathf.Lerp(startRotation, endRotation, t / timeToTake) % 360.0f;
                var eulerAngles = _cardSprite.transform.eulerAngles;
                eulerAngles = new Vector3(eulerAngles.x,
                    eulerAngles.y, zRotation);
                _cardSprite.transform.eulerAngles = eulerAngles;
                yield return null;
            }
            _currentlyRotating = false;

        }

        public void ShowNewCardUI(cardObject newCard)
        {
            _cardInfoName.text = newCard.name;
            _cardInfoDesc.text = newCard.cardInfo;
            StartCoroutine(NewCard());
        }

        private IEnumerator NewCard()
        {
            _cardInfoUI.SetActive(true);
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(timeToShowNewCard);
            _cardInfoName.text = "";
            _cardInfoDesc.text = "";
            _cardInfoUI.SetActive(false);
            Time.timeScale = 1f;
        }
    
    
    }
}
