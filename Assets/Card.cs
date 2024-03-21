using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardName; // Name of the card, set this in the Inspector
    public Sprite cardSprite; // The sprite to show in the UI when picked up, set this in the Inspector
    public GameObject slamVFX; 
   public GameObject slamAOEPrefab;

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
    if (player != null)
            Debug.Log("Slam Performed");

    {
        Debug.Log("Slam Performed");
        // Move the player down quickly to simulate a "slam" - this is just a placeholder for actual slam logic
        //player.transform.Translate(Vector3.down * 5f, Space.World); // Example movement, adjust as needed
                player.GetComponent<PlayerController>().MoveCharacter(new Vector3(0f, -1f, 0f) * 3f);

        // Create the damage effect radius

        GameObject slamEffect = Instantiate(slamAOEPrefab, player.transform.position, Quaternion.identity);


        // Add particle effects on slam location
        // This requires a Particle System to be set up in your project. Here we're instantiating a prefab.
        
        Instantiate(slamVFX, player.transform.position, Quaternion.identity);

    }
}
}
