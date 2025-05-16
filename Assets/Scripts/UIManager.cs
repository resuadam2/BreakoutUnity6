using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] lifes;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>();
        // lifes = GameObject.FindGameObjectsWithTag("life");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.LoadBlocks();
        SetScore(gameManager.points);
        SetLifes(gameManager.lifes);
    }

    public void SetScore(int points)
    {
        scoreText.text = points.ToString();
    }

    public void SetLifes(int lifesParam)
    {
        for (int i = 0; i <lifesParam; i++)
        {
            lifes[i].SetActive(true);
        }
        
        for(int i = lifesParam; i < GameManager.Instance.MAX_LIFES; i++)
        {
            lifes[i].SetActive(false);
        }
        
    }

    public void AddScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        scoreText.text = "0";
    }

    public void LoseLife()
    {
        for (int i = lifes.Length -1; i > 0; i--)
        {
            if (lifes[i].activeSelf)
            {
                lifes[i].SetActive(false);
                break;
            }
        }
    }

    public void AddLife()
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            if(!lifes[i].activeSelf)
            {
                lifes[i].SetActive(true);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
