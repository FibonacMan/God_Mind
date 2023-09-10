using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class UpAndDownLightEffect : MonoBehaviour
{
    int Direction = 1;
    public float Speed;
    public float Ratio;
    float MaxIntensity;
    private void Start()
    {
        MaxIntensity = GetComponent<Light2D>().intensity;
    }
    void Update()
    {
        GetComponent<Light2D>().intensity+=Speed * Direction;
        if (GetComponent<Light2D>().intensity > MaxIntensity)
        {
            Direction = -1;
        }
        if (GetComponent<Light2D>().intensity < MaxIntensity * Ratio)
        {
            Direction = 1;
        }
    }
}
