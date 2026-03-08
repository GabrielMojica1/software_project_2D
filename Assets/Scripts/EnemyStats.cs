using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 10;
    public float moveSpeed = 1.5f;
    public float reloadSpeed = 1f;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        //Not sure on movement yet
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
        Destroy(gameObject);
        //From here we can add the +1 to the stat page (?)
    }
}