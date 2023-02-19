using System.Collections;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    private int _projectileDamage;
    private int _projectileDespawnRate;
    private Rigidbody _projectileRigidbody;
    private int _projectileSpeed;

    private void Start()
    {
        _projectileRigidbody = GetComponent<Rigidbody>();
        _projectileRigidbody.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
        StartCoroutine(ProjectileLifeCounter());
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                var enemy = collision.gameObject;
                var enemyScript = enemy.GetComponent<enemyController>();
                enemyScript.TakeDamage(_projectileDamage);
                Despawn();
                break;
        }
    }

    public void Initialize(int projDamage, int projSpeed, int despawnTime)
    {
        _projectileDamage = projDamage;
        _projectileSpeed = projSpeed;
        _projectileDespawnRate = despawnTime;
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