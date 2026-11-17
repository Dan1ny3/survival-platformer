using UnityEngine;

public class GroundEnemyAI : MonoBehaviour
{
    [Header("Setari Viteza")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRange = 6f;

    [Header("Referinte Scena")]
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform player;

    private bool chasing = false;
    private Rigidbody rb;
    private int direction = 1;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        chasing = dist <= detectionRange;

        Move();
        HandleFlipping();
    }

    void Move()
    {
        if (chasing)
        {
            direction = player.position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            if (rightLimit != null && transform.position.x >= rightLimit.position.x)
                direction = -1;

            if (leftLimit != null && transform.position.x <= leftLimit.position.x)
                direction = 1;
        }

        float speed = chasing ? chaseSpeed : patrolSpeed;
        rb.linearVelocity = new Vector3(direction * speed, rb.linearVelocity.y, 0);
    }

    void HandleFlipping()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction < 0;
        }
    }
}