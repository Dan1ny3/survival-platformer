using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Setari Lovitura")]
    public int damage = 10;

    [Tooltip("La cate secunde poate inamicul sa iti ia viata din nou daca ramai lipit de el")]
    public float cooldownLovitura = 1f;

    private float timpulUrmatoareiLovituri = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AplicaDamage(collision.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AplicaDamage(other.gameObject);
        }
    }

    private void AplicaDamage(GameObject jucator)
    {
        // Verificam pe ceasul intern al jocului daca a trecut secunda de cooldown
        if (Time.time >= timpulUrmatoareiLovituri)
        {
            HealthUtil hp = jucator.GetComponent<HealthUtil>();
            if (hp != null)
            {
                hp.TakeDamage(damage);

                // Setam urmatorul moment in care inamicul are voie sa mai dea damage
                timpulUrmatoareiLovituri = Time.time + cooldownLovitura;
            }
        }
    }
}