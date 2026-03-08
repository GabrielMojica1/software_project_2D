using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        SelfDestroy();
    }

    private void SelfDestroy()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ship ship = other.GetComponent<Ship>();

        if (ship != null)
        {
            ship.TakeDamage();
            Destroy(gameObject);
        }
    }
}