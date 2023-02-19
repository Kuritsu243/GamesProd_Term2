using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{

    private enum AttackType
    {
        Melee,
        Ranged
    }

    [Header("Enemy Health")] 
    [SerializeField] private int maxHealth;

    [Header("Enemy Attack Settings")] 
    [SerializeField] private int meleeAttackRange;
    [SerializeField] private int meleeDamage;
    [SerializeField] private int rangedDamage;
    [SerializeField] private int rangedAttackCooldown;
    [SerializeField] private int meleeAttackCooldown;
    [SerializeField] private AttackType attackType;

    [Header("Enemy AI Config")] 
    [SerializeField] private int playerDetectionRadius;


    private int _currentHealth;
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private PlayerHealth _playerHealth;

    private bool _canAttack;
    // Start is called before the first frame update
    private void Start()
    {
        _canAttack = true;
        _currentHealth = maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < playerDetectionRadius)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        switch (attackType)
            {
                case AttackType.Melee:
                    if (Vector3.Distance(transform.position, _player.transform.position) > meleeAttackRange) return;
                    AttackPlayerMelee();
                    break;
                case AttackType.Ranged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                
            }
    }


    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void AttackPlayerMelee()
    {
        if (!_canAttack) return;
        _playerHealth.Damage(meleeDamage);
        StartCoroutine(AttackCooldown(meleeAttackCooldown));
    }

    private void AttackPlayerRanged()
    {
        
    }

    private IEnumerator AttackCooldown(int cooldownTime)
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        _canAttack = true;
    }
}
