using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesScript : MonoBehaviour
{
    [Tooltip("Cuando daño recibirá el enemigo")]
    public float damage;
    [Tooltip("Velocidad a la que se mueve el proyectil")]
    public float speed = 1f;
    [Tooltip("Dirección en la que apunta el proyectil")]
    public Vector3 direction;
    [Tooltip("Tiempo de vida del proyectil en segundos")]
    public float lifeDuration = 10f;



    private Rigidbody2D rb2D;


    // Use this for initialization
    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();

        //Normalizamos la dirección del proyectil -> tamaño = 1
        direction = direction.normalized;

        //Rotamos el proyectil
        float angleRad = Mathf.Atan2(-direction.y, direction.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);

        //Pre programar destrucción del proyectil
        Destroy(gameObject, lifeDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // s = v * t
        //transform.position += ( speed * direction ) * Time.deltaTime;
        rb2D.MovePosition(transform.position + (speed * direction) * Time.fixedDeltaTime);

    }
}

