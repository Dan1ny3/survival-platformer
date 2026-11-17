using UnityEngine;
using System.Collections.Generic;

public class TriggerZone : MonoBehaviour
{
    private PlayerManagement player;
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Start()
    {
        if (player == null)
        {
            player = GetComponentInParent<PlayerManagement>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInZone.Contains(other.gameObject))
            {
                enemiesInZone.Add(other.gameObject);
            }
        }
        else if (other.CompareTag("Coin"))
        {
            player.HandleCoinPickup(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Scoatem inamicul specific care a iesit din raza de atac
            enemiesInZone.Remove(other.gameObject);
        }

        // Curatam preventiv si eventualele obiecte distruse
        CleanupList();
    }

    public List<GameObject> GetEnemiesInZone()
    {
        CleanupList(); // Curatam lista inainte de a o trimite catre PlayerManagement
        return enemiesInZone;
    }

    private void CleanupList()
    {
        enemiesInZone.RemoveAll(enemy => enemy == null);
    }
}