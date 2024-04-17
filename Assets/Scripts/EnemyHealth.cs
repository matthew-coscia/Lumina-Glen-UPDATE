using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    void Update(){
        if(gameObject.transform.position.y < -30){
            health = 0; 
            TakeDamage(100);
        }


    }

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
        Destroy(gameObject);
    }
}
