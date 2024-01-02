using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.5f;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI pauseText;
    public bool isGameActive; //game active check for box clicking
    public Button restartButton;
    public GameObject titleScreen;
    public AudioClip gameOverSound;
    private AudioSource gameSounds; //plays game over sound on gamemanager game object, less then ideal. 
    public TextMeshProUGUI gameTimer;
    public bool isGamePaused = false; //pause check so you cant click boxes when game is paused
    public float gameTime;
    public bool timerOn = false; //sets timer on/off

    void Start()
    {
        gameSounds = GetComponent<AudioSource>();  
    }

    void Update()
    {
        gameTimeCountdown(); //runs timer
        checkForPause();     //checks for space bar keystroke
    }

    void timeFormatter() //converts the float time to an int for readability
    {
        float timerSeconds = Mathf.FloorToInt(gameTime % 60);
        gameTimer.text = "TIMER: " + timerSeconds;
    }

    void checkForPause() //pause function 
    {
        if (Input.GetKeyDown("space") && isGamePaused == false)
        {
            Time.timeScale = 0;
            isGamePaused = true;
            pauseText.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown("space") && isGamePaused == true)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            pauseText.gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnTarget() //spawns targets based on spawn rate, random targets within list
    {
        while (isGameActive)
        {
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, targets.Count);
        Instantiate(targets[index]);
        }
    }

    public void gameTimeCountdown() //manages game timer, ends game on 0. 
    {
         if(timerOn)
        {
            gameTime -= Time.deltaTime;
            timeFormatter();
        }
        if(gameTime < 0)
        {
            gameTimer.text = "TIMER: 0";
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd) //adds score based on destroyed objects value to score total
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver() //disables game, enables game over screen and restart button
    {
        if (isGameActive == true)
        {
            gameSounds.PlayOneShot(gameOverSound, .30f);
        }
        timerOn = false;
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() //uses unity engine to restart scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)  //starts game, starts coroutine to start spawning targets, resets score, removes start screen, and sets difficulty
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        timerOn = true;
        gameTime = 30;

    }
}
