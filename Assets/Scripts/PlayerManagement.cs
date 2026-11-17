using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{

    private HealthUtil health;
    private TriggerZone triggerZone;
    public GameObject sword;
    void Start()
    {
        health = GetComponent<HealthUtil>();
        triggerZone = GetComponentInChildren<TriggerZone>();
        sword.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            sword.SetActive(true);
            Invoke(nameof(DisableSword), 0.3f);
        }

    }

    public List<GameObject> GetEnemiesInAttackZone()
    {
        return triggerZone != null ? triggerZone.GetEnemiesInZone() : new List<GameObject>();
    }

    public void HandleCoinPickup(GameObject coin)
    {
        Debug.Log("Coin collected: " + coin.name);

        // Adaugam 1 punct în GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(1);
        }

        Destroy(coin); // Distrugem moneda
    }


    public void Attack()
    {
        List<GameObject> enemies = triggerZone.GetEnemiesInZone();

        foreach (GameObject enemy in enemies)
        {
            // Verificam daca inamicul nu este null 
            if (enemy != null)
            {
                HealthUtil enemyHP = enemy.GetComponent<HealthUtil>();
                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(10);
                }
            }
        }
    }

    public int GetHealth()
    {
        return health.GetCurrentHealth();
    }

    public void SetHealth(int amount)
    {

        Debug.Log("SetHealth apelat cu: " + amount);
    }

    void DisableSword()
    {
        sword.SetActive(false);
    }
}
