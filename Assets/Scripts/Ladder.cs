using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Optional: Visual or gameplay feedback when the player can climb

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            other.GetComponent<PlayerClimbing>().IsClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerClimbing>().IsClimbing = false;
        }
    }
}
