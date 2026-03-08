using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}