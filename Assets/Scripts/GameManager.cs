using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Tracks survival score (distance traveled), shows it on screen,
/// and handles the lose / restart flow for the endless runner.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public Transform player;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    bool isGameOver = false;
    float survivalScore = 0f;
    float startZ = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        isGameOver = false;
        survivalScore = 0f;
        Time.timeScale = 1f;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (player != null) startZ = player.position.z;

        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver || player == null) return;

        float distance = player.position.z - startZ;
        if (distance > survivalScore)
        {
            survivalScore = distance;
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Distance: " + Mathf.FloorToInt(survivalScore) + "m";
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        PlayerMovement pm = player != null ? player.GetComponent<PlayerMovement>() : null;
        if (pm != null)
        {
            pm.LockControls();
        }

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (finalScoreText != null)
        {
            finalScoreText.text = "You survived " + Mathf.FloorToInt(survivalScore) + "m!";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
