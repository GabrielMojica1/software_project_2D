using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

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
        SetTimeScale(1f); // Reset time scale on state change
        EventBus.Publish(new Events.GameStateChangedEvent(newState));

        // TODO: add & finalize other logic
        switch (newState)
        {
            case GameState.Menu:
                SceneManager.LoadScene("MainMenu");
                break;
            case GameState.Playing:
                // no additional logic for now
                break;
            case GameState.Paused:
                SetTimeScale(0f);
                break;
            case GameState.GameOver:
                SetTimeScale(0f);
                SceneManager.LoadScene("Assets/Scenes/GameOverScene.unity");
                break;
            case GameState.LevelComplete:

                break;
        }
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
        //replace with actual scene name when we have one
        SceneManager.LoadScene("SampleScene");
    }

    private static void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void PauseGame()
    {
        if (State == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (State == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
    }
}
