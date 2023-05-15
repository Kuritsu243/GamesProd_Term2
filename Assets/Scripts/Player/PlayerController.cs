using Player.Input;
using UnityEngine;

namespace Player
{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject soldierObject;
        
        private InputSystem _inputSystem;
        private PlayerMovement _playerMovement;
        private Vector3 _previousPos = Vector3.zero;
        private Animator _playerAnimator;
        private float _velocity;
        private float _walkDuration;
        
        // Start is called before the first frame update
        private void Start()
        {
            _inputSystem = GetComponent<InputSystem>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimator = soldierObject.GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _inputSystem.HandleAllInputs();

            switch (_velocity)
            {
                case > 0.1f:
                    _playerAnimator.Play("demo_combat_run");
                    break;
                case < 0.1f:
                    _playerAnimator.Play("demo_combat_idle");
                    break;
            }
        }
        private void LateUpdate()
        {
            _velocity = (transform.position - _previousPos).magnitude / Time.deltaTime;
            _previousPos = transform.position;
        }
    }
}
