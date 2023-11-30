using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidbody2D;

    public static bool turnAround;

    private Vector3 startPosition;

    // Despertar
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Guardar la posición de cuando despertó
        startPosition = this.transform.position;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        /*
         * Sistema con el cuál está programado el movimiento de izquierdas y derecha del enemigo, además
         * está preparado que tambien e gire la posición para que mire al sitio correcto
         */
        float currentRunningSpeed = runningSpeed;

        if (turnAround)
        {
            // Aquí la velocidad es positiva
            this.transform.eulerAngles = new Vector3 (0f, 180.0f, 0f);
        }
        else
        {
            // Giramos el enemigo
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // Aquí la velocidad es negativa
            currentRunningSpeed = -runningSpeed;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
                rigidbody2D.velocity = new Vector2(currentRunningSpeed, rigidbody2D.velocity.y);   
        }
    }
}
