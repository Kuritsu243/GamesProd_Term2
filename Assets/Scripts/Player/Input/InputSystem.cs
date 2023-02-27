using Player.Inventory;
using Player.Shooting;
using UnityEngine;

namespace Player.Input
{

    public class InputSystem : MonoBehaviour
    {
        // private
        private PlayerControls _playerControls;
        private PlayerMovement _playerMovement;
        private PlayerMouseLook _playerMouseLook;
        private PlayerInventory _playerInventory;
        private PlayerShooting _playerShooting;
        private Vector2 _movementInput;
        private Vector2 _mouseInput;
        private Vector2 _scrollInput;

        // encapsulated
        public bool MouseFire { get; }
        public float VerticalInput { get; set; }
        public float HorizontalInput { get; set; }

        public float MouseX { get; set; }

        public float MouseY { get; set; }

        public float ScrollY { get; set; }

        public bool ScrollUp { get; set; }
        public bool ScrollDown { get; set; }

        private void OnEnable()
        {
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();
                _playerControls.playerInput.playerMovement.performed += i => _movementInput = i.ReadValue<Vector2>();
                _playerControls.playerInput.playerJump.performed += _ => _playerMovement.Jump();
                _playerControls.playerInput.playerDash.performed += _ => _playerMovement.Dash();
                _playerControls.playerInput.playerMouseX.performed += i => _mouseInput.x = i.ReadValue<float>();
                _playerControls.playerInput.playerMouseY.performed += i => _mouseInput.y = i.ReadValue<float>();
                _playerControls.playerInput.playerInteract.performed += _ => _playerMouseLook.Interact();
                _playerControls.playerInput.playerInventory.performed += _ => _playerInventory.OpenInventory();
                _playerControls.playerInput.changeActiveItem.performed += i => _scrollInput = i.ReadValue<Vector2>();
                _playerControls.playerInput.changeActiveItem.performed += _ => _playerInventory.ChangeActiveItem();
                _playerControls.playerInput.playerChangeItemMode.performed +=
                    _ => _playerInventory.ChangeActiveCardMode();
                _playerControls.playerInput.playerFire.performed += _ => _playerShooting.Fire();
            }

            _playerControls.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMouseLook = GetComponent<PlayerMouseLook>();
            _playerInventory = GetComponent<PlayerInventory>();
            _playerShooting = GetComponent<PlayerShooting>();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleMouseInput();
            HandleScrollInput();
        }

        private void HandleMovementInput()
        {
            VerticalInput = _movementInput.y;
            HorizontalInput = _movementInput.x;
        }

        private void HandleMouseInput()
        {
            MouseX = _mouseInput.x;
            MouseY = _mouseInput.y;
        }

        private void HandleScrollInput()
        {
            ScrollY = _scrollInput.y;
            switch (ScrollY)
            {
                case > 0:
                    ScrollUp = true;
                    ScrollDown = false;
                    break;
                case < 0:
                    ScrollDown = true;
                    ScrollUp = false;
                    break;
                default:
                    ScrollUp = false;
                    ScrollDown = false;
                    break;
            }
        }

    }
}
