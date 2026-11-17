using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Setari Tranzitie")]
    [SerializeField] private string scenaTinta = "CaveLevel";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene(scenaTinta);
        }
    }
}