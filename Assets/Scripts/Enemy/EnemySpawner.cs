using UnityEditorInternal;
using UnityEngine;
using Events;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactory enemyFactory;

    private float topRowY = 4f;
    private float middleRowY = 2.5f;
    private float bottomRowY = 1f;

    private int enemiesPerRow = 3;
    private float spacingX = 2f;

    private int currentWave = 0;
    public int enemyCount = 0;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if (enemyCount == 0 && currentWave > 0 && currentWave < 3)
        {
            SpawnWave();
            EventBus.Publish(new LevelCompletedEvent(currentWave));
        }
        else if (enemyCount == 0 && currentWave == 3)
        {
            Debug.Log("All waves cleared");
            EventBus.Publish(new GameCompletedEvent(0 /*stops compiler from complaining*/));
        }
    }

    void SpawnWave()
    {
        currentWave++;

        for (int i = 0; i < enemiesPerRow; i++)
        {
            float xPos = i * spacingX;

            GameObject enemy = enemyFactory.CreateEnemy(EnemyTypes.Basic, new Vector3(xPos, topRowY, 0));
            EnemyStats stats = enemy.GetComponent<EnemyStats>();
            stats.spawner = this;

            enemy = enemyFactory.CreateEnemy(EnemyTypes.Fast, new Vector3(xPos, middleRowY, 0));
            stats = enemy.GetComponent<EnemyStats>();
            stats.spawner = this;

            enemy = enemyFactory.CreateEnemy(EnemyTypes.Tank, new Vector3(xPos, bottomRowY, 0));
            stats = enemy.GetComponent<EnemyStats>();
            stats.spawner = this;

            enemyCount += 3;
        }

        enemiesPerRow += 2;
    }
}