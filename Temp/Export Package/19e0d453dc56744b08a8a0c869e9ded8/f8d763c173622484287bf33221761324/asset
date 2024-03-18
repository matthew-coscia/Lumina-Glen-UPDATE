using UnityEngine;
using UnityEngine.UI; // Use this if you're working with Unity's UI system
// using TMPro; // Uncomment if you're using TextMeshPro

public class CollectibleScript : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the inspector
    public GameObject enemiesExistPanel; // Assign in the inspector
    public GameObject enemiesDontExistPanel; // Assign in the inspector
    public GameObject youwin;
    private bool playerInRange = false;

    private void Start()
    {
        enemiesExistPanel.SetActive(false);
        enemiesDontExistPanel.SetActive(false);
    }

    private void Update()
    {
        CheckPlayerDistance();

        if (playerInRange)
        {
            if (AreAllEnemiesDefeated())
            {
                enemiesExistPanel.SetActive(false);
                enemiesDontExistPanel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Logic to pick up the gem
                    enemiesDontExistPanel.SetActive(false);
                    CollectGem();
                    youwin.SetActive(true);
                }
            }
            else
            {
                enemiesExistPanel.SetActive(true);
                enemiesDontExistPanel.SetActive(false);
            }
        }
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z),
                             new Vector3(transform.position.x, 0, transform.position.z)) <= 5f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
            enemiesExistPanel.SetActive(false);
            enemiesDontExistPanel.SetActive(false);
        }
    }

    private bool AreAllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private void CollectGem()
    {
        enemiesDontExistPanel.SetActive(false);
        gameObject.SetActive(false);
    }
}
