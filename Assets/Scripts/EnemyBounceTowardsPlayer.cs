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

    void Start()
    {
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
        if (canJump)
        {
            BounceTowardsPlayer();
        }
    }

    private void BounceTowardsPlayer()
    {
        if (playerTransform == null)
            return;

        RotateTowardsPlayer();

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Neutralize y component for horizontal direction
        rb.AddForce(Vector3.up * bounceHeight + direction * bounceSpeed, ForceMode.Impulse);

        canJump = false;
        Invoke("EnableJump", waitTimeBeforeNextJump);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
