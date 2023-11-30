using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; //Creamos una variable que será de las 3 coordenadas
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //Cuando se empieza la variable tendrá las coordenadas de donde se encuentra el objeto con este srcipt
        repeatWidth = GetComponent<BoxCollider>().size.x/2; //La variable tendrá el tamaño del collider de este objeto con el script y su tamaño será dividio entre 2
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.x - transform.position.x > repeatWidth) //si la posición de inicio en la x restado por la posición en el update de x es mayor que la variable
        {
            transform.position = startPos; //Se moverá el objeto a la posición inicial
        }
    }
}
