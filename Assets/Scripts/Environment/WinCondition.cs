using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class WinCondition : MonoBehaviour
    {
        [SerializeField] private SceneObject mainMenu;
        
        public Collider WinCollider { get; private set; }
        public bool CanWin { get; set; }
        // Start is called before the first frame update
        private void Start()
        {
            WinCollider = GetComponent<Collider>();
            WinCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.gameObject.CompareTag("Player")) return;
            if (!CanWin) return;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(mainMenu);
        }
    }
}
