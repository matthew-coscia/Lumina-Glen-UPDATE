using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int maxEnemies = 10;
    public float spawnRadius = 1.0f;
    public float spawnInterval = 2.0f;

    private GameObject[] spawnPoints;
    private int currentEnemyCount = 0;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn"); // Find all spawn points in the scene
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);

            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPos = spawnPoint.transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = spawnPoint.transform.position.y;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemyCount++;
        }
    }

    // Optionally, add a method to decrement the enemy count when an enemy is defeated
    public void EnemyDefeated()
    {
        if (currentEnemyCount > 0)
            currentEnemyCount--;
    }
}
