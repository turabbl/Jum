using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public GameObject currentScoreText;
    public GameObject bestScoreText;
    int currentScore = 0;

    private void Start()
    {
        GetBestScore();   
    }

    private void GetBestScore()
    {
        bestScoreText.gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        currentScoreText.gameObject.GetComponent<Text>().text = currentScore.ToString();

        if (currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            bestScoreText.gameObject.GetComponent<Text>().text = currentScore.ToString();
            PlayerPrefs.SetInt("BestScore",currentScore);
        }
    }


}
