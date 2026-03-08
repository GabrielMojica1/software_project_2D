using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        selfDestroy();
    }

    private void selfDestroy()
    {
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}