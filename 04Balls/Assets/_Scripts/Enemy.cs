using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Guardamos en una variable la dirección a la cuál tiene que ir el enemigo para que pueda perseguir al jugador
        /*Esto se hace agarrando la posición del player y se le resta la del enemigo, después se normaliza
        para que que el valor sea 1 y no más de ello*/
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized;
        
        //Al _rigibody le añadimos fuerza, la cantidad será la dirección que es 1 * la cantidad de fuerza
        _rigidbody.AddForce(moveForce * lookDirection, ForceMode.Force);
        
        if (this.transform.position.y < -10) 
        {
            Destroy(gameObject);
        }
    }
}
