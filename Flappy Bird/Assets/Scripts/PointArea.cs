using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D otherCollider2D)
    {
        if (otherCollider2D.tag == "Player")
        {
            UIManager.sharedInstance.AddPoint();
        }
    }
}
