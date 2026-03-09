using UnityEngine;
using System.Collections.Generic;
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon Weapon;
    public WeaponDecorator(IWeapon weapon)
    {
        Weapon = weapon;
    }

    public virtual int GetCurDmg()
    {
        return Weapon.GetCurDmg();  
    }
    public virtual float GetCooldown()
    {
        return Weapon.GetCooldown();
    }
    public virtual List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab)
    {
        return Weapon.Fire(spawnPos, bulletPrefab);
    }
    public virtual IHitStrategy GetHitStrategy()
    {
        return Weapon.GetHitStrategy();
    }
}