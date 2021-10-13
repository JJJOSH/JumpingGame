using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool mute = false;
    public GameObject GameOverPanel;
    public GameObject LevelCompletedPanel;
    public GameObject GamePlayPanel;
    public GameObject StartMenuPanel;

    public static int currentLevelIndex;
    public Slider GameProgressSlider;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public static int numberOfPassedRings;
    public static int score = 0;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    void Start()
    {
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        isGameStarted = false;
        gameOver = levelCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Update our UI 
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        GameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        // Start Level
        if(Input.GetMouseButtonDown(0) && !isGameStarted)
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            isGameStarted = true;
            GamePlayPanel.SetActive(true);
            StartMenuPanel.SetActive(false);

        }

        // Game Over
        if (gameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                score = 0;
                SceneManager.LoadScene("Level");
            }
        }

        // Level Completed

        if (levelCompleted)
        {
            LevelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
