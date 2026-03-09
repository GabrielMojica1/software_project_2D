public class BaseWeapon : IWeapon
{
    int GetCurDmg();
    float GetCoolDown();
    List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab)
    {
        //spawn a bullet, assign damage value based on get damage, return spawned bullet in a list
    }
}