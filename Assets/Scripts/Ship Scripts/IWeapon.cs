using System.Collections.Generic;
public interface IWeapon
{
    int GetCurDmg();
    float GetCooldown();
    List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab);

}