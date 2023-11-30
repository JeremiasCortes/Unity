using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
   
    public TextMeshProUGUI collectableLabel, scoreLabel, maxScoreLabel;

    

    // Update is called once per frame
    void Update()
    {
        // Si estamos en los siguientes estados...
        if (GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            // Obtenemos la cantidad del score y la mostramos
            int currentObjects = GameManager.sharedInstance.collectedScore;
            this.collectableLabel.text = currentObjects.ToString();
        }

        // Si estamos en el siguiente estado
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Obtenemos la distancia recorrida (la puntuación) y la mostramos
            float travelledDistance = PlayerController.sharedInstance.GetDistance() * 15f + (GameManager.sharedInstance.addPointsToScore);
            this.scoreLabel.text = "Score\n" + travelledDistance.ToString("f0");

            // Obtenemos el maxScore y lo mostramos
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0);
            this.maxScoreLabel.text = "MaxScore:\n" + maxScore.ToString("f0");
        }
    }
}
