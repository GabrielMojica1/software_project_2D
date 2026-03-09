using UnityEngine;
using System.Collections.Generic;
public class BaseWeapon : IWeapon
{
    public int GetCurDmg()
    {
        return 1;
    }
    public float GetCooldown()
    {
        return 0.5f;
    }
    public List<GameObject> Fire(Vector3 spawnPos, GameObject bulletPrefab)
    {
        
        //spawn a bullet, assign damage value based on get damage, return spawned bullet in a list
        List<GameObject> bulletList = new List<GameObject>();
        GameObject newBullet;
        newBullet = GameObject.Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        newBullet.GetComponent<PlayerBullet>().damage = this.GetCurDmg();
        newBullet.GetComponent<PlayerBullet>().currentStrategy = this.GetHitStrategy();
        bulletList.Add(newBullet);
        return bulletList;
    }
    public IHitStrategy GetHitStrategy()
    {
        return new NormalHitStrategy();
    }
}