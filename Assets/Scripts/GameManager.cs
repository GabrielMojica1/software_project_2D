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
        State = newState;

        // TODO: add & finalize other logic
        switch (newState)
        {
            case GameState.Menu:
                SceneManager.LoadScene("MainMenu");
                break;
            case GameState.Playing:
                //replace with actual scene name when we have one
                SceneManager.LoadScene("SampleScene");
                break;
            case GameState.Paused:

                break;
            case GameState.GameOver:

                break;
            case GameState.LevelComplete:

                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
    }
}
