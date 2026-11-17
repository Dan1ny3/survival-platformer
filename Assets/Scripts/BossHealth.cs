using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Setari Viata")]
    public float viataMaxima = 500f;
    private float viataCurenta;

    [Header("Conexiune UI")]
    [Tooltip("Trage aici BossHealthBar-ul din Canvas")]
    public Slider baraDeViataUI;

    [Tooltip("Trage aici tot obiectul BossHealthBar pentru a-l ascunde la final")]
    public GameObject containerUI;

    void Start()
    {
        viataCurenta = viataMaxima;

        if (baraDeViataUI != null)
        {
            baraDeViataUI.maxValue = viataMaxima;
            baraDeViataUI.value = viataCurenta;
        }
    }

    public void TakeDamage(float damageAmmount)
    {
        viataCurenta -= damageAmmount;

        if (baraDeViataUI != null)
        {
            baraDeViataUI.value = viataCurenta;
        }

        if (viataCurenta <= 0)
        {
            Mori();
        }
    }

    private void Mori()
    {
        // Ascundem bara de viata de pe ecran cand lupta se termina
        if (containerUI != null)
        {
            containerUI.SetActive(false);
        }

        Debug.Log("Golemul a fost distrus!");

        GameManager.Instance.ShowVictory();

        Destroy(gameObject);
    }
}