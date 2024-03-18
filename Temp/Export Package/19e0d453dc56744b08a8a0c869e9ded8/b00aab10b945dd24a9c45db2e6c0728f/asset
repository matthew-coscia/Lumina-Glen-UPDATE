using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f; // Default health

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle the enemy's death here (e.g., play animation, remove from scene)
        Destroy(gameObject); // For simplicity, just destroy the enemy object
    }
}
