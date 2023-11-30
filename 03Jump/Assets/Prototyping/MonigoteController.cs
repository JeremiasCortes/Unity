using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonigoteController : MonoBehaviour
{
    private Animator _animator; //Llamamos a Animator y lo guardamos en _animator
    private const string MOVE_HANDS = "Move Hands"; //Utilizaremos MOVE_HANDS para referirnos a "Move Hands"
    // Es constante para que nunca podamos cambiarla de valor
    private bool isMovingHands = false; //La opción booleana será falsa

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();  //En _animator guardaremos el Componente de Animator
        _animator.SetBool(MOVE_HANDS, isMovingHands); // Establecemos como booleano la animación MOVE_HANDS en el valor original que es falso
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Si le damos a espacio
        {
            isMovingHands = !isMovingHands; //isMovingHands valdrá lo contrario
            _animator.SetBool(MOVE_HANDS, isMovingHands); //y se establece el nuevo valor
        }
    }
}
