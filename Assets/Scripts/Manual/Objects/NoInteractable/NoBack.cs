using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBack : MonoBehaviour
{
    public GameObject Target;
    public float Distance;
    float targetNowlyX;
    void Update()
    {
        if (targetNowlyX < Target.transform.position.x)
        {
            targetNowlyX = Target.transform.position.x;
        }
        transform.position = (targetNowlyX + Distance) * Vector3.right + transform.position.y * Vector3.up;
    }
}