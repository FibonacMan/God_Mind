using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrails : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<SpriteRenderer>().color == Color.clear)
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().widthMultiplier = 0;
            transform.GetChild(1).GetComponent<TrailRenderer>().widthMultiplier = 0;
        }
        else
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().widthMultiplier = 0.75f;
            transform.GetChild(1).GetComponent<TrailRenderer>().widthMultiplier = 0.75f;
        }
    }
}
