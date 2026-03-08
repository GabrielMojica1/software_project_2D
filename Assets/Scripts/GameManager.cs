using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        LevelComplete
    }

    public GameState State { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetState(GameState.Menu);
    }

    public void SetState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                
                break;
            case GameState.Playing:
                // Start the game, load level, etc.
                break;
            case GameState.Paused:
                // Pause the game, show pause menu, etc.
                break;
            case GameState.GameOver:
                // Show game over screen, reset game, etc.
                break;
            case GameState.LevelComplete:
                // Show level complete screen, load next level, etc.
                break;
        }
    }

    // TODO: add methods for starting game, pausing, resuming, etc.

}
