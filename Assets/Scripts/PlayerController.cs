using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5.0f;
    public float gravity = 9.81f;
    public float jumpHeight = 2.0f;
    public Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float dashCooldown = 3f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f; 
    public Image cooldownFillImage; 
    public TextMeshProUGUI cooldownText;
    private Vector3 dashDirection;
    private bool isDashing = false;
    private float dashEndTime = 0f;
    private float nextDashTime = 0f;
    bool isGrounded = false;
    private Rigidbody rb;
    private bool running = false;
    private float speedStartTime;
    private AudioSource dashSource;
    public AudioClip dashSound;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        dashSource = GameObject.FindGameObjectWithTag("SpellAudioSource").GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!PlayerHealth.isDead)
        {
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
                isDashing = true;
                dashDirection = Vector3.zero;

                if (Input.GetKey(KeyCode.W))
                {
                    dashDirection += transform.forward;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    dashDirection -= transform.forward;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    dashDirection -= transform.right;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    dashDirection += transform.right;
                }

                if (dashDirection != Vector3.zero)
                {
                    dashDirection.Normalize();
                }
                if (dashDirection == Vector3.zero)
                {
                    dashDirection = transform.forward;
                }

                dashEndTime = Time.time + dashDuration;
                nextDashTime = Time.time + dashCooldown + dashDuration;
                dashSource.clip = dashSound;
                dashSource.Play();
            }

            if (isDashing)
            {
                Dash();
            }

            void Dash()
            {
                if (Time.time >= dashEndTime)
                {
                    isDashing = false;
                    return;
                }

                controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            }


            Vector3 move = (transform.right * x + transform.forward * z).normalized;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

             CheckSpeed();

            UpdateCooldownUI();
        }
     
    }

    void UpdateCooldownUI()
    {
        float cooldownLeft = nextDashTime - Time.time;
        bool isCooldownActive = cooldownLeft > 0;

        
        cooldownFillImage.fillAmount = isCooldownActive ? cooldownLeft / dashCooldown : 0;

        
        cooldownText.text = isCooldownActive ? cooldownLeft.ToString("F1") : "Dash";

    
        cooldownFillImage.color = isCooldownActive ? Color.red : Color.green;

        if (!isCooldownActive){
            cooldownFillImage.fillAmount = 100;
        }



    }

    public void MoveCharacter(Vector3 direction){
        controller.Move(direction);

    }

    public void Leap(){
     float verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);


    }

    public void speedAbility()
    {
        speedStartTime = Time.time;
        speed = 6.0f;
        running = true;
    }


    void CheckSpeed()
    {
        if (running && Time.time - speedStartTime >= 3.0f)
        {
            speed = 3.0f;
            running = false;
        }
    }






}
