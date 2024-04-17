using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAOE : MonoBehaviour
{
    public float radius = 5f;
    public int damage = 100;

    private void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.radius = radius;

        Invoke("DetectAndDamageEnemies", 0.1f); 
    }

    void DetectAndDamageEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) 
            {
                hitCollider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
