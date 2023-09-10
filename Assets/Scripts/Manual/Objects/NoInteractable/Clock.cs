using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float Division;
    void Update()
    {
        if (Division > 1) transform.localEulerAngles = Vector3.forward * (90 - (Mathf.Floor(GameObject.FindGameObjectWithTag("Time").transform.localPosition.x * Division) - Mathf.Floor(GameObject.FindGameObjectWithTag("Time").transform.localPosition.x) * Division) / 25 * 360);
        else transform.localEulerAngles = Vector3.forward * (90 - (Mathf.Floor(GameObject.FindGameObjectWithTag("Time").transform.localPosition.x * Division) / Division) / 25 * 360);
    }
}