using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public float minDistance = 2; 
    public float detectionRange = 10f; 
    public int damageAmount = 5;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange && distance > minDistance)
        {
            MoveTowardsPlayer();
        }

    }

    private void MoveTowardsPlayer() {
        float step = moveSpeed * Time.deltaTime;
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy got triggered!");
            var PlayerHealth = other.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                      PlayerHealth.TakeDamage(damageAmount);

            }
    


        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

}
