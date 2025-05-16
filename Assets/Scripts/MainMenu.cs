using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject recordsPanel, menuPanel;
    [SerializeField] TMPro.TextMeshProUGUI recordText;

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
        audioSource.Play();
    }

    public void ShowMenu()
    {
        audioSource.Play();
        if (recordsPanel.activeSelf)
        {
            recordsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }

    public void ShowRecord()
    {
        audioSource.Play();
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            recordsPanel.SetActive(true);
        }
        recordText.text = "RECORD ACTUAL: " + SaveManager.LoadRecord().ToString();
    }
}
