using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;


    public TextMeshProUGUI pointText;
    public int points;
    public TextMeshProUGUI maxScoreText;

    private int maxScore = 0;

    private void Awake()
    {
       

        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        
        maxScore = PlayerPrefs.GetInt("maxscore", 0);
        pointText.text = "0";

        maxScoreText.text = "Max Score - " + maxScore;
    }

    public void AddPoint()
    {
        points++;
        pointText.text = points.ToString();

        if (points > maxScore)
        {
            maxScore = points;
            PlayerPrefs.SetInt("maxscore", points);
            maxScoreText.text = "Max Score - " + maxScore;
        }
    }

   
        
    

   
}