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
        /*Transladamos hacia la izquierda la izquierda del universo (no la del objeto ya que como gira su izquierda estaría cambiando constantemente) * velocidad de
        translamiento * segundos de la vida real.
        */
        
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime); //El objeto con este script rodará hacia arriba * velocidad de roatación * segundo de la vida real
    }
}
