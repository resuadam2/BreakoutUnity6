using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;

    private void Start()
    {
        if (SaveManager.SaveRecord(GameManager.Instance.points)) // Llamar a guardar record para comprobar si hay un nuevo record
        {
            scoreText.text = "NEW RECORD!!! \n SCORE: " + GameManager.Instance.points.ToString();
        }
        else
        {
            scoreText.text = "SCORE: " + GameManager.Instance.points.ToString();
        }
        GameManager.Instance.ResetGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        audioSource.Play();
    }
}
