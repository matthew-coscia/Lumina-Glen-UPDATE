using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5.0f;
    public float gravity = 9.81f;
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float dashCooldown = 3f;
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.2f; // How long the dash will last
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashEndTime = 0f;
    private float nextDashTime = 0f;
    bool isGrounded = false;
    private Rigidbody rb; // The Rigidbody component attached to the player


    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // Check if on the ground
        if (velocity.y <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (isGrounded) 
        {
            if (Input.GetButton("Jump"))
            {
                Debug.Log("Jump button pressed");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= nextDashTime)
        {
             // Start dashing
            isDashing = true;
            dashDirection = transform.forward;
            dashEndTime = Time.time + dashDuration;
            nextDashTime = Time.time + dashCooldown + dashDuration;
        }

        if (isDashing)
        {
            Dash();
        }

         void Dash()
    {
        // Check if the dash duration has ended
        if (Time.time >= dashEndTime)
        {
            isDashing = false;
            return;
        }

        // Move the player in the dash direction
        controller.Move(dashDirection * dashSpeed * Time.deltaTime);
    }


        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); // Apply gravity
    }

}
