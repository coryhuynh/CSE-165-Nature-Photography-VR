using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScoreScript : MonoBehaviour
{
    public float startTime;
    public TMPro.TextMeshProUGUI startTimeText;
    public Slider timeSlider;
    public float time;
    public int score;
    public TMPro.TextMeshProUGUI timeText;
    public TMPro.TextMeshProUGUI scoreText;
    public bool gameStart;
    public bool pauseGame;
    public GameObject startMenu;
    // Start is called before the first frame update
    void Start()
    {
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
        if (gameStart && !pauseGame)
        {
            time -= Time.deltaTime;
            float timeRemaining = time + 1;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
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
}
