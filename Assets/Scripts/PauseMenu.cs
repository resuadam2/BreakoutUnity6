using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI; // This is set in the inspector

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ||  Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Restarts the time in game
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Stops the time in game
        isPaused = true;
    }

    public void RestartLevel()
    {
        // Cargar de nuevo el mismo nivel pero reseteandolo
        Debug.Log("Botón de resetear nivel pulsado.");
    }

    public void LoadMainMenu()
    {
        // Cargar el menú principal
        Debug.Log("Botón de volver al menú principal pulsado.");
    }
}
