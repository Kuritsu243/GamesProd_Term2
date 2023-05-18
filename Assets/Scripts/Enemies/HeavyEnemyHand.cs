using System;
using Player.Health;
using UnityEngine;

namespace Enemies
{
    public class HeavyEnemyHand : MonoBehaviour
    {
    
        public bool CanHit { get; set; }
        public float DamageAmount { get; set; }


        public void OnTriggerEnter(Collider other)
        {
            if (!CanHit) return;
            if (!other.gameObject.CompareTag("Player")) return;
            var playerHealthScript = other.GetComponent<PlayerHealth>();
            playerHealthScript.Damage(DamageAmount);
        }
    }
}