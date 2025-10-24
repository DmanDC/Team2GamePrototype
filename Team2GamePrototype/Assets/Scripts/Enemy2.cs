using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        
        Destroy(gameObject);
    }

    [SerializeField] EnemyHealthbar healthBar;
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthbar>();
    }
}
