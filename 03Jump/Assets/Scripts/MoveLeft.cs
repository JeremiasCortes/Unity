using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 30;

    public float speedAumento = 100;
    private PlayerController _playerController; //Del script "PlayerController" lo metemos en una variable llama "_playerController"

    private void Start()
    {
        //En la varibale "_playerController" meteremos el script (componente) PlayerController que est� dentro del Game Object Playeer
        _playerController = GameObject.Find("Player") //Hasta aqu� tenemos el Game Object despu�s abajo continuamos a tener
            .GetComponent<PlayerController>(); //El componente llamado "PlayerController" con el cu�l podremos acceder a todo el componente
        //_playerController = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver) { //Si gameOver no es verdadera (porque el signo ! lo contradice) se ejecuta el movimiento a la izquierda

            transform.Translate(Time.deltaTime * speed * Vector3.left);
        }

        speed += Time.deltaTime/speedAumento;
    }
}
