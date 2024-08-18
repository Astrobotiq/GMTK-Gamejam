using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> throwableObjects; // List of throwable objects in the level
    public List<Transform> spawnPoints; // List of spawn points for meteors
    public MeteorManager meteorManager; // Reference to the MeteorManager
    public GameObject player; // Reference to the player
    public GameObject levelEndTrigger; // Trigger box for level completion

    private bool levelCompleted = false;

    void Start()
    {
        InitializeLevel();
    }

    void Update()
    {
        if (!levelCompleted)
        {
            CheckForLevelCompletion();
        }
    }

    void InitializeLevel()
    {
        // Spawn initial meteors or other objects as needed
        
    }

    void CheckForLevelCompletion()
    {
        if (Vector2.Distance(player.transform.position, levelEndTrigger.transform.position) < 0.5f)
        {
            levelCompleted = true;
            Debug.Log("Level Completed!");
            NotifyGameManagerLevelCompleted();
        }
    }

    void NotifyGameManagerLevelCompleted()
    {
        GameManager.Instance.OnLevelCompleted();
    }

    public void StartLevel()
    {
        levelCompleted = false;
        InitializeLevel();
    }
}