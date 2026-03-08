using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactory enemyFactory;

    private float topRowY = 4f;
    private float middleRowY = 2.5f;
    private float bottomRowY = 1f;

    private int enemiesPerRow = 3;
    private float spacingX = 2f;

    private int currentWave = 0;
    private int enemyCount = 0;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if (enemyCount == 0 && currentWave > 0 && currentWave < 3)
        {
            SpawnWave();
        }
        else if (enemyCount == 0 && currentWave == 3)
        {
            Debug.Log("All waves cleared");
        }
    }

    void SpawnWave()
    {
        currentWave++;

        for (int i = 0; i < enemiesPerRow; i++)
        {
            float xPos = i * spacingX;

            enemyFactory.CreateEnemy(EnemyTypes.Basic, new Vector3(xPos, topRowY, 0));
            enemyFactory.CreateEnemy(EnemyTypes.Fast, new Vector3(xPos, middleRowY, 0));
            enemyFactory.CreateEnemy(EnemyTypes.Tank, new Vector3(xPos, bottomRowY, 0));

            enemyCount += 3;
        }

        enemiesPerRow += 2;
    }
}