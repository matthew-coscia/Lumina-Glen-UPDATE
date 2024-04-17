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

        Vector3 movementDelta = newPos - previousPos;

        transform.position = newPos;
        previousPos = newPos;

        if (playerController && playerTransform)
        {
            if (playerController.isGrounded && IsPlayerOnCrate())
            {
                playerController.Move(movementDelta);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null;
            playerController = null;
        }
    }

    private bool IsPlayerOnCrate()
    {
        Collider crateCollider = GetComponent<Collider>();
        if (crateCollider.bounds.Contains(playerTransform.position))
        {
            return true;
        }
        return false;
    }
}
