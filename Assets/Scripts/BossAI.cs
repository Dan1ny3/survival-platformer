using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    [Header("Referinte")]
    [Tooltip("Trage jucatorul aici")]
    public Transform player;

    [Tooltip("Trage bara de viata aici ca sa apara cand te vede")]
    public GameObject bossHealthBarUI;

    private Animator animator;
    private Rigidbody rb;

    [Header("Setari Lupta")]
    public float razaDetectie = 15f;
    public float vitezaUrmarire = 3f;
    public float distantaAtac = 2.5f;
    public float timpIntreAtacuri = 2f;

    private bool isAttacking = false;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Daca jucatorul a murit sau daca boss-ul deja ataca, oprim logica temporar
        if (player == null || isAttacking) return;

        // Calculam distanta pana la jucator
        float distance = Vector3.Distance(transform.position, player.position);

        // Jucatorul e in afara arenei
        if (distance > razaDetectie)
        {

            if (animator != null) animator.SetFloat("Speed", 0f);
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }


        if (bossHealthBarUI != null && !bossHealthBarUI.activeSelf)
        {
            bossHealthBarUI.SetActive(true);
        }

        if (distance > distantaAtac)
        {
            ChasePlayer();
        }
        else
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private void ChasePlayer()
    {
        if (animator != null) animator.SetFloat("Speed", 1f);

        direction = player.position.x > transform.position.x ? 1 : -1;

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        rb.linearVelocity = new Vector3(direction * vitezaUrmarire, rb.linearVelocity.y, 0);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        if (animator != null) animator.SetFloat("Speed", 0f);

        if (animator != null) animator.SetTrigger("Attack");

        yield return new WaitForSeconds(timpIntreAtacuri);

        isAttacking = false;
    }
}
