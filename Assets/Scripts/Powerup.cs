using UnityEngine;

public enum PowerupType{
    Damage,
    FireRate,
    MultiShot,
    Laser,
    AoE
}

public class Powerup : MonoBehaviour
{
    public PowerupType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShipShooting playerShooting = other.GetComponent<ShipShooting>();
        if(playerShooting != null)
        {
            playerShooting.ApplyPowerup(type);
            Destroy(gameObject);
        }
    }
}