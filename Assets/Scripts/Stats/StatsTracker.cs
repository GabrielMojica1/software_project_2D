using UnityEngine;
using Events;

public class StatsTracker : MonoBehaviour
{
    public int enemiesKilled;
    public int damageDealt;
    public int itemsCollected;
    public int levelsCompleted;

    private void OnEnable()
    {
        EventBus.Subscribe<Events.EnemyKilledEvent>(OnEnemyKilled);
        EventBus.Subscribe<Events.DamageDealtEvent>(OnDamageDealt);
        EventBus.Subscribe<Events.ItemCollectedEvent>(OnItemCollected);
        EventBus.Subscribe<Events.LevelCompletedEvent>(OnLevelCompleted);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<Events.EnemyKilledEvent>(OnEnemyKilled);
        EventBus.Unsubscribe<Events.DamageDealtEvent>(OnDamageDealt);
        EventBus.Unsubscribe<Events.ItemCollectedEvent>(OnItemCollected);
        EventBus.Unsubscribe<Events.LevelCompletedEvent>(OnLevelCompleted);
    }

    void OnEnemyKilled(EnemyKilledEvent e)
    {
        enemiesKilled++;
    }

    void OnDamageDealt(DamageDealtEvent e)
    {
        damageDealt += e.Amount;
    }

    void OnItemCollected(ItemCollectedEvent e)
    {
        itemsCollected++;
    }

    void OnLevelCompleted(LevelCompletedEvent e)
    {
        levelsCompleted++;
    }
}
