using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int blocks;

    [SerializeField] public int MAX_LIFES = 5;
    public int lifes = 3;

    [SerializeField] private int pointsPerBlock = 10;
    public int points = 0;

    private void Awake()
    {
        // Patrón Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadBlocks()
    {
        /*
         * FindGameObjectsWithTag nos devuelve un array con los GameObjects que contienen esa etiqueta
         */
        blocks = GameObject.FindGameObjectsWithTag("block").Length;
    }

    public void LoseLife()
    {
        lifes--;
        FindAnyObjectByType<UIManager>().LoseLife();
        if (lifes <= 0)
        {
            Debug.Log("Game Over :(((");
            SceneManager.LoadScene("GameOver"); // Cargar escena de GameOver
        } else
        {
            ResetLevel();
        }
    }

    public void AddLife()
    {
        if(lifes < MAX_LIFES)
        {
            lifes++;
            FindAnyObjectByType<UIManager>().AddLife();
        }
    }

    public void ResetLevel()
    {
        FindAnyObjectByType<Player>().ResetPlayer();
        FindAnyObjectByType<Ball>().ResetBall();
    }

    public void BlockDestroyed()
    {
        blocks--; // Restamos un bloque
        points += pointsPerBlock;
        FindAnyObjectByType<UIManager>().AddScore(points);
        if (blocks <= 0)
        {
            Debug.Log("Nivel completado :)");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetGame()
    {
        points = 0;
        lifes = 3;
    }
}
