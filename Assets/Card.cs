using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public Sprite cardSprite;
    public GameObject slamVFX;
    public GameObject slamAOEPrefab;
  //  public LayerMask groundLayer;
   // public Transform groundCheck;
    private float checkGroundRadius = 0.5f;
    bool SlammingDown;
    private Vector3 jumpVelocity;
    private bool isJumping;
    public float jumpSpeed = 10f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;
    public AudioClip healSound;
    public AudioClip slamSound;
    public AudioClip sprintSound;
    private AudioSource spellAudioSource;


    private void Start()
    {
       // groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        SlammingDown = false;
        spellAudioSource = GameObject.FindGameObjectWithTag("SpellAudioSource").GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Debug.Log("Slamming Down: "+ SlammingDown);
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"

        if (SlammingDown)
        {
            Debug.Log("Velocity: " + player.GetComponent<PlayerController>().velocity.y);

            //   GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"

            if (player.GetComponent<PlayerController>().velocity.y <= 0)
            {
                GameObject slamEffect = Instantiate(slamAOEPrefab, player.transform.position, Quaternion.identity);
                Instantiate(slamVFX, player.transform.position, Quaternion.identity);
                SlammingDown = false;
            }


        }
        if (isJumping)
        {
            Debug.Log("Jumping");
            PerformLeapAbility();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupCard(other.gameObject);
        }
    }

    void PickupCard(GameObject player)
    {
        InventoryManager inventoryManager = player.GetComponent<InventoryManager>();
        if (inventoryManager != null && inventoryManager.AddCard(this))
        {
            Debug.Log("Card Added");
            gameObject.SetActive(false); // Remove the card from the game world
        }
    }

    public void UseAbility()
    {

        Debug.Log("Ability Used: " + cardName);
        switch (cardName)
        {
            case "Slam":
                PerformSlamAbility();
                break;
            case "Heal":
                PerformHealAbility();
                break;
            case "Leap":
                PerformLeapAbility();
                break;
            default:
                Debug.Log("Unknown card ability.");
                break;
        }
    }

    void PerformSlamAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("Slam Performed IMPORTANT");
            // Move the player down quickly to simulate a "slam" - this is just a placeholder for actual slam logic
            //player.transform.Translate(Vector3.down * 5f, Space.World); // Example movement, adjust as needed
            player.GetComponent<PlayerController>().MoveCharacter(new Vector3(0f, -1f, 0f) * 3f);

            // Create the damage effect radius
            SlammingDown = true;
            spellAudioSource.clip = slamSound;
            spellAudioSource.Play();
        }
    }
    
    /*
    bool IsGrounded()
    {
        // Check if player's groundCheck transform is close enough to the ground
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, checkGroundRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
    */

    void PerformLeapAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController controller = player.GetComponent<CharacterController>();
        player.GetComponent<PlayerController>().speedAbility();
        spellAudioSource.clip = sprintSound;
        spellAudioSource.Play();


    }

    void PerformHealAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth PlayerHealthVar = player.GetComponent<PlayerHealth>();
        PlayerHealthVar.health = 10;
        PlayerHealthVar.UpdateHealthBar();
        spellAudioSource.clip = healSound;
        spellAudioSource.Play();
    }

}
