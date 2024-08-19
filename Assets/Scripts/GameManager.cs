using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelManager levelManager;
    public OxygenManager oxygenManager;

    private int currentLevel = 0;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        SceneManager.LoadSceneAsync(level);
        
        
    }

    public void OnLevelCompleted()
    {
        if (currentLevel < SceneManager.sceneCount )
        {
            currentLevel++;
            Debug.Log("Starting next level: " + currentLevel);
            StartLevel(currentLevel);
        }
        else
        {
            Debug.Log("You Win!");
            // Handle win condition, maybe trigger end game UI or transition to a win scene
        }
    }

    public void OnGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        // Handle game over logic, such as restarting the game or showing a game over screen
    }

    void Update()
    {
        if (!isGameOver)
        {
            CheckForGameOver();
        }
    }

    void CheckForGameOver()
    {
        if (oxygenManager.GetOxygenLevel() <= 0)
        {
            OnGameOver();
        }
    }
}