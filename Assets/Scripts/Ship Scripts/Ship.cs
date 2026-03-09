using UnityEngine;

public class Ship : MonoBehaviour, IMockShip
{
    public float baseSpeed { get; set; } = 5f;
    public float boostMultiplier { get; set; } = 3f;
    public float boostDuration { get; set; } = 0.2f;
    public float cooldownDuration { get; set; } = 1f;
    public int lives { get; set; } = 3;

    [HideInInspector] public float boostTimer { get; set; } = 0f;
    [HideInInspector] public float cooldownTimer { get; set; } = 0f;

    private IMovementState currentState;
    private ShipShooting shooting;
    [HideInInspector] public bool isInvincible = false;
    public SpriteRenderer shipSprite; 
    private float xMin, xMax, yMin, yMax;

    private void Start()
    {
        currentState = new NormalMovement(this);
        shooting = GetComponent<ShipShooting>();

        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

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
        if (Input.GetKeyDown(KeyCode.Return))
            shooting.Shoot();

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

    public void TakeDamage()
    {
        LivesManager.instance.PlayerHit(this);
    }

    public void SetState(IMovementState newState)
    {
        currentState = newState;
    }

    public void MoveShip(Vector3 direction, float speed)
    {
        if (direction.magnitude > 1f) direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Turn(float amount)
    {
        transform.Rotate(Vector3.forward, amount);
    }
}