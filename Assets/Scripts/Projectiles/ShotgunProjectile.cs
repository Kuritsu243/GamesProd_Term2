using System;
using UnityEngine;
using Enemies;
using Random = UnityEngine.Random;


namespace Projectiles
{
    public class ShotgunProjectile : MonoBehaviour
    {
        private Rigidbody _pelletRigidbody;
        private Collider _pelletCollider;
        private int _pelletDamage;
        private int _pelletSpeed;
        private int _pelletDespawnTime;
        private Vector3 _direction;
        private void Start()
        {


        }


        public void Initialize(int damage, int projectileSpeed, int despawnTime, Vector3 spawnDirection)
        {
            _pelletDamage = damage;
            _pelletSpeed = projectileSpeed;
            _pelletDespawnTime = despawnTime;
            _direction = spawnDirection;
            Invoke(nameof(Despawn), _pelletDespawnTime);
            _pelletRigidbody = GetComponent<Rigidbody>();
            _pelletCollider = GetComponent<Collider>();
            // _pelletRigidbody.AddForce((_direction + transform.forward) * _pelletSpeed, ForceMode.Impulse);
            _pelletRigidbody.velocity = (spawnDirection + transform.forward) * projectileSpeed;

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
