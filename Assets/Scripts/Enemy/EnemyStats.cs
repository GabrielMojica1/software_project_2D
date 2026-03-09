using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;
    public float moveSpeed = 1.5f;
    public float reloadSpeed = 1f;

    public float powerupDropChance = 1.1f;
    public int currentHealth;
    public EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(Random.value <= powerupDropChance)
        {
            ShipShooting playerShooting = FindObjectOfType<ShipShooting>();

            if(playerShooting != null && PowerupManager.instance != null)
            {
                PowerupType randomType = PowerupManager.instance.GetRandomPowerupType();
                playerShooting.ApplyPowerup(randomType);
            }
        }

        if (spawner != null)
        {
            spawner.enemyCount--;
        }

        if (Application.isPlaying)
        {
            Destroy(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        //This is so that the test cases dont complain ^^

        //From here we can add the +1 to the stat page (?)
    }
}