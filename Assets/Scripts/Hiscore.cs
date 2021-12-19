using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Hiscore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI highScore;


    void Start()
    {
        AddHighScore();
        highScore.text =  PlayerPrefs.GetInt("highScore").ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHighScore()
    {
        if (FindObjectOfType<GameSession>().score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", FindObjectOfType<GameSession>().score);
        }
    
    }
}
