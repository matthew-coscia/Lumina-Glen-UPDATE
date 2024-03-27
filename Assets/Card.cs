using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName; // Name of the card, set this in the Inspector
    public Sprite cardSprite; // The sprite to show in the UI when picked up, set this in the Inspector
    public GameObject slamVFX; 
   public GameObject slamAOEPrefab;
   public LayerMask groundLayer; // Assign this in the editor to match your ground layer
    public Transform groundCheck; // Position this at the bottom of the player in the editor
        private float checkGroundRadius = 0.5f; // Adjust as needed for your game
   bool SlammingDown; 
   

  private void Start(){
    groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
    SlammingDown = false; 
   }

   private void Update()
    {
        Debug.Log("Slamming Down: "+ SlammingDown);
                        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"

        if (SlammingDown) {
                   Debug.Log("Velocity: " + player.GetComponent<PlayerController>().velocity.y);

             //   GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"
             
             if (player.GetComponent<PlayerController>().velocity.y <= 0){
            GameObject slamEffect = Instantiate(slamAOEPrefab, player.transform.position, Quaternion.identity);
             Instantiate(slamVFX, player.transform.position, Quaternion.identity);
             SlammingDown = false; 
             }
        
       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player GameObject has the tag "Player"
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
            // HealPlayer();
            break;
        case "Explode":
            // Explode();
            break;
        default:
            Debug.Log("Unknown card ability.");
            break;
    }
}

void PerformSlamAbility()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"
    if (player != null){
        Debug.Log("Slam Performed IMPORTANT");
        // Move the player down quickly to simulate a "slam" - this is just a placeholder for actual slam logic
        //player.transform.Translate(Vector3.down * 5f, Space.World); // Example movement, adjust as needed
        player.GetComponent<PlayerController>().MoveCharacter(new Vector3(0f, -1f, 0f) * 3f);

        // Create the damage effect radius
        SlammingDown = true; 
    }
}

bool IsGrounded()
    {
        // Check if player's groundCheck transform is close enough to the ground
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, checkGroundRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject) // Make sure we don't detect the player's own collider
            {
                return true;
            }
        }
        return false;
    }



}
