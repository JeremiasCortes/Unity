using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotateSpeed = 60;
    public float translateSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.left * translateSpeed * Time.deltaTime; 
        /*Transladamos hacia la izquierda la izquierda del universo (no la del objeto ya que como gira su izquierda estar�a cambiando constantemente) * velocidad de
        translamiento * segundos de la vida real.
        */
        
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime); //El objeto con este script rodar� hacia arriba * velocidad de roataci�n * segundo de la vida real
    }
}
