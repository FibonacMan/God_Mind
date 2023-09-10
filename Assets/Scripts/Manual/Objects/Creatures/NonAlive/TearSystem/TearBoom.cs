using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearBoom : MonoBehaviour
{
    bool OneTime;
    void Update()
    {
        if (transform.localScale.x == 1 && !OneTime)
        {
            OneTime = true;
            GetComponent<ParticleSystem>().Play();
        }
    }
}
