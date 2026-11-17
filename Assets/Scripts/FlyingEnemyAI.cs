using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed = 2f;            // Viteza de deplasare orizontala
    [SerializeField] private float patrolDistance = 5f;   // Cat de departe merge la stanga si la dreapta
    [SerializeField] private float oscillationHeight = 1f; // Cat de sus/jos oscileaza
    [SerializeField] private float oscillationSpeed = 3f;  // Cat de repede oscileaza (frecventa)

    private Vector3 startPosition;
    private int direction = 1;
    private float oscillationTimer;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        HandleHorizontalMovement();
        HandleVerticalOscillation();
    }

    private void HandleHorizontalMovement()
    {
        // Miscare simpla stanga-dreapta
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

        // Verificam daca am depasit distanta de patrulare
        if (Mathf.Abs(transform.position.x - startPosition.x) >= patrolDistance / 2)
        {
            ChangeDirection();
        }
    }

    private void HandleVerticalOscillation()
    {
        oscillationTimer += Time.deltaTime * oscillationSpeed;
        float offset = Mathf.Sin(oscillationTimer) * oscillationHeight;

        // Aplicam doar oscilatia pe axa Y 
        transform.position = new Vector3(transform.position.x, startPosition.y + offset, transform.position.z);
    }

    private void ChangeDirection()
    {
        direction *= -1; // Inversam directia

        // Intoarcem liliacul vizual folosind componenta gasita in copil
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction < 0;
        }
    }
}