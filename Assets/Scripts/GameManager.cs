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
        State = GameState.Menu;
    }

    public void SetState(GameState newState)
    {
        State = newState;
        // Handle state change logic here
    }

    // TODO: add methods for starting game, pausing, resuming, etc.
    
}
