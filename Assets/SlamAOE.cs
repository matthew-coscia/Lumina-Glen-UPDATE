using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAOE : MonoBehaviour
{
    public float radius = 5f;
    public int damage = 100;

    private void Start()
    {
        // Optionally, expand or shrink the collider based on the desired radius
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = radius;

        // Detect enemies immediately or after a short delay
        Invoke("DetectAndDamageEnemies", 0.1f); // Adjust the delay as needed
    }

    void DetectAndDamageEnemies()
    {
        // Find all colliders within the radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            // Check if the collider belongs to an enemy
            if (hitCollider.CompareTag("Enemy")) // Make sure your enemies have the "Enemy" tag
            {
                // Apply damage to the enemy
                // This requires your enemies to have a script component that can receive damage
                hitCollider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }

        // Destroy the AoE effect GameObject after applying damage
        Destroy(gameObject);
    }
}
