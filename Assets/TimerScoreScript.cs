using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScoreScript : MonoBehaviour
{

    public float time;
    public int score;
    public TMPro.TextMeshProUGUI timeText;
    public TMPro.TextMeshProUGUI scoreText;
    public bool gameStart;
    public GameObject startMenu;
    // Start is called before the first frame update
    void Start()
    {
        time = 90;
        score = 0;
        gameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
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
        startMenu.SetActive(false);
    }
}
