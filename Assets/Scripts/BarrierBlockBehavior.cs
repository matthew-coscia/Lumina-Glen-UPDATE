using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBlock : MonoBehaviour
{
    public GameObject textToDisplay;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has collided!");
            textToDisplay.SetActive(true);

        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("the player has exit!");
            textToDisplay.SetActive(false);

        }
    }
}
