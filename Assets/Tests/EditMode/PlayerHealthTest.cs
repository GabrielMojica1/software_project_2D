using NUnit.Framework;
using UnityEngine;

public class PlayerHealthTest
{
    private Ship ship;
    private LivesManager livesManager;

    [SetUp]
    public void Setup()
    {
        livesManager = new GameObject().AddComponent<LivesManager>();

        ship = new GameObject().AddComponent<ShipShooting>().gameObject.AddComponent<Ship>();
        ship.lives = 3;
        ship.isInvincible = false;
    }

    [Test]
    public void TakeDamage_NormalHit_ReducesLives()
    {
        livesManager.PlayerHit(ship);

        Assert.AreEqual(2, ship.lives);
    }

    [Test]
    public void TakeDamage_TwoLives_OneLiveRemains()
    {
        ship.lives = 2;

        livesManager.PlayerHit(ship);

        Assert.AreEqual(1, ship.lives);
        Assert.IsNotNull(ship);
    }

    [Test]
    public void TakeDamage_LastLife_ShipDies()
    {
        ship.lives = 1;

        livesManager.PlayerHit(ship);

        Assert.IsTrue(ship == null);
    }

    [Test]
    public void TakeDamage_MoreHitsThanLives_ShipDies()
    {
        livesManager.PlayerHit(ship);
        livesManager.PlayerHit(ship);
        livesManager.PlayerHit(ship);

        Assert.IsTrue(ship == null);
    }

    [Test]
    public void TakeDamage_FullLives_ShipSurvives()
    {
        livesManager.PlayerHit(ship);

        Assert.IsNotNull(ship);
    }

    [Test]
    public void TakeDamage_WhenInvincible_DoesNotReduceLives()
    {
        ship.isInvincible = true;

        livesManager.PlayerHit(ship);

        Assert.AreEqual(3, ship.lives);
    }
}