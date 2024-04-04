using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGuardBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3;
    public float minDistance = 2;
    public int damageAmount = 20;

    public enum FSMStates
    {
        Patrol,
        Chase,
        Attack
    }

    public FSMStates currentState;

    public float attackDistance = 20;
    public float chaseDistance = 30;
    public float shootingTimer = 1;

    GameObject[] wanderPoints;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    float distanceToPlayer;
    public GameObject spellProjectile;
    float elapseTime = 0;

    public float fieldOfView = 45f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }

        elapseTime += Time.deltaTime;
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if (Vector3.Angle(directionToPlayer, transform.forward) <= fieldOfView)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    void UpdatePatrolState()
    {
        if (Vector3.Distance(transform.position, nextDestination) < 2)
        {
            FindNextPoint();

        }
        else if (IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }


        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
    }

    void UpdateChaseState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        EnemySpellCast();
    }

    void EnemySpellCast()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            ShootProjectile();
            shootingTimer = 1;
        }
    }

    void ShootProjectile()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 0.5f + new Vector3(0, 0.2f, 0);

        Vector3 playerPosition = player.position + new Vector3(0, 0.5f, 0);
        GameObject projectile = Instantiate(spellProjectile, spawnPosition, Quaternion.identity);
        Vector3 directionToPlayer = (playerPosition - spawnPosition).normalized;
        projectile.GetComponent<Rigidbody>().velocity = directionToPlayer * 8;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
