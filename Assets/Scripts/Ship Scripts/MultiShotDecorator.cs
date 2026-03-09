using UnityEngine;
using System.Collections.Generic;
public class MultiShotDecorator : WeaponDecorator
{
    public MultiShotDecorator(IWeapon weapon) : base(weapon)
    {
        
    }
    public override List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab)
    {
        List<GameObject> bullets = new List<GameObject>();
        bullets.AddRange(base.Fire(spawnPos + new Vector3(1.0f, 0, 0), bulletPrefab));
        bullets.AddRange(base.Fire(spawnPos, bulletPrefab));
        bullets.AddRange(base.Fire(spawnPos - new Vector3(1.0f, 0, 0), bulletPrefab));
        return bullets;
    }
}