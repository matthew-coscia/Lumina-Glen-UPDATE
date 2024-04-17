using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealth : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth = 100;
    public int health;
    void Start()
    {
        healthBar.value = maxHealth;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("Boss is Hit!");
            TakeDamage(3);
        }
        collision.gameObject.SetActive(false);
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }
    void Die()
    {
        healthBar.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
