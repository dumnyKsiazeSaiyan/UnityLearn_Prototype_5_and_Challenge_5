using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    private AudioSource audioSource;

    public Slider volumeSlider;

    private float spawnRate = 1.0f;
    private int score;
    private int lives;

    public bool isGameActive;

    //Pause
    public GameObject pauseScreen;
    public bool paused = false;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        volumeSlider.value = audioSource.volume;
    }
    void ChangePause()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            audioSource.Pause();

        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            audioSource.UnPause();


        }
    }

    private void Update()
    {
        ChangePause();
       
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototype 5");
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 0;
        spawnRate /= difficulty;


        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
    }
}
