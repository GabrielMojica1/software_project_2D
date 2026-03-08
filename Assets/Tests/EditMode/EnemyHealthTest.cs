using NUnit.Framework;
using UnityEngine;

public class GameTests
{
    private EnemyStats enemyStats;

    [SetUp]
    public void Setup()
    {
        enemyStats = new GameObject().AddComponent<EnemyStats>();
        enemyStats.maxHealth = 10;
        enemyStats.currentHealth = 10;
    }

    [Test]
    public void TakeDamage_NormalDamage_ReducesHealth()
    {
        enemyStats.TakeDamage(3);

        Assert.AreEqual(7, enemyStats.currentHealth);
    }

    [Test]
    public void TakeDamage_DamageEqualsHealth_EnemyDies()
    {
        enemyStats.TakeDamage(10);

        Assert.IsTrue(enemyStats == null);
    }

    [Test]
    public void TakeDamage_DamageOneBelowHealth_EnemySurvives()
    {
        enemyStats.TakeDamage(9);

        Assert.AreEqual(1, enemyStats.currentHealth);
        Assert.IsNotNull(enemyStats);
    }

    [Test]
    public void TakeDamage_DamageExceedsHealth_EnemyDies()
    {
        enemyStats.TakeDamage(999);

        Assert.IsTrue(enemyStats == null);
    }

    [Test]
    public void TakeDamage_ZeroDamage_HealthUnchanged()
    {
        enemyStats.TakeDamage(0);

        Assert.AreEqual(10, enemyStats.currentHealth);
    }
}