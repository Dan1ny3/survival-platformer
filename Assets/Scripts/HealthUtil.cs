using System;
using UnityEngine;

public class HealthUtil : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public event Action<int, int> OnHealthChanged;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Anuntam UI-ul ca viata s-a schimbat
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Anuntam UI-ul ca viata s-a schimbat dupa vindecare
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            // Apelam panoul de Defeat din GameManager in loc sa dam restart
            GameManager.Instance.ShowDefeat();
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public int GetMaxHealth() => maxHealth;
    public int GetCurrentHealth() => currentHealth;
}