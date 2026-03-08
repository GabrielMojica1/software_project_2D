using NUnit.Framework;
using UnityEngine;

public class PlayerHealthTest
{
    private Ship ship;

    [SetUp]
    public void Setup()
    {
        ship = new GameObject().AddComponent<Ship>();
        ship.gameObject.AddComponent<ShipShooting>();
        ship.lives = 3;
    }

    [Test]
    public void TakeDamage_NormalHit_ReducesLives()
    {
        ship.TakeDamage();

        Assert.AreEqual(2, ship.lives);
    }

    [Test]
    public void TakeDamage_TwoLives_OneLiveRemains()
    {
        ship.lives = 2;

        ship.TakeDamage();

        Assert.AreEqual(1, ship.lives);
        Assert.IsNotNull(ship);
    }

    [Test]
    public void TakeDamage_LastLife_ShipDies()
    {
        ship.lives = 1;

        ship.TakeDamage();

        Assert.IsTrue(ship == null);
    }

    [Test]
    public void TakeDamage_MoreHitsThanLives_ShipDies()
    {
        ship.TakeDamage();
        ship.TakeDamage();
        ship.TakeDamage();

        Assert.IsTrue(ship == null);
    }

    [Test]
    public void TakeDamage_FullLives_ShipSurvives()
    {
        ship.TakeDamage();

        Assert.IsNotNull(ship);
    }
}