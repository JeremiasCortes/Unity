using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PandaScript : MonoBehaviour
{

    [Tooltip("Vida del panda")]
    public float health = 45f;
    public float currentHealth;
    [Tooltip("Velocidad del panda")]
    public float speed = 5f;
    [Tooltip("Daño que hacemos a la tarta cuando el panda llega a ella")]
    public int cakeEatenPerBite = 5;

    private Animator animator;

    private Rigidbody2D rb2D;

    //Hashings representando los tres nombres de los triggers del animator del panda
    private int animatorDieTriggerHash = Animator.StringToHash("DieTrigger");
    private int animatorEatTriggerHash = Animator.StringToHash("EatTrigger");
    private int animatorHitTriggerHash = Animator.StringToHash("HitTrigger");

    public TowerScript towerScript;

    //Variable compartida por todos los pandas con la información necesaria de waypoints
    private static GameManager gameManager;

    //Waypoint actual al que dirigirse el panda del 0 al 11
    //private int currentWaypointNumber;

    //Waypoint actual en el que se dirige el panda
    private Waypoint currentWaypoint;


    //Umbral a partir del cual consideramos que ya hemos alcanzado el waypoint
    private const float waypointThreshold = 0.01f;

    // Use this for initialization
    void Start()
    {
        currentHealth = health;
        towerScript = FindObjectOfType<TowerScript>();
        
        //Solo instanciamos el Game Manager si nadie lo ha instanciado previamente
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        currentWaypoint = gameManager.firstWaypoint;

        //Obtengo una referencia a la propia componente del panda, de tipo Animator
        animator = GetComponent<Animator>();
        //Obtenemos referencia a nuestro rigidbody 2D
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Comprobar si el panda ha llegado al waypoint del final (a la tarta) 
        //Si es asi, hay que lanzar la animación de comer y además, eliminar el script actual del panda 
        if (currentWaypoint == null)
        {
            Eat();
            return;
        }
        //Si el panda no está en el final, hay que calcular la distancia que hay entre la posición actual del panda y el waypoint al que se dirige
        float distance = Vector2.Distance(this.transform.position,
                                            currentWaypoint.GetPosition());
        //Si el panda está lo suficientemente cerca del waypoint, ir a por el siguiente waypoint
        if (distance <= waypointThreshold)
        {
            currentWaypoint = currentWaypoint.GetNextWaypoint();
        }
        else
        {
            MoveTowards(currentWaypoint.GetPosition());
        }
    }



    private void MoveTowards(Vector3 destination)
    {

        //s = v * t
        float step = speed * Time.fixedDeltaTime;
        //this.transform.position = Vector3.MoveTowards(this.transform.position, destination, step);
        rb2D.MovePosition(Vector3.MoveTowards(this.transform.position, destination, step));

    }

    private bool isDead = false;

    

    private void Hit(float damage)
    {
        if (!isDead)
        {
            //Resto el daño de mi vida actual
            currentHealth -= damage;

            if (currentHealth > 0) //estoy vivo aún
            {
                this.animator.SetTrigger(animatorHitTriggerHash);
            }
            else //currentHealth <=0 -> estoy muerto 
            {
                this.animator.SetTrigger(animatorDieTriggerHash);
                isDead = true;
                speed = -1;
                //Tenemos que añadir azúcar al medidor
                gameManager.OneMorePandaInHell();
            }
        }
    }

    private void Eat()
    {
        this.animator.SetTrigger(animatorEatTriggerHash);
        gameManager.BiteTheCake(cakeEatenPerBite);
        Destroy(this);
    }


    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Projectile")
        {
            Hit(otherCollider.GetComponent<ProjectilesScript>().damage);
        }
    }
}
