using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;

    public GameObject CreateEnemy(EnemyTypes type, Vector3 spawnPosition)
    {
        GameObject prefab = null;

        if (type == EnemyTypes.Basic)
        {
            prefab = basicEnemyPrefab;
        }
        else if(type == EnemyTypes.Fast) 
        {
            prefab = fastEnemyPrefab;
        }
        else if(type == EnemyTypes.Tank)
        {
            prefab = tankEnemyPrefab;
        }


       GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

        return enemy;
    }
}