using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> throwableObjects; // List of throwable objects in the level
    public List<Transform> spawnPoints; // List of spawn points for meteors
    public MeteorManager meteorManager; // Reference to the MeteorManager
    

    

    void Start()
    {
        InitializeLevel();
    }

    void InitializeLevel()
    {
        // Spawn initial meteors or other objects as needed
        
    }
    
    public void CompleteLevel()
    {
        NotifyGameManagerLevelCompleted();
    }

    void NotifyGameManagerLevelCompleted()
    {
        Debug.Log("Level is finished.");
        //GameManager.Instance.OnLevelCompleted();
    }

    public void StartLevel()
    {
        InitializeLevel();
    }
}