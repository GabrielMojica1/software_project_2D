using UnityEngine;

public class Boosting : IMovementState
{
    private Ship ship;

    public Boosting(Ship ship)
    {
        this.ship = ship;
    }

    public void Move()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) dir.y = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) dir.y = -1f;

        ship.MoveShip(dir, ship.baseSpeed * ship.boostMultiplier);
    }

    public void HandleLeft() { }
    public void HandleRight() { }
    public void HandleUp() { }
    public void HandleDown() { }
    public void HandleBoost() { }

    public void UpdateState()
    {
        if (ship.boostTimer <= 0f)
        {
            ship.cooldownTimer = ship.cooldownDuration;
            ship.SetState(new Overheated(ship));
        }
    }
}