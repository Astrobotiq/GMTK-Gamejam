using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    public GameObject rotatingMeteorPrefab; // Prefab for rotating meteor
    public GameObject movingMeteorPrefab; // Prefab for moving meteor
    public GameObject spinningMeteorPrefab; // Prefab for spinning meteor
    public Transform[] spawnPoints; // Array of spawn points
    public float spawnInterval = 2f; // Time between spawns in seconds

    private Dictionary<Transform, int> lastSpawnedType = new Dictionary<Transform, int>(); // Track last spawned type per spawn point
    private GameObject currentSpinningMeteor = null; // Track the current spinning meteor

    private void Start()
    {
        // Initialize the dictionary
        foreach (Transform spawnPoint in spawnPoints)
        {
            lastSpawnedType[spawnPoint] = -1; // -1 indicates that no meteor has been spawned yet
        }

        StartCoroutine(SpawnMeteors());
    }

    private IEnumerator SpawnMeteors()
    {
        while (true)
        {
            SpawnMeteor();
            yield return new WaitForSeconds(spawnInterval); // Adjust spawn interval here
        }
    }

    private void SpawnMeteor()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Determine the type of meteor to spawn
        int lastType = lastSpawnedType[spawnPoint];
        int spawnType;
        do
        {
            spawnType = Random.Range(0, 3); // Randomly choose between 0, 1, or 2
        } while (spawnType == lastType || (spawnType == 2 && currentSpinningMeteor != null)); 
        // Ensure we do not spawn the same type consecutively or another spinning meteor if one exists

        // Update the dictionary with the type of meteor just spawned
        lastSpawnedType[spawnPoint] = spawnType;

        // Spawn the meteor based on type
        GameObject meteorPrefab = null;
        switch (spawnType)
        {
            case 0:
                Debug.Log("Spawning Rotating Meteor");
                meteorPrefab = rotatingMeteorPrefab;
                break;
            case 1:
                Debug.Log("Spawning Moving Meteor");
                meteorPrefab = movingMeteorPrefab;
                break;
            case 2:
                Debug.Log("Spawning Spinning Meteor");
                meteorPrefab = spinningMeteorPrefab;
                currentSpinningMeteor = Instantiate(meteorPrefab, spawnPoint.position, Quaternion.identity);
                return; // Exit the function to prevent further spawning in this frame
        }

        if (meteorPrefab != null)
        {
            Instantiate(meteorPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    // Method to clear the reference to the spinning meteor when it is destroyed
    public void OnSpinningMeteorDestroyed()
    {
        currentSpinningMeteor = null;
    }
}
