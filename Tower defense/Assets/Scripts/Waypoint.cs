using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField]
    private Waypoint nextWaypoint;

    //Devuelve la posición del waypoint actual
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    //Devuelve el siguiente punto de ruta
    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }




}