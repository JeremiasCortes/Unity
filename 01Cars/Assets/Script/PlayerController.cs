using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Propiedades
    //[HideInInspector] // La siguiente línea aún siendo publica no saldrá en el inspector (Unity)
    [Range(0, 20), SerializeField, 
     Tooltip("Velocidad actual del coche")] //Hacer que en el inspector (Unity) solo se pueda editar entre esos valores
    //SerializedField lo que hace es que aún siendo privada la siguiente línea saldrá en el inspector pero no podrá ser accesible con otro script
    private float speed = 10f;
    
    [Range(0,90), SerializeField,
    Tooltip("Velocidad de giro")]
    private float turnSpeed = 45f;

    private float horizontalInput, verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        // Movimiento del vehículo.
        // S = s0 + V*t*(dirección

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        this.transform.Translate(translation:speed*Time.deltaTime*Vector3.forward*verticalInput); //0,0,1
        this.transform.Rotate(turnSpeed*Time.deltaTime*Vector3.up*horizontalInput);
        
    }
}
