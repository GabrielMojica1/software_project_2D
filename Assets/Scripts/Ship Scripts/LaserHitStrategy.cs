using UnityEngine;
using System.Collections.Generic;
public class LaserHitStrategy : IHitStrategy
{
    public bool Execute(Collider2D enemy, int dmgAmt)
    {
        enemy.GetComponent<EnemyStats>().TakeDamage(dmgAmt);
        return false;
    }
}