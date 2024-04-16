using System.Collections;
using UnityEngine;

public class EnemyBounceTowardsPlayer : MonoBehaviour
{
    public float bounceSpeed = 5f;
    public float bounceHeight = 2f;
    public float detectionRadius = 10f;
    public float rotationSpeed = 5f;
    public float waitTimeBeforeNextJump = 2f; // Time to wait before jumping again

    private Rigidbody rb;
    private Transform playerTransform;
    private bool isJumping = false;
    private bool canJump = true;
    private AudioSource jumpSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        jumpSource = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        StartCoroutine(JumpAndRotateRoutine());
    }

    IEnumerator JumpAndRotateRoutine()
    {
        while (true)
        {
            if (playerTransform != null && IsPlayerInRange() && !isJumping && canJump)
            {
                isJumping = true;
                BounceTowardsPlayer();
                yield return new WaitForSeconds(waitTimeBeforeNextJump);
                jumpSource.Play();
                isJumping = false;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (rb.velocity.y == 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }


    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius;
    }

    private void BounceTowardsPlayer()
    {
        RotateTowardsPlayerImmediately();

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Neutralize y component for horizontal direction
        rb.AddForce(Vector3.up * bounceHeight + direction * bounceSpeed, ForceMode.Impulse);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void RotateTowardsPlayerImmediately()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
