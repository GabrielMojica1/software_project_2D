using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyFactory enemyFactory;

    void Start()
    {
        SpawnWave();
    }

    void SpawnWave()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 position = new Vector3(i * 2, 5, 0);
            enemyFactory.CreateEnemy(EnemyTypes.Tank, position);
        }
    }
}