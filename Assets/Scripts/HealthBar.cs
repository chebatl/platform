using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int health;
    public int heartsCount;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i<health){
               hearts[i].sprite = fullHeart;
            }else{
               hearts[i].sprite = emptyHeart;
            }
            if(i < heartsCount){
               hearts[i].enabled = true;
            }else{
               hearts[i].enabled = false;
            }
        }
    }
}
