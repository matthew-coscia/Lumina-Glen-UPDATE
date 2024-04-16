using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTurretBehavior : MonoBehaviour
{
    public Transform player;
    public float range = 10f;
    public float speed = 10f;
    public float cooldown = .1f;
    public GameObject projectilePrefab;
    float shootingTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 180f);

            if (directionToPlayer.magnitude <= range)
            {
                shootingTimer -= Time.deltaTime;
                if (shootingTimer <= 0f)
                {
                    ShootProjectile();
                    shootingTimer = cooldown;
                }
            }
        }
    }

    void ShootProjectile()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 0.5f + new Vector3(0, 0.2f, 0);

        Vector3 playerPosition = player.position + new Vector3(0, 0.5f, 0);
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Vector3 directionToPlayer = (playerPosition - spawnPosition).normalized;
        projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * speed;
    }
}

