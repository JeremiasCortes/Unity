using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        // Si atraviesa la zona el player...
        if (otherCollider2D.tag == "Player")
        {
            // En cuanto crucemos la zona...
            // Añadimos un nuevo nivel de bloque
            LevelGenerator.sharedInstance.AddLevelBlock();
            // Quitamos un bloque de nivel viejo
            LevelGenerator.sharedInstance.RemoveOldestLevelBlock();
        }
        
    }
}
