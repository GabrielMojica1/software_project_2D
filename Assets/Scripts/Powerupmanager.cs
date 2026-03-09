using UnityEngine;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager instance;

    public List<PowerupType> availablePowerups;

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

    public PowerupType GetRandomPowerupType()
    {
        if(availablePowerups.Count == 0)
        {
            Debug.Log("no avaialable powerups");
            return PowerupType.Damage;
        }

        int randomIndex = Random.Range(0, availablePowerups.Count);

        return availablePowerups[randomIndex];
    }
}