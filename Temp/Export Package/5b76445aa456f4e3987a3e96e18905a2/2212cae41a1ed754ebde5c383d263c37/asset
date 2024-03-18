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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

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
            if (playerTransform != null && IsPlayerInRange() && !isJumping)
            {
                isJumping = true; // Prevent multiple jumps at the same time
                BounceTowardsPlayer();
                yield return new WaitUntil(() => CheckIfGrounded()); // Wait until the enemy is grounded
                yield return new WaitForSeconds(waitTimeBeforeNextJump); // Wait for specified time before next jump
                isJumping = false;
            }
            yield return null;
        }
    }

    bool CheckIfGrounded()
    {
        Vector3 spherePosition = transform.position + Vector3.down * 0.5f;
        float checkRadius = 0.5f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        return Physics.CheckSphere(spherePosition, checkRadius, groundLayer);
    }

    void FixedUpdate()
    {
        if (playerTransform != null && isJumping)
        {
            RotateTowardsPlayer();
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius;
    }

    private void BounceTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0;
        rb.AddForce(Vector3.up * bounceHeight, ForceMode.Impulse);
        rb.AddForce(direction * bounceSpeed, ForceMode.Impulse);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
