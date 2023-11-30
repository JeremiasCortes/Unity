using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad de movimiento arriba/abajo de las tuberías")]
    private float _speed = 0;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField]
    [Tooltip("Tiempo de cambio para las tuberías")]
    private float switchTime = 2f;

    private float distanceToDestroy = 30f;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

        // Invocamos el método cada x segundos
        InvokeRepeating("SwitchMovement", 0, switchTime);
    }

    private void Update()
    {
        float xCam = Camera.main.transform.position.x;
        float xPipe = this.transform.position.x;

        if (xCam > xPipe + distanceToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    void SwitchMovement()
    {
        GetComponent<Rigidbody2D>().velocity *= -1;
    }
}
