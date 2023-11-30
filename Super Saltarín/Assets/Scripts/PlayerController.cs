using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    [SerializeField]
    [Tooltip("Fuerza de salto")]
    private float jumpForce = 5f;

    private Rigidbody2D rigidbody;

    //Sirve para detectar la variable del suelo
    public LayerMask groundLayer;

    public Animator animator;

    [SerializeField]
    [Tooltip("Velocidad de correr")]
    private float runningSpeed = 1.5f;

    private Vector3 startPosition;

    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 50, INITIAL_MANA = 15, MAXIM_HEALTH = 100, MAXIM_MANA = 100, MIN_HEALTH = 25;
    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE_MULTIPLIER = 1.5f;

    bool isSuperJump = false;

    public AudioClip jumpSound;

    // Variable de estado para rastrear si ya se ha realizado un salto en el frame actual
    

    private void Awake()
    {
        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();

        // Toma el valor de donde empieza el personaje la primera vez
        startPosition = this.transform.position;

        AudioSource audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        // Empezamos con las siguientes animaciones...
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);

        // Cada vez que reiniciemos el personaje empezar� en la posici�n inicial
        this.transform.position = startPosition;

        // Establecemos los puntos iniciales respectivos
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        // Inicializamos la corutina
        StartCoroutine("TirePlayer");
    }

    /// <summary>
    /// Corutina que va disminuyendo cada x tiempo puntos de...
    /// </summary>
    /// <returns></returns>
    IEnumerator TirePlayer()
    {
        // Mientras tengamos vida suficiente (mayor de x cantidad)...
        while (this.healthPoints > MIN_HEALTH)
        {
            // Quitar un punto
            this.healthPoints--;
            // Poner corutina a descansar x tiempo
            yield return new WaitForSeconds(1f);
        }

        // Estamos aqu� cuando la vida ya no se deba de bajar m�s
        // Cerramos la corutina
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        // Solo dejaremos que los controles funcionen si estamos en el estado "inGame"
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Si se pulsa el bot�n...
            if (Input.GetButtonDown("a"))
            {
                // Si no se ha realizado un salto en este frame, permitir el salto y marcar como realizado


                Jump(false);


            }
            // Si se pulsa el bot�n...
            else if (Input.GetButtonDown("JumpHight"))
            {
                // Si no se ha realizado un salto en este frame, permitir el salto y marcar como realizado


                Jump(true);


            }
        }
        //En cada frame revisamos si se est� tocando el suelo.
        animator.SetBool("isGrounded", IsTouchingTheGround());
    }

    void FixedUpdate()
    {
        //Solo nos movemos si estamos en el estado "inGame"
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (rigidbody.velocity.x < runningSpeed)
            {
                rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y); //Velocidad en el eje de las X y de las Y
            }
        }

        
    }

    

    /// <summary>
    /// Funci�n de saltar
    /// </summary>
    /// <param name="isSuperJump"> Variable para saber si aplicar un salto o otro</param>
    void Jump(bool isSuperJump)
    {
        // Si se est� tocando suelo
        if (IsTouchingTheGround())
        {
            

            
           
            if (!isSuperJump) 
            {
                // Fuerza = massa * aceleraci�n ===> a = F/m
                // Hacemos un salto normal
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                AudioSource audio = GetComponent<AudioSource>();
                if (audio != null && this.jumpSound != null)
                {
                    audio.PlayOneShot(this.jumpSound);
                }

            }
            // Si el valor llamado a la funci�n de verdadera y hay mana suficiente...
            else if (isSuperJump == true && this.manaPoints >= SUPERJUMP_COST)
            {
                // Descontamos mana
                manaPoints -= SUPERJUMP_COST;

                // Fuerza = massa * aceleraci�n ===> a = F/m
                // Aplicamos un salto m�s fuerte
                rigidbody.AddForce(Vector2.up * jumpForce * SUPERJUMP_FORCE_MULTIPLIER, ForceMode2D.Impulse);
                AudioSource audio = GetComponent<AudioSource>();
                if (audio != null && this.jumpSound != null)
                {
                    audio.PlayOneShot(this.jumpSound);
                }

            }


        }
    }

    /// <summary>
    /// Funci�n para saber si el jugador est� tocando el suelo o no
    /// </summary>
    bool IsTouchingTheGround()
    {
        //Devuelve true si estamos tocando el suelo y false en el caso contrario.
        //Lanazamos un rayo hacia abajo desde la posici�n del jugador, donde si choca contra la capa de suelo...
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer))
        {
            //Aqu�, s� estamos tocando con el suelo
            return true;
        } else
        {
            //No estamos tocando suelo
            return false;
        }
    }

    /// <summary>
    /// Funci�n para matar al jugador
    /// </summary>
    public void Kill()
    {
        // LLamamos al gameOver()
        GameManager.sharedInstance.GameOver();
        // Establecemos la animaci�n de morir
        this.animator.SetBool("isAlive", false);

        // Guardamos el maxscore
        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0);
        // Guardamos datos de puntuaci�n
        float variable = this.GetDistance() * 15 + GameManager.sharedInstance.addPointsToScore;

        // Si la puntuaci�n es mayor que el maxScore
        if (currentMaxScore < variable) 
        {
            // Tenemos nuevo record
            PlayerPrefs.SetFloat("maxscore", variable);
        }

        // Parramos la corutina
        StopCoroutine("TirePlayer");
    }

    /// <summary>
    /// Funci�n para obtener la distancia recorrida
    /// </summary>
    /// <returns></returns>
    public float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x,0), 
                                                   new Vector2(this.transform.position.x,0));
        return travelledDistance;
    }

    /// <summary>
    /// Funci�n que calcula la vida que tenemos actualmente
    /// </summary>
    /// <param name="points">Cantidad a restar o a�adir</param>
    public void CollectHealth(int points)
    {
        // A�adimos/Restamos la cantidad que se tenga que calcular
        this.healthPoints += points;
        // Si intentamos superar el m�ximo de cantidad
        if (healthPoints >= MAXIM_HEALTH)
        {
            // No lo permitimos, establecemos en todo momento el m�ximo
            this.healthPoints = MAXIM_HEALTH;
        }
    }

    /// <summary>
    /// Funci�n que calcula el mana que tenemos
    /// </summary>
    /// <param name="points">Cantidad a restar o a�adir</param>
    public void CollectMana(int points)
    {
        // A�adimos/Restamos la cantidad que se tenga que calcular
        this.manaPoints += points;
        // Si intentamos superar el m�ximo de cantidad
        if (manaPoints >= MAXIM_MANA)
        {
            // No lo permitimos, establecemos en todo momento el m�ximo
            this.manaPoints = MAXIM_MANA;
        }
    }

    // Funci�n para congelar o descongelar el tiempo
    public void ToggleTimeFreeze(bool isTimeFrozen)
    {
        // Si la variable recibida es true...
        if (isTimeFrozen)
        {
            // El tiempo no se congela
            Time.timeScale = 1f; // Restaurar el tiempo a su velocidad normal (1x)
        }
        else // Si la variable es false
        {
            // Congelamos el tiempo
            Time.timeScale = 0f; // Congelar el tiempo
        }

        // Al final de todo, giramos el valor recibido
        isTimeFrozen = !isTimeFrozen;
    }

    /// <summary>
    /// Funci�n que devuelve la cantidad de vida
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return this.healthPoints;
    }

    /// <summary>
    /// Funci�n que devuelve la cantidad de mana
    /// </summary>
    /// <returns></returns>
    public int GetMana()
    {
        return this.manaPoints;
    }

    
    private void OnTriggerEnter2D(Collider2D othercollider2D)
    {
        // Si se choca con la siguiente etiqueta...
        if (othercollider2D.tag == "Enemy")
        {
            // Se reta lo siguiente...
            this.healthPoints -= 13;
        }

        // Si se choca con la siguiente etiqueta...
        if (othercollider2D.tag == "rock")
        {
            // Se reta lo siguiente...
            this.healthPoints -= 5;
        }

        // Si al chocar con algo la cantidad de vida y mientras se est� en juego baja la vida a o menos de 0...
        if(this.healthPoints <= 0 && GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Llamamos la funci�n de matar al jugador
            Kill();
        }
    }
}
