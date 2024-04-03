using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIsland : MonoBehaviour
{
     public float speed = 3;
    public float distance = 5;

    private Vector3 startPos;
    private Vector3 previousPos;
    private Transform playerTransform;
    private CharacterController playerController;

    void Start()
    {
        startPos = transform.position;
        previousPos = transform.position;
    }

    void Update()
    {
        Vector3 newPos = startPos + new Vector3(Mathf.Sin(Time.time * speed) * distance, 0, 0);
        
        // Calculate the crate's movement vector since the last frame
        Vector3 movementDelta = newPos - previousPos;
        
        // Apply the crate's movement
        transform.position = newPos;
        previousPos = newPos; // Update previousPos for the next frame

        // If the player is standing on the crate, move them with it
        if (playerController && playerTransform)
        {
            // Check if the player is grounded and standing on this crate
            if (playerController.isGrounded && IsPlayerOnCrate())
            {
                playerController.Move(movementDelta);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger area
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the trigger area
        if (other.CompareTag("Player"))
        {
            playerTransform = null;
            playerController = null;
        }
    }

    private bool IsPlayerOnCrate()
    {
        // Check if the player's feet are within the bounds of the crate's physical collider
        Collider crateCollider = GetComponent<Collider>();
        if (crateCollider.bounds.Contains(playerTransform.position))
        {
            return true;
        }
        return false;
    }
}
