using UnityEngine;

namespace Player.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")] 
        [SerializeField] private float maxHealth;

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
            Debug.Log("player died");
        }

        public void Damage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0) Die();
        }
    }
}
