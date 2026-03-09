public class NormalHitStrategy : IHitStrategy
{
    public bool Execute(Collider2D enemy, int dmgAmt)
    {
        enemy.GetComponent<EnemyStats>().TakeDamage(dmgAmt);
        return true;
    }
}