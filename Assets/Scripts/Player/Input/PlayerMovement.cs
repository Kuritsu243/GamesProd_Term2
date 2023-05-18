using System;
using System.Collections;
using Cards;
using Player.Inventory;
using Player.Buff;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Input
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player Movement")]
        [SerializeField] private float playerSpeed;
        [SerializeField] private AudioClip[] footstepSounds;
        [Header("Jumping and Gravity")]
        [SerializeField] private float playerGravity;
        [SerializeField] private float playerJumpHeight;
        [Header("Player Dash")]
        [SerializeField] private float playerDashMultiplier;
        [SerializeField] private float playerDashCooldown;
        [SerializeField] private float playerDashDuration;
        [Header("Player Jump")] 
        [SerializeField] private float playerJumpHeightModifier;
        [SerializeField] private float playerJumpBuffDuration;
        [Header("What layer is classed as floor")]
        [SerializeField] private LayerMask groundMask;

        private InputSystem _inputSystem;
        private CharacterController _characterController;
        private AudioSource _playerAudioSource;
        private PlayerMouseLook _playerMouseLook;
        private PlayerInventory _playerInventory;
        private PlayerBuff _playerBuff;
        private Vector3 _playerVelocity;
        private Vector3 _verticalVelocity = Vector3.zero;
        private bool _isGrounded;
        private bool _isPlayerJumping;
        private bool _isDashing;
        private bool _isInDashCooldown;
        private bool _isJumpBuffActive;
        private bool _canPlaySound = true;

        public bool IsJumpBuffActive { get => _isJumpBuffActive; set => _isJumpBuffActive = value; }
        
        public float JumpHeight
        {
            get => playerJumpHeight;
            set => playerJumpHeight = value;
        }

        public float MoveSpeed
        {
            get => playerSpeed;
            set => playerSpeed = value;
        }
        public bool IsZipLining { get; set; }
        private void Start()
        {
            _inputSystem = GetComponent<InputSystem>();
            _characterController = GetComponent<CharacterController>();
            _playerAudioSource = GetComponent<AudioSource>();
            _playerMouseLook = GetComponent<PlayerMouseLook>();
            _playerBuff = GetComponent<PlayerBuff>();
            _playerInventory = GetComponent<PlayerInventory>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _inputSystem.HandleAllInputs();
            HandleAllMovement();
        }

        private void HandleMovement()
        {
            if (IsZipLining) return;
            CheckIfGrounded();
            if (_isGrounded)
                _verticalVelocity.y = 0f;

            _playerVelocity = _isDashing switch
            {
                true => (transform.right * _inputSystem.HorizontalInput +
                         transform.forward * _inputSystem.VerticalInput) * (playerSpeed * playerDashMultiplier),
                false when !_playerBuff.IsSpeedBuffActive => (transform.right * _inputSystem.HorizontalInput +
                                                              transform.forward * _inputSystem.VerticalInput) *
                                                             playerSpeed,
                false when _playerBuff.IsSpeedBuffActive => (transform.right * _inputSystem.HorizontalInput +
                                                             transform.forward * _inputSystem.VerticalInput) *
                                                            (playerSpeed * _playerBuff.SpeedBuffModifier),
                _ => throw new ArgumentOutOfRangeException(nameof(_isDashing))
            };

            _characterController.Move(_playerVelocity * Time.deltaTime);
        
            switch (_isPlayerJumping)
            {
                case true when !_isGrounded:
                    _isPlayerJumping = false;
                    break;
                case true when _isGrounded && !_playerBuff.IsJumpBuffActive:
                    _verticalVelocity.y = Mathf.Sqrt(-2f * playerJumpHeight * playerGravity);
                    break;
                case true when _isGrounded && _playerBuff.IsJumpBuffActive:
                    _verticalVelocity.y = Mathf.Sqrt(-2f * (playerJumpHeight + _playerBuff.JumpBuffModifier) * playerGravity);
                    break;
                case false:
                    break;
            }
        
        
            _verticalVelocity.y += playerGravity * Time.deltaTime;
            _characterController.Move(_verticalVelocity * Time.deltaTime);
            if (_playerVelocity == Vector3.zero) return;
            StartCoroutine(PlayFootstepSound());
        }
    

        public void Jump()
        {
            _isPlayerJumping = true;
        }

        public void Dash()
        {
            if (_isInDashCooldown) return;
            if (!_playerBuff.IsDashBuffActive) return;
            _isDashing = true;
            _verticalVelocity.y = 0.0f;
            StartCoroutine(DashCooldown());
        }

        private void CheckIfGrounded()
        {
            _isGrounded = _characterController.isGrounded;
        }

        private IEnumerator PlayFootstepSound()
        {
            if (!_canPlaySound) yield break;
            if (!_isGrounded) yield break;
            var maxIndex = footstepSounds.Length;
            var chosenIndex = Random.Range(0, maxIndex);
            var chosenClip = footstepSounds[chosenIndex];
            var footstepDuration = chosenClip.length + 0.25f;
            _playerAudioSource.PlayOneShot(chosenClip, 0.1f);
            _canPlaySound = false;
            yield return new WaitForSeconds(footstepDuration);
            _canPlaySound = true;
        }

        private void HandleAllMovement()
        {

            HandleMovement();
            _playerMouseLook.HandleRotation();
        }

        private IEnumerator DashCooldown()
        {
            yield return new WaitForSeconds(playerDashDuration);
            _isDashing = false;
            _isInDashCooldown = true;
            yield return new WaitForSeconds(playerDashCooldown);
            _isInDashCooldown = false;
        }
    }
}
