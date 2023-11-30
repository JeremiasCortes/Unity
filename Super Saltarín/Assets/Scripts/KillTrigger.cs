using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si lo otro que ha colisionado tiene la siguiente etiqueta..
        if (other.tag == "Player")
        {
            // Llamamos a la función Kill
            PlayerController.sharedInstance.Kill();
        }
    }
}
