using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int bricksAmount;
    public int level;
    public float ballSpeed;
    public int lives;
    public bool isGameOver = false;
    public int score = 0;

    private GameObject ball;
    public GameObject gameOverScreen;
    public GameObject levelPassedScreen;
    public GameObject pauseGameScreen;
    private TextMeshProUGUI scoreui;
    public GameObject live1;
    public GameObject live2;
    public GameObject live3;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        scoreui = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameScreen.activeSelf)
            {
                pauseGameScreen.SetActive(false);
            }
            else
            {
                pauseGameScreen.SetActive(true);
            }
        }
    }

    void GameOver()
    {
        if (lives <= 0)
        {
            GameOverActivities();
            gameOverScreen.SetActive(true);
        }

        if (bricksAmount <= 0 && lives > 0)
        {
            GameOverActivities();
            levelPassedScreen.SetActive(true);
        }
    }

    void GameOverActivities()
    {
        isGameOver = true;
        Destroy(ball);
    }

    void ManageLives()
    {
        if (lives < 3)
        {
            live3.SetActive(false);
        }
        if (lives < 2)
        {
            live2.SetActive(false);
        }
        if (lives < 1)
        {
            live1.SetActive(false);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int sceneBuildIndex = activeScene.buildIndex;

        SceneManager.LoadScene(sceneBuildIndex + 1);
    }

    public void ResumeGame()
    {
        pauseGameScreen.SetActive(false);
    }

    void UpdateUI()
    {
        scoreui.text = "Score: " + score;
        ManageLives();
    }
}
