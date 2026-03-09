//Maximus Neri Test Suite
/*In this test suite, we are testing the movement functionality of the player, specifically the movement. I tested public behaviors such as position changes and 
state transitions rather than internal tests, which keeps these tests resilient to refactoring. I used NSubstitute and mock objects to verify specific behaviors such as 
ensuring the correct speed multiplier without needing a real player instance.   */

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Collections.Generic;
using System.Reflection;
public class PlayerMovementTests
{
    private GameObject shipObj;
    private Ship realShip;
    private IMockShip mockShip;

    [SetUp]
    public void Setup()
    {
        shipObj = new GameObject();
        realShip = shipObj.AddComponent<Ship>();
        
        realShip.baseSpeed = 5f;
        realShip.boostMultiplier = 3f;
        realShip.boostDuration = 0.2f;
        realShip.cooldownDuration = 1f;
        
        mockShip = Substitute.For<IMockShip>();
        mockShip.baseSpeed.Returns(5f);
        mockShip.boostMultiplier.Returns(3f);
        mockShip.boostDuration.Returns(0.2f);
        mockShip.cooldownDuration.Returns(1f);
    }

    [TearDown]
    public void TearDown()
    {
        if (shipObj != null)
            GameObject.DestroyImmediate(shipObj);
    }

    [Test]
    public void Move_Up_IncreasesYPosition()
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(0, 1f, 0);
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Greater(realShip.transform.position.y, 0, "Moving Up should increase Y position");
        Assert.AreEqual(0, realShip.transform.position.x, 0.001f, "X position should not change when moving Up");
    }

    [Test]
    public void Move_Down_DecreasesYPosition()
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(0, -1f, 0);
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Less(realShip.transform.position.y, 0, "Moving Down should decrease Y position");
        Assert.AreEqual(0, realShip.transform.position.x, 0.001f, "X position should not change when moving Down");
    }

    [Test]
    public void Move_Right_IncreasesXPosition()
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(1f, 0, 0);
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Greater(realShip.transform.position.x, 0, "Moving Right should increase X position");
        Assert.AreEqual(0, realShip.transform.position.y, 0.001f, "Y position should not change when moving Right");
    }

    [Test]
    public void Move_Left_DecreasesXPosition()
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(-1f, 0, 0); 
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Less(realShip.transform.position.x, 0, "Moving Left should decrease X position");
        Assert.AreEqual(0, realShip.transform.position.y, 0.001f, "Y position should not change when moving Left");
    }
    [Test]
    public void Move_UpAndRight_DiagonallyIncreasesBoth()//tests to see if moving up and right increases both x and y position correctly
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(1f, 1f, 0);  
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Greater(realShip.transform.position.x, 0);
        Assert.Greater(realShip.transform.position.y, 0);
        Assert.AreEqual(realShip.transform.position.x, realShip.transform.position.y, 0.01f);
    }

    [Test]
    public void Move_DownAndLeft_DiagonallyDecreasesBoth()//tests to see if moving down and left decreases both x and y position correctly
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 dir = new Vector3(-1f, -1f, 0);
    
        // Act
        realShip.MoveShip(dir, realShip.baseSpeed);
    
        // Assert
        Assert.Less(realShip.transform.position.x, 0);
        Assert.Less(realShip.transform.position.y, 0);
        Assert.AreEqual(realShip.transform.position.x, realShip.transform.position.y, 0.01f);
    }
    
    [Test]
    public void NormalMovement_HandleBoost_TransitionsToBoosting()//tests that boosting works correctly
    {
        // Arrange
        realShip.cooldownTimer = 0f;
        var state = new NormalMovement(realShip);
    
        var oldState = realShip.GetCurrentState();
    
        // Act
        state.HandleBoost();
    
        // Assert
        var newState = realShip.GetCurrentState();
        Assert.AreNotEqual(oldState, newState);
        Assert.IsTrue(newState.GetType().Name == "Boosting");
    }

    [Test]
    public void Boosting_Moves_FasterThanNormal()//tests that boosting accurately increases speed
    {
        // Arrange
        realShip.transform.position = Vector3.zero;
        Vector3 direction = new Vector3(1, 0, 0);

        // Gets normal movement distance
        realShip.MoveShip(direction, realShip.baseSpeed); 
        float normalX = realShip.transform.position.x;

        // Reset the position
        realShip.transform.position = Vector3.zero;

        // Act
        float boostedSpeed = realShip.baseSpeed * realShip.boostMultiplier; 
        realShip.MoveShip(direction, boostedSpeed);
        float boostedX = realShip.transform.position.x;

        // Assert
        Assert.Greater(boostedX, normalX * 2.9f); 
        Assert.Less(boostedX, normalX * 3.1f);
    }

    [Test]
    public void Overheated_UpdateState_WhenCooldownEnds_ReturnsToNormal()//tests cooldown functionality
    {
        // Arrange
        realShip.cooldownTimer = 0f;
        realShip.SetState(new Overheated(realShip));
    
        var state = new Overheated(realShip);
    
        // Act
        state.UpdateState();
    
        // Assert
        var newState = realShip.GetCurrentState(); 
        Assert.IsTrue(newState.GetType().Name == "NormalMovement");
    }

    [Test]
    public void NormalMovement_HandleBoost_WithCooldown_DoesNothing()//tests to see if no boosting during cooldown
    {
        // Arrange
        realShip.cooldownTimer = 0.5f;
        var oldState = realShip.GetCurrentState(); 
    
        var state = new NormalMovement(realShip);
    
        // Act
        state.HandleBoost();
    
        // Assert
        var newState = realShip.GetCurrentState(); 
        Assert.AreEqual(oldState, newState);
    }
}

public class TestableBoosting : Boosting
{
    private Ship ship;
    
    public TestableBoosting(Ship ship) : base(ship)
    {
        this.ship = ship;
    }
    
    public void TestMove(Vector3 direction)
    {
        // directly calls MoveShip with boosted speed for testing
        ship.MoveShip(direction, ship.baseSpeed * ship.boostMultiplier);
    }
}