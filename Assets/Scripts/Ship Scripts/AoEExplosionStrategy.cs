class AoEExplosionStrategy : IHitStrategy
{
    boolean Execute(Collider2D enemy, int dmgAmt)
    {
        float blastRadius = 2.5f;
        Vector2 explosionPos = GetComponent(enemy).transform.position;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explosionPos, blastRadius);

        foreach (Collider2D hitEnemy in hitColliders)
        {
            if (enemy != null)
            {
                TakeDamage(GetComponent(hitEnemy).EnemyStats(), dmgAmt);
            }
        }

        return true;
    }
}