using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float lifetime = 2f; // Tiempo de vida de la bala
    public float speed = 5f; // Velocidad de la bala
    public int damage = 1; // Daó


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // s = v * t
        this.transform.Translate(Vector3.up*speed * Time.deltaTime);
    }
}
