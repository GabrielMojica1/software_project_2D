using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPrefab;

    private GameObject _pauseMenuInstance;

    void Start()
    {
        var existingPauseMenu = GameObject.Find("PauseMenu");
        if (existingPauseMenu != null)
        {
            existingPauseMenu.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<Events.GameStateChangedEvent>(OnGameStateChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<Events.GameStateChangedEvent>(OnGameStateChanged);
        DestroyPauseMenuInstance();
    }

    private void OnGameStateChanged(Events.GameStateChangedEvent e)
    {
        if (e.NewState == GameManager.GameState.Paused)
        {
            CreatePauseMenuIfNeeded();
            UpdatePauseMenuStatsText();
        }
        else
        {
            DestroyPauseMenuInstance();
        }
    }

    private void CreatePauseMenuIfNeeded()
    {
        if (_pauseMenuInstance != null)
        {
            return;
        }

        _pauseMenuInstance = Instantiate(_pauseMenuPrefab);
        _pauseMenuInstance.name = "PauseMenu";
    }

    private void UpdatePauseMenuStatsText()
    {
        if (_pauseMenuInstance == null)
        {
            return;
        }

        Text statsText = _pauseMenuInstance.GetComponentInChildren<Text>(true);
        if (statsText == null)
        {
            Debug.LogWarning("PauseManager: No legacy UI Text component found in pause menu prefab.");
            return;
        }

        var stats = StatsTracker.Instance;
        if (stats == null)
        {
            statsText.text = "Paused\n\nStats unavailable";
            return;
        }

        statsText.text =
            "Paused\n\n" +
            "Enemies Killed: " + stats.enemiesKilled + "\n" +
            "Damage Dealt: " + stats.damageDealt + "\n" +
            "Items Collected: " + stats.itemsCollected + "\n" +
            "Levels Completed: " + stats.levelsCompleted;
    }

    private void DestroyPauseMenuInstance()
    {
        if (_pauseMenuInstance != null)
        {
            Destroy(_pauseMenuInstance);
            _pauseMenuInstance = null;
        }
    }
}
