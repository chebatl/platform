using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score {get; private set;}
    [SerializeField] private Text scoreText;
    public static GameManager INSTANCE {get; private set;}
    private void Awake() {
        INSTANCE = this;
    }

    public void GetCoin(){
        ++score;
        scoreText.text = score.ToString();
    }
}
