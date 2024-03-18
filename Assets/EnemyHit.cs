using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject enemyExpelled;
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

    void DestroyEnemy() 
    {
        Instantiate(enemyExpelled, transform.position, transform.rotation);
        gameObject.SetActive(false);
        // Instantiate(lootPrefab, transform.position, transform.rotation);
        // LevelManager.Instance.EnemyKilled();
        Destroy(gameObject, 0.5f);
    }
}
