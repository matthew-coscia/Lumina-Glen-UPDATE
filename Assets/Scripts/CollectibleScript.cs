using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class CollectibleScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemiesExistPanel;
    public GameObject enemiesDontExistPanel;
    public GameObject youwin;
    public string nextLevel; 
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
                    enemiesDontExistPanel.SetActive(false);
                    CollectGem();
                    youwin.SetActive(true);
                    Invoke("LoadNextLevel", 5);
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
        return GameObject.FindGameObjectsWithTag("Enemy").Length < 3;
    }

    private void CollectGem()
    {
        enemiesDontExistPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    void LoadNextLevel(){

    SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel(){
        
       SceneManager.LoadScene(SceneManager.GetActiveScene().name); 

    }
}
