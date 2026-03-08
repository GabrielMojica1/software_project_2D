using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float shootTimer;

    void Start()
    {
        shootTimer = Random.Range(1f, 3f);
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot();
            EnemyStats stats = GetComponent<EnemyStats>();
            shootTimer = stats.reloadSpeed;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}