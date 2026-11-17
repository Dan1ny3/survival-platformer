using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        // Verificam mai intai daca obiectul lovit are tag-ul "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Inamicii normali (Slime, Liliac, Mimic etc.)
            HealthUtil hp = other.GetComponent<HealthUtil>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            // Boss-ul Golem (Are scriptul separat pentru UI)
            BossHealth bossHp = other.GetComponent<BossHealth>();
            if (bossHp != null)
            {
                bossHp.TakeDamage(damage);
            }
        }
    }
}