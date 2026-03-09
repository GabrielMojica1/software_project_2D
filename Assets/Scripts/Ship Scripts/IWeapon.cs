using System.Collections.Generic;
using UnityEngine;
public interface IWeapon
{
    int GetCurDmg();
    float GetCooldown();
    IHitStrategy GetHitStrategy();
    List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab);

}