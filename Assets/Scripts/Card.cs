using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName;
    public Sprite cardSprite;
    public GameObject slamVFX;
    public GameObject slamAOEPrefab;
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
        SlammingDown = false;
        spellAudioSource = GameObject.FindGameObjectWithTag("SpellAudioSource").GetComponent<AudioSource>();
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (SlammingDown)
        {


            if (player.GetComponent<PlayerController>().velocity.y <= 0)
            {
                GameObject slamEffect = Instantiate(slamAOEPrefab, player.transform.position, Quaternion.identity);
                Instantiate(slamVFX, player.transform.position, Quaternion.identity);
                SlammingDown = false;
            }


        }
        if (isJumping)
        {
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
            gameObject.SetActive(false); 
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
             case "Ammo":
                performAmmoAbility();
                break;
            default:
                break;
        }
    }

    void PerformSlamAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().MoveCharacter(new Vector3(0f, -1f, 0f) * 3f);

            Invoke("spawnSlamEffects", .01f);
            spellAudioSource.clip = slamSound;
            spellAudioSource.Play();
        }
    }

    void spawnSlamEffects(){
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject slamEffect = Instantiate(slamAOEPrefab, player.transform.position, Quaternion.identity);
                Instantiate(slamVFX, player.transform.position, Quaternion.identity);
    }
    

    void PerformLeapAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController controller = player.GetComponent<CharacterController>();
        player.GetComponent<PlayerController>().speedAbility();
        spellAudioSource.clip = sprintSound;
        spellAudioSource.Play();
    }

    void performAmmoAbility(){
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        SpellShooter spellShooter = mainCam.GetComponent<SpellShooter>();

        spellShooter.ammoAbility();
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
