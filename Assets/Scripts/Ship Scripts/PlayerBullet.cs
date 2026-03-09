using UnityEngine;
using System.Collections.Generic;
public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 8f;
    public int damage = 1;
    public IHitStrategy currentStrategy;

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        SelfDestroy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStats enemy = other.GetComponent<EnemyStats>();

        if (enemy != null)
        {
            if(currentStrategy.Execute(other, damage))
            {
                Destroy(gameObject);
            }
        }
    }

    void SelfDestroy()
    {
        if(transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
}