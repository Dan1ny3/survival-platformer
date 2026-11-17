using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private HealthUtil playerHealth;

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        if (playerHealth != null && slider != null)
        {
            slider.maxValue = playerHealth.GetMaxHealth();
            slider.value = playerHealth.GetCurrentHealth();
            Debug.Log("UI Initializat cu: " + slider.value);
        }
        else
        {
            Debug.LogError("Lipseste PlayerHealth sau Slider de pe HealthBarUI!");
        }
    }

    void OnEnable()
    {
        if (playerHealth != null) playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    void OnDisable()
    {
        if (playerHealth != null) playerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if (slider != null)
        {
            slider.value = currentHealth;
        }
    }
}