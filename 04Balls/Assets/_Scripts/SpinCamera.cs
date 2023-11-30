using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCamera : MonoBehaviour
{
    public float speedRotate;
    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, speedRotate * horizontalInput * Time.deltaTime);
    }
}
