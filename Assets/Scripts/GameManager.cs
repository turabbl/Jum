using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject gameOverPanel;
    public GameObject touchToStartObj;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        StartCoroutine(gameOverCoroutine());
        gameOverPanel.SetActive(true);
        GameObject.Find("CurrentScore").GetComponent<Text>().color = Color.white;
        GameObject.Find("BestScore").GetComponent<Text>().color = Color.white;
        GameObject.Find("BestScore_Text").GetComponent<Text>().color = Color.white;
    }

    IEnumerator gameOverCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.01f;
        yield return new WaitForSeconds(0.1f);
        
        gameOverPanel.SetActive(true);

        yield break;
    }



    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        touchToStartObj.SetActive(false);
    }

}
