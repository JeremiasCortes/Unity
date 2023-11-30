using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movingForward;

    private void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        //Si tiene la siguiente etiqueta...
        if (otherCollider2D.tag == "rockL")
        {
            // Avanzar� a la derecha
            Enemy.turnAround = true;

        }
        // Si tiene la siguiente etiqueta...
        else if (otherCollider2D.tag == "rockR")
        {
            // Avanzar� a la izquierda
            Enemy.turnAround = false;
        }
    }
}
