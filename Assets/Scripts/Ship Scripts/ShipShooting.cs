using UnityEngine;
using System.Collections.Generic;
public class ShipShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private IWeapon currentWeapon;
    private float fireCooldownTimer;
    public void Start()
    {
        currentWeapon = new BaseWeapon();
        fireCooldownTimer = currentWeapon.GetCooldown();
    }

    public void Update()
    {
        fireCooldownTimer -= Time.deltaTime;
    }
    public void Shoot()
    {
        if(fireCooldownTimer <= 0)
        {
            currentWeapon.Fire(transform.position, bulletPrefab);
            fireCooldownTimer = currentWeapon.GetCooldown();
        }
    }
    public void ApplyPowerup(int powerupType)
    {
        switch (powerupType)
        {
            case PowerupType.Damage:
                currentWeapon = new DamageBuffDecorator(currentWeapon);
                break;
            case PowerupType.FireRate:
                currentWeapon = new FireRateBuffDecorator(currentWeapon);
                break;
            case PowerupType.MultiShot:
                currentWeapon = new MultiShotDecorator(currentWeapon);
                break;
            case PowerupType.Laser:
                currentWeapon = new LaserDecorator(currentWeapon);
                break;
            case PowerupType.AoE:
                currentWeapon = new AoEDecorator(currentWeapon);
                break;
        }
    }
}