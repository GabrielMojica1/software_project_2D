using UnityEngine;
using Events;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;
    public float moveSpeed = 1.5f;
    public float reloadSpeed = 1f;

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

        EventBus.Publish(new DamageDealtEvent(damage));
    }

    public void Die()
    {
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
        EventBus.Publish(new EnemyKilledEvent());

    }
}