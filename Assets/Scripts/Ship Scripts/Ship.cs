using UnityEngine;

public class Ship : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float boostMultiplier = 3f;
    public float boostDuration = 0.2f;
    public float cooldownDuration = 1f;

    [HideInInspector] public float boostTimer = 0f;
    [HideInInspector] public float cooldownTimer = 0f;

    private IMovementState currentState;

    // Screen boundaries (will use later)
    private float xMin, xMax, yMin, yMax;

    private void Start()
    {
        currentState = new NormalMovement(this);

        // Get the screen bounds to use for clamping method
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight   = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        xMin = bottomLeft.x;
        yMin = bottomLeft.y;
        xMax = topRight.x;
        yMax = topRight.y;
    }

    private void Update()
    {
        currentState.Move();

        if (Input.GetKey(KeyCode.LeftArrow))
            currentState.HandleLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            currentState.HandleRight();
        if (Input.GetKey(KeyCode.DownArrow))
            currentState.HandleDown();
        if (Input.GetKey(KeyCode.UpArrow))
            currentState.HandleUp();
        if (Input.GetKeyDown(KeyCode.Space))
            currentState.HandleBoost();

        currentState.UpdateState();

        if (boostTimer > 0f)
            boostTimer -= Time.deltaTime;
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        ClampPosition();
    }

    private void ClampPosition()
    {
        float margin = 0.4f;
        Vector3 pos = transform.position;
        
        pos.x = Mathf.Clamp(pos.x, xMin + margin, xMax - margin);

        float upperLimit = yMin + (yMax - yMin) * 0.25f; 
        pos.y = Mathf.Clamp(pos.y, yMin + margin, upperLimit);

        transform.position = pos;
    }

    public void SetState(IMovementState newState)//State pattern implementation, allows for easy switching between movement states
    {
        currentState = newState;
    }

    public void MoveShip(Vector3 direction, float speed)
    {
        if (direction.magnitude > 1f) direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Turn(float amount)//Just in case we need to add turning
    {
        transform.Rotate(Vector3.forward, amount);
    }
}