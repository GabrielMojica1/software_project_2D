using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System.Collections.Generic;

// Kai Lindemer
// testing the logic of the decorator and strategy patterns rather than 
// the output to avoid test brittleness.

// assured the tests are complete with NSubstitute to mock the IWeapon and IHitStrategy interfaces
// while keeping them isolated from implementation details

public class WeaponPowerupTests
{
    [Test]
    public void DamageBuffDecorator_IncreasesBaseDamage_ByTwo()
    {
        //Arrange / create a mock weapon that deals 5 damage normally
        IWeapon mockWeapon = Substitute.For<IWeapon>();
        mockWeapon.GetCurDmg().Returns(5);

        DamageBuffDecorator decorator = new DamageBuffDecorator(mockWeapon);

        //Act
        int finalDamage = decorator.GetCurDmg();

        //Assert
        Assert.AreEqual(7, finalDamage, "DamageBuffDecorator should add 2 to the base waepon damage");
    }

    [Test]
    public void FireRateBuffDecorator_ReducesCooldown_ToTwentyFivePercent()
    {
        //Arrange / created a mock weapon with a 4 sec cooldown
        IWeapon mockWeapon = Substitute.For<IWeapon>();
        mockWeapon.GetCooldown().Returns(4.0f);

        FireRateBuffDecorator decorator = new FireRateBuffDecorator(mockWeapon);

        //Act
        float finalCooldown = decorator.GetCooldown();

        //Assert
        Assert.AreEqual(1.0f, finalCooldown, 0.01f, "FireRateBuffDecorator should reduce cooldown to 25% of base value");
    }

    [Test]
    public void LaserDecorator_Returns_LaserHitStrategy()
    {
        //Arrange
        IWeapon mockWeapon = Substitute.For<IWeapon>();
        // The base weapon would normally return a NormalHitStrategy
        mockWeapon.GetHitStrategy().Returns(new NormalHitStrategy());

        LaserDecorator decorator = new LaserDecorator(mockWeapon);

        //act
        IHitStrategy strategy = decorator.GetHitStrategy();

        //Assert
        Assert.IsInstanceOf<LaserHitStrategy>(strategy, "LaserDecorator should always return a LaserHitStrategy.");
    }
    [Test]
    public void MultiShotDecorator_CallsInnerFire_ExactlyThreeTimes()
    {
        //arrange
        IWeapon mockWeapon = Substitute.For<IWeapon>();
        //return an empty list instead of null to prevent 'colleection can't be null' during AddRange()
        mockWeapon.Fire(Arg.Any<Vector3>(), Arg.Any<GameObject>())
                  .Returns(new List<GameObject>());

        MultiShotDecorator decorator = new MultiShotDecorator(mockWeapon);
        Vector3 dummyPos = Vector3.zero;

        //Act
        decorator.Fire(dummyPos, null);

        //Assert
        mockWeapon.Received(3).Fire(Arg.Any<Vector3>(), Arg.Any<GameObject>());
    }
}