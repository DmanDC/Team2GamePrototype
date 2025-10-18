using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public int damage = 20;
    public bool death = false;

    public HealthBar healthBar;
    private void Start()
    {
        // Try to auto-resolve if not set in Inspector (prefab case)
        if (healthBar == null)
        {
            // 1) if you made the bar a child of the Player prefab
            healthBar = GetComponentInChildren<HealthBar>(true);

            // 2) otherwise, find the HUD bar that already exists in the scene
            if (healthBar == null) healthBar = FindObjectOfType<HealthBar>(true);
        }


        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage();

        }
    }



    public void TakeDamage()
    {
       
         currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject, 0.2f);
        death = true;
        
    }

}
