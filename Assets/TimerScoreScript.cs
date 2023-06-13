using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScoreScript : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject player;
    public CamScript camera;
    public PhotoAlbumScript album;
    public float startTime;
    public TMPro.TextMeshProUGUI startTimeText;
    public TMPro.TextMeshProUGUI gameOverText;
    public Slider timeSlider;
    public float time;
    public int score;
    public TMPro.TextMeshProUGUI timeText;
    public TMPro.TextMeshProUGUI scoreText;
    public bool gameStart;
    public bool pauseGame;
    public bool gameOver;
    public GameObject startMenu;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 180;
        time = 90;
        score = 0;
        gameStart = false;
        pauseGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            time = startTime;
        }
        if (gameStart && !pauseGame && !gameOver)
        {
            time -= Time.deltaTime;
            float timeRemaining = time + 1;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeText.text = "Time "+ string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if(time <= 0){
            gameOver = true;
            gameOverText.text = "Your final score was " + camera.score.ToString() + "\nClick below to stay in the gallery and view your photos";
            album.startAlbum();
            gameOverScreen.SetActive(true);
            player.transform.position = new Vector3(-198, 2, 180);
            time = startTime;
        }

        scoreText.text = "Score "+ string.Format("{000}", camera.score);
    }

    public void startGame()
    {
        gameStart = true;
        pauseGame = false;
        startMenu.SetActive(false);
    }

    public void sliderValChange()
    {
        startTime = timeSlider.value;
        float timeRemaining = time + 1;
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        startTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void onContinue(){
        gameOverScreen.SetActive(false);
        restartButton.SetActive(true);
    }
}
