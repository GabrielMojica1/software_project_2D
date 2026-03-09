class NormalHitStrategy : IHitStrategy
{
    boolean Execute(Collider2D enemy, int dmgAmt)
    {
        TakeDamage(GetComponent(enemy).EnemyStats(), dmgAmt);
        return true;
    }
}