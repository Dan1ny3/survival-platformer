using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    void Update()
    {
        // Verificam daca jucătorul apasa tasta ESC 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Ascundem meniul
        Time.timeScale = 1f;          // Repornim timpul in joc
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Aratam meniul
        Time.timeScale = 0f;           // Inghetam timpul (fizica, miscarea)
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Resetam timpul inainte de a iesi
        SceneManager.LoadScene("MainMenu"); // Pune numele scenei tale de meniu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}