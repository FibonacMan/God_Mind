using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float TimeSpeed;
    bool Timable = true;
    void Update()
    {
        if (Timable) StartCoroutine(TimeUp());
        if (transform.localPosition.x > 27.5f)
        {
            transform.localPosition += Vector3.up;
            transform.localPosition = Vector3.zero;
        }
    }
    IEnumerator TimeUp()
    {
        Timable = false;
        transform.localPosition += Vector3.right * TimeSpeed * Random.Range(0.95f, 1.05f);
        yield return new WaitForSeconds(Mathf.PI * Mathf.PI / 55f);
        Timable = true;
    }
}
