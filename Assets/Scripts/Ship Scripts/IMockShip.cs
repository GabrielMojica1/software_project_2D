using UnityEngine;

public interface IMockShip
{
    // Properties that match Ship class
    float baseSpeed { get; }
    float boostMultiplier { get; }
    float boostDuration { get; }
    float cooldownDuration { get; }
    
    float boostTimer { get; set; }
    float cooldownTimer { get; set; }
    
    // Methods that Ship class implements
    void MoveShip(Vector3 direction, float speed);
    void SetState(IMovementState newState);
}