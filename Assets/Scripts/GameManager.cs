using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int bricksAmount;
    public int level;
    public float ballSpeed = 15.0f;
    public int lives;
    public bool isGameOver = false;
    public int score = 0;

    private GameObject ball;
    private TextMeshProUGUI scoreui;

    public GameObject pauseGameScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject levelPassedScreen;

    [SerializeField]
    private GameObject live1;
    [SerializeField]
    private GameObject live2;
    [SerializeField]
    private GameObject live3;

    private AudioSource musicSource;
    private AudioSource levelPassedAudio;

    void Start()
    {
        ball = GameObject.Find("Ball");
        scoreui = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        musicSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        levelPassedAudio = GameObject.Find("LevelPassedAudio").GetComponent<AudioSource>();

        musicSource.Play();
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
                musicSource.Play();
            }
            else
            {
                pauseGameScreen.SetActive(true);
                musicSource.Stop();
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
            levelPassedAudio.Play();
            GameOverActivities();
            levelPassedScreen.SetActive(true);
        }
    }

    void GameOverActivities()
    {
        isGameOver = true;
        musicSource.Stop();
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
        musicSource.Play();
    }

    void UpdateUI()
    {
        scoreui.text = "Score: " + score;
        ManageLives();
    }
}
