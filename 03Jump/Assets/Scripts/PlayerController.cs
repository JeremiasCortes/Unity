using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    // Una constante es una variable que no se le puede cambiar nunca su valor
    //Hacemos que estas frases estén dentro de una variable para no tener problemas con los espacios a la hora de utilizarlas
    const string SPEED_MULTIPLIER = "Speed Multiplier";
    const string SPEED_F = "Speed_f";
    const string JUMP_TRIGGER = "Jump_trig";
    const string DEATH_B = "Death_b";
    const string DEATH_TYPE_INT = "DeathType_int";

    private Rigidbody playerRb; //Agarramos la componente Rigibody de nuestro personaje y la guardamos en una variable
    public int jumForce = 10; //Fuerza de salto
    public float gravityMultiplier; //Variable del multiplicador de gravedad
    public bool isOnGround = true; //Variable para saber si está tocando suelo
    private bool _gameOver; //booleana de game over
    public bool GameOver { get => _gameOver; } //Como el game over es privada haremos que tenga una "copia" que será pública y tendrá el mismo valor que la privada

    private Animator _animator; //Agarramos la componente Animator en una variable

    public ParticleSystem explosion; //Agarramos la componente ParticleSystem en una variable
    public ParticleSystem correr;

    public AudioClip jumpSound, crashSound; //Creamos dos variables de AudioClip donde se encontrarán los efectos de sonido correspondientes
    private AudioSource _audioSource; //Agarramos la componente AudioClip en una variable

    [Range(0,1)] public float audioVolume = 1.0f; //Nivel de volumen

    private float speedMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>(); //Hacemos el la variable creada al inicio tenga la componente

        playerRb = GetComponent<Rigidbody>();

        Physics.gravity = gravityMultiplier * new Vector3(0, -9.81f, 0); //Al empezar la gravedad será la multiplicación de la gravedad y el multiplicador

        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime/2; //Ecucación de multiplicación de aumento de velocidad (para animación)
        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier); //Velocidad en la que la animación irá yendo más rápido
        _animator.SetFloat(SPEED_F, 1);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround==true) { //Si se salta (para saltar se debe haber tocado suelo) activamos el condicional
            playerRb.AddForce(Vector3.up * jumForce, ForceMode.Impulse); //F = m*aceleration ecucación del salto que será una fuerza añadida
            isOnGround = false; //Además de saltar se hará falsa la variable de tocar suelo para que no pueda hacer doble salto
            _animator.SetTrigger(JUMP_TRIGGER); //Se mostrará la animación de saltar
            
            correr.Stop(); //Dejará de lanzar las partículas de correr

            _audioSource.PlayOneShot(jumpSound, audioVolume); //Hará el sonido de la variable salto con el sonido a la cantidad de la variable
        }
    }

    private void OnCollisionEnter(Collision other) //Colisiones con otros
    {
        if (other.gameObject.CompareTag("Ground") && !GameOver) //Si el otro tiene tag suelo y el GameOver es falso (o sea, no se ha perdido) activamos if
        {
            isOnGround=true; //La variable de si tocabamos suelo pasa a ser verdadera para poder volver a saltar
            correr.Play(); //Se activarán las partículas de correr
        }

        if (other.gameObject.CompareTag("Obstacle")) //Pero si el otro tiene el tag de obstáculo
        {
            _gameOver = true; //EL game over se activará

            explosion.Play(); //Se hará unas partículas de explosion
            correr.Stop(); //Se quitará las partículas de correr

            _animator.SetBool(DEATH_B, true); //La animación de muerte será verdadera (se activará)
            _animator.SetInteger(DEATH_TYPE_INT, Random.Range(1, 3)); //Se verá la animación de muerte entre 1 y 2, el 3 no entra

            _audioSource.PlayOneShot(crashSound, audioVolume); //Hará el sonido de la variable salto con el sonido a la cantidad de la variable

            Invoke("RestartGame", 3.0f);
        }
    }

    void RestartGame()
    {
        speedMultiplier = 1; //Que el multiplicador de velocidad de la animación vuelva a valer 1
        SceneManager.LoadScene("Prototype 3");
    }
}
