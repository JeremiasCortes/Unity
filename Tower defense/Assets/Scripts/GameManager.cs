//#if UNITY_IOS || UNITY_ANDROID
//#define USING_MOBILE
//#endif

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //Tenemos acceso al primer waypoint
    public Waypoint firstWaypoint;

    //Variable para saber donde spawnear los panda
    private Transform spawnPoint;

    [Header("Pandas y oleadas")]
    //Objeto que spawnearemos en las oleadas
    public GameObject pandaprefab;
    //N�mero de oleadas
    public int numberOfWaves;
    //N�mero de enemigos por oleada
    public int numberOfPandasPerWave;

    //Referecnia al medidor de az�car para aumentar cuando matemos a un panda
    private static SugarMeterScript sugarMeter;

    //Variable booleana para saber si el rat�n se halla sobre la zona donde poder poner torreta
    private bool _isPointerOnAllowedArea = false;
    //Funci�n que devuelve el valor anterior
    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    public PandaScript pandaScript;

    private int suggarExtra;

    private HealthBarScript healthBarScript;

    private bool canPlantTurret = false; // Variable para saber si se puede plantar una torreta

//#if USING_MOBILE
    /* private void Update()
    {
        if (Input.touchCount > 0)
        {
             Touch touch = Input.GetTouch(0); // Obtener el primer toque (�ndice 0)

            if (touch.phase == TouchPhase.Began)
            {
                // Obtener la posici�n del toque en el mundo (convertirla de pantalla a mundo)
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Verificar si el toque ha ocurrido dentro del colisionador del "trigger"
                if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
                {
                    _isPointerOnAllowedArea = true;
                }
            }
        }
    } */
//#else
    //Se llama autom�ticamente cuando el rat�n entra dentro de alguno de los collider del Game Manager
    private void OnMouseEnter()
    {
        //Como estoy dentro del collider, puedo plantar torretas
        _isPointerOnAllowedArea = true;
    }

    //Se llama autom�ticamente cuando el rat�n sale del collider del Game Manager
    private void OnMouseExit()
    {
        //Como estoy fuera del collider, no puedo plantar nada
        _isPointerOnAllowedArea = false;
    }
//#endif



    // // // // // //
    // Game Over //
    // // // // // //

    [Header("Pantallas de Game Over")]
    public GameObject winnignScreen;
    public GameObject losingScreen;

    //Referencia a la barra de vida del castillo de az�car...
    private HealthBarScript playerHealth;
    //N�mero de pandas que nos quedan a�n por derrotar y ganar el nivel
    private int numberOfPandasToDefeat;

    //Variable que contiene el valor de aumento de la velocidad m�xima de los pandas
    public float aumentoDeVidaMaxima;

    private void Start()
    {
        if (healthBarScript == null)
        {
            healthBarScript = FindObjectOfType<HealthBarScript>();
        }

        if (pandaScript == null)
        {
            pandaScript = pandaprefab.GetComponent<PandaScript>();
        }

        pandaScript.health = 7.5f;
        pandaScript.speed = 1.2f;

        //Recuperamos una referecnia de la barra de vida del jugador
        playerHealth = FindObjectOfType<HealthBarScript>();
        //Recuperamos el objeto SpawnPoint
        spawnPoint = GameObject.Find("Spawning Point").transform;
        StartCoroutine(WavesSpawn());

        if (sugarMeter == null )
        {
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    /*M�todo que ser� llamado cuando se cumpla las condiciones bien, porque el jugador gane derrotando todas las oleadas o bien
     * porque se haya quedado sin vida en su castillo de az�car*/
    /// <summary>
    /// Funci�n que finaliza el juego, tanto como si ha perdido o ganado
    /// </summary>
    /// <param name="playerHasWon">Variable booleana para saber si ha ganado (true) o no (false)</param>
    private void GameOver(bool playerHasWon)
    {
        //Comprobamos si el jugador ha ganado o no, para activar una pantalla o otra
        if (playerHasWon)
        {
            winnignScreen.SetActive(true);
        } else
        {
            losingScreen.SetActive(true);
        }

        //Congelamos el tiempo pars que se pare el vidoejuego por detr�s de las escenas.
        Time.timeScale = 0;
    }

    //Funci�n que llamamos cada vez que matamos a un Panda...
    public void OneMorePandaInHell()
    {
        int suggarGanada = suggarExtra + 1;
        if (suggarGanada >= 3)
        {
            suggarGanada = 3;
        }
        numberOfPandasToDefeat--;
        sugarMeter.AddSugar(suggarGanada);
        suggarGanada = 0;
    }

    //Funci�n que da�a la vida del jugador cuando el panda de come la tarta
    //Monitorizar� adem�s de si todav�a le queda vida, si se nos agota, llama�ra al game over con el par�metro en false
    public void BiteTheCake(int damage)
    {
        //Lo primero es hacer da�o a la barra de vida  y saber si a�n queda tarta por comer
        bool isCakeAllEaten = playerHealth.ApplyDamage(damage);

        //Si los pandas se han comido toda la tarta -> Game Over, hemos perdido
        if (isCakeAllEaten)
        {
            GameOver(false);
        }

        //Como hay un panda menos (porque cuando come, explota), lo notificaremos al Game Manager
        OneMorePandaInHell();
    }

    //Corutina que crear� oleadas de enemigos
    private IEnumerator WavesSpawn()
    {
        //Para cada oleada
        for (int i = 0; i < numberOfWaves; i++)
        {
            

            //Llamamos a la rutina PandaSpawner para que gestione la oleada en cuesti�n y esperamos a ques est� haya concluido
            yield return PandaSpawner();

            //Cuando la corrutina acaba puedo incrementar la cantidad de pandas para ls iguiente oleada
            numberOfPandasPerWave += 1;
        }

        //Si hemos acabado con todas las hordas, hay que llamar a Game Over y decidir que hemos ganado el juego
        GameOver(true);
    }

    //Corutina que crea los pandas de una oleada simple y espera que no queda ninguno
    private IEnumerator PandaSpawner()
    {
        //Tengo que derrotar tantos pandas como indiquela oleada actual
        numberOfPandasToDefeat = numberOfPandasPerWave;

       


        PandaScript pandaScript = pandaprefab.GetComponent<PandaScript>();
        if (pandaScript != null)
        {
            pandaScript.health += aumentoDeVidaMaxima;
            pandaScript.speed += Random.Range(0.05f, 0.125f);
        }

        
        
        //Vamos a generar progesivamente los pandas de la oleada
        for (int i = 0; i < numberOfPandasPerWave; i++)
        {
            //INstanciamos el panda, en la posici�n del spawner y sin rotar nada...
            Instantiate(pandaprefab, spawnPoint.position, Quaternion.identity);

            //Indico a la corutina que se duerma  este tiempo
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        }


        //Una vez todos los pandas se han spawneado, esperar a que todos hayan sido derrotadospor el jugador o bien no pueda derrotarlos y pierda
        yield return new WaitUntil(() => numberOfPandasToDefeat <= 0);
        suggarExtra++;
        healthBarScript.currentHealth += 2;
        healthBarScript.updateHealthBar();
    }

    
}
