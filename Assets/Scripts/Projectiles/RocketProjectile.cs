using System;
using Enemies;
using Player.Health;
using UnityEngine;

namespace Projectiles
{
    public class RocketProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffect;
        private Rigidbody _rocketRigidbody;
        private Collider _rocketCollider;
        private float _rocketDamage;
        private int _rocketSpeed;
        private int _rocketDespawnTime;
        private int _rocketSplashRadius;
        // Start is called before the first frame update
        private void Start()
        {
            _rocketRigidbody = GetComponent<Rigidbody>();
            _rocketCollider = GetComponent<Collider>();
            // _rocketRigidbody.AddForce(transform.forward * _rocketSpeed, ForceMode.VelocityChange);
            Physics.IgnoreLayerCollision(3, 7);
        }
        

        public void Initialize(float damage, int projectileSpeed, int despawnTime, int splashRadius)
        {
            _rocketDamage = damage;
            _rocketSpeed = projectileSpeed;
            _rocketDespawnTime = despawnTime;
            _rocketSplashRadius = splashRadius;
            Invoke(nameof(Explode), _rocketDespawnTime);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player"))
                Physics.IgnoreCollision(other, GetComponent<Collider>());
                
            Explode();
        }

        private void Explode()
        {
            Destroy(gameObject);
            var position = transform.position;
            var spawnedEffect = Instantiate(explosionEffect, position, Quaternion.identity);
            Destroy(spawnedEffect, 1.5f);
            var hitColliders = new Collider[20];
            var size = Physics.OverlapSphereNonAlloc(position, _rocketSplashRadius, hitColliders);
            if (size < 1) return;
            for (var i = 0; i < size; i++)
            {
                var currentCollider = hitColliders[i];
                var currentGameObject = currentCollider.transform.root.gameObject;
                switch (currentGameObject.tag)
                {
                    case "Enemy":
                        var hitEnemyScript = currentGameObject.GetComponent<EnemyController>();
                        hitEnemyScript.TakeDamage(_rocketDamage);
                        break;
                    case "Player":
                        var playerHealthScript = currentGameObject.GetComponent<PlayerHealth>();
                        playerHealthScript.Damage(_rocketDamage / 5);
                        break;
                    
                }
                
            }
        }
    }
}
