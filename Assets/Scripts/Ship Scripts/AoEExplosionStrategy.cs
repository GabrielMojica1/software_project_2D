public class AoEExplosionStrategy : IHitStrategy
{
    public bool Execute(Collider2D enemy, int dmgAmt)
    {
        float blastRadius = 2.5f;
        Vector2 explosionPos = enemy.transform.position;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explosionPos, blastRadius);

        foreach (Collider2D hitEnemy in hitColliders)
        {
            GameObject hitStats = hitEnemy.getComponent<EnemyStats>();
            if (hitStats != null)
            {
                hitEnemy.GetComponent<EnemyStats>().TakeDamage(dmgAmt);
            }
        }

        return true;
    }
}