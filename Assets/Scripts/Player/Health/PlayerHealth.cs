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
        
        public float MaxHealth => maxHealth;

        // Start is called before the first frame update
        private void Start()
        {
            CurrentHealth = maxHealth;
        }
        

        public void Die()
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