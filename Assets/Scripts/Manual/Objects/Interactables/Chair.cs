using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform ChangeStuff;
    float Nowly;
    void Update()
    {
        if (Nowly != transform.localPosition.x)
        {
            Nowly = transform.localPosition.x;
            ChangeStuff.localPosition = new Vector3(transform.localPosition.x, ChangeStuff.localPosition.y, ChangeStuff.localPosition.z);
        }
        if (transform.parent.tag != "Full")
        {
            transform.parent.tag = "Goal";
        }
    }
}
