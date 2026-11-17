using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Monede")]
    [SerializeField] private TextMeshProUGUI coinText;
    private int score = 0;

    [Header("Ecrane Final de Joc")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private bool gameover = false;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        UpdateUI();
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
        Time.timeScale = 1f;

        // Resetam starea la inceput de nivel
        gameover = false;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null) { coinText.text = "Coins: " + score; }
    }

    public void ShowVictory()
    {
        // Daca jucatorul a murit deja in acelasi frame, oprim functia
        if (gameover) return;

        gameover = true; // Blocam viitoarele apeluri
        if (victoryPanel != null) victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowDefeat()
    {
        // Daca boss-ul a murit deja in acelasi frame, oprim functia
        if (gameover) return;

        gameover = true; // Blocam viitoarele apeluri
        if (defeatPanel != null) defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainHub");
    }
}