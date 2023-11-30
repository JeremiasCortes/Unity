using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position =new Vector3(target.position.x,
                                             this.transform.position.y,
                                             this.transform.position.z);
    }
}
