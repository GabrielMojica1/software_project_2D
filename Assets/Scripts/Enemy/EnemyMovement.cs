using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private float direction = 1f;

    public float xMin = -8f;
    public float xMax = 8f;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.right * direction * stats.moveSpeed * Time.deltaTime);

        if (transform.position.x >= xMax)
        {
            direction = -1f;
        }
        else if (transform.position.x <= xMin)
        {
            direction = 1f;
        }
    }
}