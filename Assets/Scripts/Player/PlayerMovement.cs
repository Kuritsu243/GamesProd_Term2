using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float playerSpeed;
    [Header("Jumping and Gravity")]
    [SerializeField] private float playerGravity;
    [SerializeField] private float playerJumpHeight;
    [Header("Player Dash")]
    [SerializeField] private float playerDashMultiplier;
    [SerializeField] private float playerDashCooldown;
    [SerializeField] private float playerDashDuration;
    [Header("What layer is classed as floor")]
    [SerializeField] private LayerMask groundMask;

    
    
    private InputSystem _inputSystem;
    private CharacterController _characterController;
    private PlayerMouseLook _playerMouseLook;
    private Vector3 _playerVelocity;
    private Vector3 _verticalVelocity = Vector3.zero;
    private bool _isGrounded;
    private bool _isPlayerJumping;
    private bool _isDashing;
    private bool _isInDashCooldown;
    private void Start()
    {
        _inputSystem = GetComponent<InputSystem>();
        _characterController = GetComponent<CharacterController>();
        _playerMouseLook = GetComponent<PlayerMouseLook>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _inputSystem.HandleAllInputs();
        HandleAllMovement();
    }

    private void HandleMovement()
    {
        CheckIfGrounded();
        if (_isGrounded)
            _verticalVelocity.y = 0f;

        _playerVelocity = _isDashing switch
        {
            true => (transform.right * _inputSystem.HorizontalInput + transform.forward * _inputSystem.VerticalInput) *
                    (playerSpeed * playerDashMultiplier),
            false => (transform.right * _inputSystem.HorizontalInput + transform.forward * _inputSystem.VerticalInput) *
                     playerSpeed
        };

        _characterController.Move(_playerVelocity * Time.deltaTime);
        
        switch (_isPlayerJumping)
        {
            case true when !_isGrounded:
                _isPlayerJumping = false;
                break;
            case true when _isGrounded:
                _verticalVelocity.y = Mathf.Sqrt(-2f * playerJumpHeight * playerGravity);
                break;
            case false:
                break;
        }
        
        
        _verticalVelocity.y += playerGravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
    

    public void Jump()
    {
        _isPlayerJumping = true;
    }

    public void Dash()
    {
        if (_isInDashCooldown) return;
        _isDashing = true;
        _verticalVelocity.y = 0.0f;
        StartCoroutine(DashCooldown());
    }

    private void CheckIfGrounded()
    {
        _isGrounded = _characterController.isGrounded;
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
