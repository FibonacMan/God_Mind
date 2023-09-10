using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShokingImager : MonoBehaviour
{
    void Update()
    {
        transform.localPosition += Vector3.right * Random.Range(-20f, 20f) + Vector3.up * Random.Range(-20f, 20f);
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -25f, 25f), Mathf.Clamp(transform.localPosition.y, -25f, 25f));
    }
}
