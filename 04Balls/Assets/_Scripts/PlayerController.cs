using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigibody;

    public float moveForce;
    

    private GameObject focalPoint;

    public bool hasPowerUp;
    public float powerUpForce;
    public float powerUpTime;

    public GameObject[] powerUpIndicators;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Guardamos en una variable el valor del movimiento vertical
        float forwardInput = Input.GetAxis("Vertical");
        _rigibody.AddForce(focalPoint.transform.forward * moveForce * forwardInput);

        //Bucle para que la variable que se contenga dentro de los powerUpIndicators siga la posición siempre de nuestro
        //jugador
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = this.transform.position;
        }

        if (this.transform.position.y < -10)
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    /// <summary>
    /// Función para el powerup, para cuando se recoja active la función del powerup correspondiente, este se encarga de
    /// llamarlos y destruir el gameobject una vez recogido
    /// </summary>
    /// <param name="other">Es el nombre con quien se colosionará con el otro</param>
    private void OnTriggerEnter(Collider other)
    {
        //Si el otro tiene la etiqueta "PowerUp"
        if (other.CompareTag("PowerUp"))
        {
            //"Tener el powerup" será verdadero
            hasPowerUp = true;
            
            //Destruiremos el otro gameobject (el powerup)
            Destroy(other.gameObject);
            
            //Inicializaremos una corutina con la variable
            StartCoroutine(PowerUpCountdown());
        }
    }

    /// <summary>
    ///Función para dar funcionamiento a los powerups
    /// </summary>
    /// <param name="otherCollision"> Es la colisión del otro game object</param>
    private void OnCollisionEnter(Collision otherCollision)
    {
        //Si la otra colisión tiene la etiqueta "Enemy" y nosotros tenemos el powerup...
        if (otherCollision.gameObject.CompareTag(("Enemy")) && hasPowerUp)
        {
            
            //Guardamos en una variable de Rigybody, que contendrá el valor del rigibody del otro que haya colisionado
            //con nosotros.
            Rigidbody enemyRigibody = otherCollision.gameObject.GetComponent<Rigidbody>();
            
            //Guardamos en una variable "fuera de nuestro jugaor" la resta de la posición del otro - la nuestra
            Vector3 awayFromPlayer = otherCollision.gameObject.transform.position - this.transform.position;
            
            //Al enemigo (ru rigibody) le añadimos una fuerza hacia afuera (será la dirreción la variable "awayFromPlayer"
            //multiplicado por la fuerza "powerUpForce" que con esta conseguiremos que el impulso hacia afuera sea notorio
            enemyRigibody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Función de espera para avisar y cambiar entre diferentes fragmentos de tiempos que esté activado un powerup
    /// Además de tener el tiempo que contendrá el powerup hasta que finalice
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerUpCountdown()
    {
        //Para cada GameObject llamado indicador en poweUpIndictors
        foreach (GameObject indicator in powerUpIndicators)
        {
            //Su gameObject si está activo ponerlo en visible (true)
            indicator.gameObject.SetActive(true);
            //Después de un tiempo
            yield return new WaitForSecondsRealtime(powerUpTime / powerUpIndicators.Length);
            //Ponerlo en no visible (no true)
            indicator.gameObject.SetActive(false);
            
            //Este bucle se repetirá hasta pasar por toda la array de powerUpIndicators
        }
        hasPowerUp = false;
    }
}
