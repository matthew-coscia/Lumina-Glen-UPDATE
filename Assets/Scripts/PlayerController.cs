using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If on the ground and falling, reset fall velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to ensure the character stays grounded
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); // Apply gravity
    }

}
