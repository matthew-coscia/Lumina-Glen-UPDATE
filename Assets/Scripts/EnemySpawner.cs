using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the inspector
    public int maxEnemies = 10; // Maximum number of enemies to spawn
    public float spawnRadius = 1.0f; // Radius around the spawn point to vary enemy spawn location
    public float spawnInterval = 2.0f; // Time between each spawn attempt

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

            // Choose a random spawn point
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPos = spawnPoint.transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = spawnPoint.transform.position.y; // Ensure the enemy spawns on the ground if your spawnPoints are at ground level

            // Spawn the enemy
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
