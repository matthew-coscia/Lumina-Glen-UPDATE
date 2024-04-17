using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHit : MonoBehaviour
{
    public GameObject enemyExpelled;

    public GameObject lootPrefab;
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
        if (other.CompareTag("Fireball"))
        {
            Debug.Log("The player hit me!");
            DestroyEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy() 
    {
        Instantiate(enemyExpelled, transform.position, transform.rotation);
        gameObject.SetActive(false);
        if(Random.Range(0, 3) == 2)
        {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject, 0.5f);
    }
}
