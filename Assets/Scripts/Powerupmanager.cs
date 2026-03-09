using UnityEngine;
using System.Collections.Generic;

public class Powerupmanager : MonoBehaviour
{
    public static Powerupmanager instance;

    public List<GameObject> powerupPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPowerup(Vector3 spawnPosition)
    {
        if(powerupPrefabs.count == 0)
        {
            Debug.Log("powerup prefabs list empty");
            return;
        }

        int randomIndex = Random.Range(0, powerupPrefabs.Count);

        GameObject randomPowerupPrefab = powerupPrefabs[randomIndex];

        Instantiate(randomPowerupPrefab, spawnPosition, Quaternion.identity);
    }
}