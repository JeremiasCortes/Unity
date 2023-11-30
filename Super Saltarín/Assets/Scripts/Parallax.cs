using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0f;

    public float speed{
        get
        {
            return _speed;
        } 
        set 
        { 
            _speed = value; 
        }
    }
    
    private Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.rigidbody2D.velocity = new Vector2(speed, 0);

        float parentPosition = this.transform.parent.transform.position.x;

        if (this.transform.position.x - parentPosition >= 10.23293f)
        {
            this.transform.position = new Vector3(parentPosition-10.23293f, this.transform.position.y, this.transform.position.z);
        }
    }
}
