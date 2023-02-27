using Player.Input;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private InputSystem _inputSystem;
  
        // Start is called before the first frame update
        private void Start()
        {
            _inputSystem = GetComponent<InputSystem>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _inputSystem.HandleAllInputs();
        }
    }
}
