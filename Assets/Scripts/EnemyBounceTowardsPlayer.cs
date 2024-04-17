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
    private bool canJump = true;
    private AudioSource jumpSource;

    void Start()
    {
        jumpSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius && canJump)
        {
            RotateTowardsPlayer();
            BounceTowardsPlayer();
        }
        if (IsSpinning())
        {
            rb.AddTorque(-rb.angularVelocity, ForceMode.Impulse);
        }

    }
    private bool IsSpinning()
    {
        // Check if the angular velocity of the slime is non-zero, indicating rotational forces are acting on it
        return rb.angularVelocity.magnitude > 0.01f;
    }

    private void BounceTowardsPlayer()
    {
        if (playerTransform == null)
            return;

        RotateTowardsPlayer();

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Neutralize y component for horizontal direction

        // Apply impulse force only if the slime is grounded
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * bounceHeight + direction * bounceSpeed, ForceMode.Impulse);
            jumpSource.Play();
            canJump = false;
            Invoke("EnableJump", waitTimeBeforeNextJump);
        }
    }

    private void RotateTowardsPlayer()
    {
        if (playerTransform == null)
            return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    private void EnableJump()
    {
        canJump = true;
    }

    private bool IsGrounded()
    {
        // Cast a ray downwards to check if the slime is grounded
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the slime collides with an object tagged as "Ground", reset canJump and cancel its velocity
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            // Cancel out the slime's velocity upon collision with the ground
            rb.velocity = Vector3.zero;
        }
    }
}
