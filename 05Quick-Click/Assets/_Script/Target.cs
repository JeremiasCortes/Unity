#if UNITY_IOS || UNITY_ANDROID
#define USING_MOBILE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rigibody;

    private float minForce = 9f, 
        maxForce = 14f,
        maxTorque = 12f, 
        xRange = 4f, 
        ySpawnPos = -1f;

    private GameManager gameManager;

    [Range(-100,15)]
    public int pointValue;

    public ParticleSystem explosionParticle;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _rigibody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigibody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();

        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Genera un Vector aleatorio en tres dimensiones
    /// </summary>
    /// <returns>Fuerza aleatoria hacia arriba</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Genera un número aleatorio float
    /// </summary>
    /// <returns>Devuelve un número aleatorio entre -maxTorque y maxTorque</returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Genera una posición aleatoria en 3D, con la coordenada "z" = 0</returns>
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    //Si se está usando el móvil...
#if USING_MOBILE
    private void OnMouseEnter()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (gameObject.CompareTag("Good"))
            {
                gameManager._audioSource.PlayOneShot(gameManager.goodSound, 1);
            }
        }

        if (gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

#else //Sino es móvil...
       private void OnMouseOver()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            if (gameObject.CompareTag("Good"))
            {
                gameManager._audioSource.PlayOneShot(gameManager.goodSound, 1);
            }
        }

        if (gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    
#endif

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Good"))
        {
            gameManager.GameOver();
            gameManager.UpdateScore(-pointValue);
        }
    }
}
