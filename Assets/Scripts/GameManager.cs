using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score {get; private set;}
    [SerializeField] private Text scoreText;
    public static GameManager INSTANCE {get; private set;}
    [SerializeField] private GameObject gameOverPanel;
    private void Awake() {
        Time.timeScale = 1;
        DontDestroyOnLoad(this);
        if(INSTANCE == null){
            INSTANCE = this;
        }else{
            Destroy(this);
        }
        int tempScore = PlayerPrefs.GetInt("score");
        if(tempScore > 0){
            score = tempScore;
            scoreText.text = score.ToString();
        }
    }

    public void GetCoin(){
        ++score;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("score",score);
    }

    public void NextLevel(){
        SceneManager.LoadScene(2);
    }

    public void ShowGameOver(){
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame(){
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
