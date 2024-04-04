using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFallenEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object does not have the tag "Player"
        if (!other.CompareTag("Player"))
        {
            // Deactivate the other object
            other.gameObject.SetActive(false);
        }
    }
}
