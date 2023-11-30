using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;
    

    public List<GameObject> targetPrefabs;
    public float spawnRate = 1.5f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int _score;
    private int Score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 999);
        }
        get
        {
            return _score;
        }
    }

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public GameObject titleScreen;

    private const string Max_Score = "MAX_SCORE";

    private int numberOfLives = 6;
    public List<GameObject> lives;

    private float timerLeft2 = 65;
    
    private void Start()
    {
        ShowMaximScore();

        _audioSource = GetComponent<AudioSource>();
    }

    public AudioClip goodSound, badSound;
    internal AudioSource _audioSource;

    /// <summary>
    /// Método que inicia la partida cambiando el valor del estado del juego
    /// </summary>
    /// <param name="difficulty">Número entero que indica la dificultad del juego</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        
        titleScreen.SetActive(false);

        timerLeft2 -= difficulty * 5;
        spawnRate /= difficulty;
        numberOfLives -= difficulty;

        for (int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }
        
        StartCoroutine(SpawnTarget());
        
        Score = 0;
        UpdateScore(Score);
    }

    private void Update()
    {
        if (gameState == GameState.inGame)
        {
            timerLeft2 -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.Round(timerLeft2));
            if (timerLeft2<=0)
            {
                GameOver();
            }
        }
    }


    /// <summary>
    /// Corutina que va spawneando aleatoriamente los targets (objetos buenos y malos)
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    /// <summary>
    /// Actualiza la información del score y lo muestra en la pantalla
    /// </summary>
    /// <param name="scoreAdd">Número de puntos a añadir a la puntuación global</param>
    public void UpdateScore(int scoreAdd)
    {
        Score += scoreAdd;
        scoreText.text = "Score: " + Score;
        
    }

    public void ShowMaximScore()
    {
        int maxScore = PlayerPrefs.GetInt(Max_Score, 0);
        scoreText.text = "Maximos puntos: " + maxScore;
        
        
    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(Max_Score, 0);
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt(Max_Score, Score);
        }
    }
    
    public void GameOver()
    {
        numberOfLives--;

        if (numberOfLives >=0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            Color tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
        
        
        
        if (numberOfLives<=0)
        {
            SetMaxScore();
        
            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
