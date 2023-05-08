using UnityEngine;
using UnityEngine.SceneManagement;
namespace Player.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")] 
        [SerializeField] private float maxHealth;

        [SerializeField] private SceneObject deathScreen;
        
        public float CurrentHealth { get; set; }

        // Start is called before the first frame update
        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
        
        }

        private void Die()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(deathScreen);
        }

        public void Damage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0) Die();
        }
    }
}
