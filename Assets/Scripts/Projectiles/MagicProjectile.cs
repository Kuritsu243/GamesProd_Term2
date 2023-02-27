using System.Collections;
using Enemies;
using Player.Health;
using UnityEngine;

namespace Projectiles
{
    public class MagicProjectile : MonoBehaviour
    {
        private int _projectileDamage;
        private int _projectileDespawnRate;
        private Rigidbody _projectileRigidbody;
        private int _projectileSpeed;
        private bool _enemyProjectile;

        private void Start()
        {
            _projectileRigidbody = GetComponent<Rigidbody>();
            _projectileRigidbody.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
            StartCoroutine(ProjectileLifeCounter());
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject);
            var collidedObject = collision.transform.root.gameObject;
            switch (collidedObject.tag)
            {
                case "Enemy" when !_enemyProjectile:
                    var enemyScript = collidedObject.GetComponent<EnemyController>();
                    enemyScript.TakeDamage(_projectileDamage);
                    Despawn();
                    break;
                case "Player" when _enemyProjectile:
                    var playerHealth = collidedObject.GetComponentInChildren<PlayerHealth>();
                    playerHealth.Damage(_projectileDamage);
                    Despawn();
                    break;
                
            }
        }

        public void Initialize(int projDamage, int projSpeed, int despawnTime, bool enemyProj)
        {
            _projectileDamage = projDamage;
            _projectileSpeed = projSpeed;
            _projectileDespawnRate = despawnTime;
            _enemyProjectile = enemyProj;
        }

        private IEnumerator ProjectileLifeCounter()
        {
            yield return new WaitForSeconds(_projectileDespawnRate);
            Despawn();
        }

        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}