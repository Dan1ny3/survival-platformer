using UnityEngine;

public class FallDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificam daca cel care a cazut este jucatorul
        if (other.CompareTag("Player"))
        {

            GameManager.Instance.ShowDefeat();

            Debug.Log("Jucatorul a picat in abis! A aparut ecranul de Defeat.");
        }
    }
}