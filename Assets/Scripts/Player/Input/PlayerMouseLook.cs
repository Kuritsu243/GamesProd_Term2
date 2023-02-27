using UnityEngine;
using UnityEngine.InputSystem;

// penis

namespace Player.Input
{
    public class PlayerMouseLook : MonoBehaviour
    {
        [Header("Camera")] [Range(40, 120)] 
        [SerializeField] private float cameraFOV;
        [SerializeField] private float xClamp;

        [Header("Mouse Options")] 
        [SerializeField] private float mouseSensitivityX;
        [SerializeField] private float mouseSensitivityY;

        [Header("Raycast Settings")] 
        [SerializeField] private int maxInteractDistance;
        [SerializeField] private LayerMask layerMask;
        private InputSystem _inputSystem;
        private Camera _playerCamera;


        private float _mouseX;
        private float _mouseY;
        private float _xRotation;
        private Vector3 _targetRotation;
    
    
    
        private void Start()
        {
            _inputSystem = GetComponent<InputSystem>();
            _playerCamera = GetComponentInChildren<Camera>();
            _playerCamera.fieldOfView = cameraFOV;
        }

        private void FixedUpdate()
        {
            CalculateMouseInput();
        }

        private void CalculateMouseInput()
        {
            _mouseX = _inputSystem.MouseX * mouseSensitivityX;
            _mouseY = _inputSystem.MouseY * mouseSensitivityY;
        }

        public void HandleRotation()
        {
            transform.Rotate(Vector3.up, _mouseX * Time.deltaTime);
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -xClamp, xClamp);
            _targetRotation = transform.eulerAngles;
            _targetRotation.x = _xRotation;
            _playerCamera.transform.eulerAngles = _targetRotation;
        }

        public void Interact()
        {
            var rayOrigin = _playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(rayOrigin, out var hit, maxInteractDistance)) return;
            switch (hit.transform.tag)
            {
                case "Card":
                    var collidedCard = hit.transform.gameObject;
                    collidedCard.GetComponent<cardController>().CollectCard();
                    break;
                case null:
                    Debug.Log("this has no tag");
                    break;
            }
        
        }
    
    }
}
