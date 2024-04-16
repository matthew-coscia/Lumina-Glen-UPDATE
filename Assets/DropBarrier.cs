using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBarrier : MonoBehaviour
{

    public GameObject player;
    public AudioClip soundEffect; 

    public GameObject barrier; 
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
       
            if (barrier != null)
            {
                barrier.SetActive(false);
                AudioSource.PlayClipAtPoint(soundEffect, player.transform.position, 3);

            }
            
        
    }
}
