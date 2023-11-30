    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecicionsScript : MonoBehaviour
{

    public bool willItRainToday = false;


    // Start is called before the first frame update
    void Start()
    {
        /*if (willItRainToday)
        {
            Debug.Log("No te olvides agarrar el paraguas");
        }
        else
        {
            Debug.Log("No lo cojas el paraguas hombre, hace soleado");
        }*/

        /*if (!willItRainToday)
        {
            Debug.Log("Vamos al cine")
        }*/

        bool iAmLate = true;
        bool isThereSomeTrafic = false;
        bool iAmHungry = true;
        bool kidsAreHungry = false;

        if (iAmLate && !isThereSomeTrafic)//Si uno de los dos es verdadero, se activa el if
        {
            Debug.Log("Dale al gas que llego tarde");
        }
        else
        {
            Debug.Log("No le doy al gas porque no llego tarde o hay tráfico");
        }

        int maxSpeed = 100;

        if (maxSpeed == 120)
        {
            Debug.Log("Podemos ir a fondo"); }
        else if (maxSpeed < 120 && maxSpeed >= 60) {
            Debug.Log("Podemos ir a velocidad de crucero");
        }
        else if (maxSpeed<60 && maxSpeed>=40)
        {
            Debug.Log("Debemos ir a velocidad de ciudad");
        }
        else
        {
            Debug.Log("Mejor vamos dando un paseo");
        }



        if (iAmHungry || kidsAreHungry)
            {
                Debug.Log("Vamos a hacer la comida");
            }
            else
            {
                Debug.Log("Nadie tiene hambre");
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
