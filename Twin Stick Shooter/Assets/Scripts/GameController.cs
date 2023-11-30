using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public Transform enemy;

    [Header("Oleadas de enemigos")]
    public float timeBeforeSpawing = 1.5f;
    public float timeBetweenEnemies = 0.25f;
    public float timeBetweenWaves = 2f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    [Header("Intervas gráfica de usuario")]
    private int score = 0;
    private int wave = 0;

    public int _wave
    {
        get { return wave; }
        set { wave = value; }
    }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    // Start is called before the first frame update
    void Start()
    {
        // Llamamos a la corutina
        StartCoroutine(SpawnEnemies());
    }

    // Creamos una corutina
    IEnumerator SpawnEnemies()
    {
        // Indicamos que querremos que espere antes de continuar con el arranque del juego
        yield return new WaitForSeconds(timeBeforeSpawing);

        //Después de ese tiempo de espera inicial, arrancamos con el bucle de las oleadas
        while (true)
        {
            //No crees nada hasta que la oleada de enemigos previa haya sido eliminada
            if (currentNumberOfEnemies <= 0)
            {
                IncreaseWave();
                //No quedan enemigos, hay que crear nuevos enemigos
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    //Generamos aleatoriamente el enemigo fuera de la pantalla  
                    float randDistance = Random.Range(25, 25); // Distancia de aparición
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position; // Posición del Game Controller (0,0,0)
                    enemyPos.x = randDirection.x * randDistance;
                    enemyPos.y = randDirection.y * randDistance;

                    // Instanciamos al enemigo en la pantalla
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    // Indicamos que hay un nuevo enemigo en pantalla
                    currentNumberOfEnemies++;
                    // Indicamos a la corutina que duerma un corto perirodo
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }

            /* Si llego aquí, es que aún tengo enemigos, le inidico al bucle principal
             * que espere más
             */
            yield return new WaitForSeconds(timeBeforeSpawing);
        }
    }

    public void KillEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increaseScore)
    {
        this.score += increaseScore;
        this.scoreText.text = "Score: " + this.score;
    }

    void IncreaseWave()
    {
        this.wave++;
        this.waveText.text = "Wave: " + this.wave;
    }
}
