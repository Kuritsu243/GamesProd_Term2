using System;
using UnityEngine;
using Enemies;


namespace Projectiles
{
    public class ShotgunProjectile : MonoBehaviour
    {
        private Rigidbody _pelletRigidbody;
        private Collider _pelletCollider;
        private int _pelletDamage;
        private int _pelletSpeed;
        private int _pelletDespawnTime;
        private void Start()
        {
            _pelletRigidbody = GetComponent<Rigidbody>();
            _pelletCollider = GetComponent<Collider>();
            _pelletRigidbody.AddForce(transform.forward * _pelletSpeed, ForceMode.Impulse);
        }


        public void Initialize(int damage, int projectileSpeed, int despawnTime)
        {
            _pelletDamage = damage;
            _pelletSpeed = projectileSpeed;
            _pelletDespawnTime = despawnTime;
            Invoke(nameof(Despawn), _pelletDespawnTime);
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.transform.root.tag)
            {
                case "Player":
                    Physics.IgnoreCollision(other, _pelletCollider);
                    break;
                case "Enemy":
                    var enemyScript = other.GetComponent<EnemyController>();
                    enemyScript.TakeDamage(_pelletDamage);
                    Despawn();
                    break;
            }
        }
    }
}
