using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5;
    public float minDistance = 2; // minimum distance to stop moving towards the player
    public float detectionRange = 10f; // max distance at which the enemy can detect the player 
    public int damageAmount = 5;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            // Find the player by tag. 
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {


        // distance between enemy position and player position
        float distance = Vector3.Distance(transform.position, player.position);


        // Only move towards the player if they are within detection range and further than min Distance
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
            // apply damage
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
            // Apply damage continuously
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

}
