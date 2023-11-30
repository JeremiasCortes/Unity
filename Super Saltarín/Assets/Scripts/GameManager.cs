using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


// Posibles estados del juego
public enum GameState{
    Menu,
    inGame,
    gameOver
}


public class GameManager : MonoBehaviour
{
    //Variable que referencia al propio GameManager
    public static GameManager sharedInstance;

    //Variable para saber en qué estado del juego nos encontramos ahora mismo
    //AL inicio, queremos que empiece en el menú principal
    public GameState currentGameState = GameState.Menu;

    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    public int collectedScore = 0;

    public int addPointsToScore = 0;


    private void Awake()
    {
        sharedInstance = this;
        
    }

    private void Start()
    {
        //Empezamos el juego en el menu
        BackToMenu();
        
    }

    private void Update()
    {
        // Si pulsamos el botón Start y no estamos jugando pondremos el juego en marcha
        if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        {
            // Llamamos a la función responsable de arrancar el juego
            StartGame();
        // Pero si se pulsa el botón de pausa...
        } else if (Input.GetButtonDown("Pause"))
        {
            // Volveremos al menu
            BackToMenu();
        }

        // Si se cumple lo siguiente...
        if (this.currentGameState != GameState.gameOver && this.currentGameState != GameState.Menu && Input.GetButtonDown("Cancel"))
        {
            // Saldremos del juego
            ExitGame();
        }
    }

    
    /// <summary>
    /// Método encargado de iniciar el juego
    /// </summary>
    public void StartGame()
    {
        // Establece el estado del juego
        SetStateGame(GameState.inGame);

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();

        // Si la posición del jugador es mayor que...
        if (PlayerController.sharedInstance.transform.position.x > 10)
        {
            // Removemos todos los bloques y generamos el bloque inicial
            LevelGenerator.sharedInstance.RemoveAllBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }

        // Llamamos al StartGame() del PlayerController
        PlayerController.sharedInstance.StartGame();

   

        // Reseteamos valores
        collectedScore = 0;
        addPointsToScore = 0;
    }


    /// <summary>
    /// Método encargado de finalizar el juego
    /// </summary>
    public void GameOver()
    {
        // Establece el estado del juego
        SetStateGame(GameState.gameOver);
    }


    /// <summary>
    /// Método para volver al menú principal cuando el usuario lo quiera hacer
    /// </summary>
    public void BackToMenu()
    {
        // Establece el estado del juego
        SetStateGame(GameState.Menu);
    }

    /// <summary>
    /// Método para finalizar la ejecución del videojuego
    /// </summary>
    public void ExitGame()
    {
        // Si es está en el UNITY_EDITOR...
        #if UNITY_EDITOR
            // Ajustaremos que se podrá cerrar el modo de juego al salir del juego (con Esc o un botón)
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Sino se cumple la condición anterior se hará un salir de la app
            Application.Quit();
        #endif
    }

    /// <summary>
    /// Método encargado de cambiar el estado del juego
    /// </summary>
    /// <param name="newGameState">Le llega el estado al cuál deberá ponerse</param>
    void SetStateGame(GameState newGameState)
    {
        // Si es Menu...
        if (newGameState == GameState.Menu)
        {
            // Aquí se prepara la escena para mostrar el Menú
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

            // Congelamos el tiempo
            PlayerController.sharedInstance.ToggleTimeFreeze(false);
        }
        // Si es inGame...
        else if (newGameState == GameState.inGame)
        {
            // Aquí se prepara la escena para mostrar el Juego
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;

            // Descongelamos el tiempo
            PlayerController.sharedInstance.ToggleTimeFreeze(true);
        }
        // Si es gameOver...
        else if (newGameState == GameState.gameOver)
        {
            // Aquí se prepara la escena para mostrar el Game Over
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;

            // Congelamos el tiempo
            PlayerController.sharedInstance.ToggleTimeFreeze(false);
        }

        // Asignamos el estado del juego actual
        this.currentGameState = newGameState;
    }

    /// <summary>
    /// Función que se encarga de calcular el Score
    /// </summary>
    /// <param name="objectValue">Valor del Score a añadir</param>
    public void CollectObject(int objectValue)
    {
        // Al Score recolectado le subimos el valor a añadir
        this.collectedScore += objectValue;
    }

    /// <summary>
    /// Función que se encarga de calcular los puntos del Score
    /// </summary>
    /// <param name="valueAddPointsScore">Valor de los Puntos del Score a añadir</param>
    /// <returns></returns>
    public float AddPointsScore(int valueAddPointsScore)
    {
        // A los puntos del score le subimos el valor a añadir
        this.addPointsToScore += valueAddPointsScore;
        // Devolvemos los puntos que se tienen que añadir
        return (float)this.addPointsToScore;
    }
}
