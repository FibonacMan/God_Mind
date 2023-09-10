using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public float speed;
    Vector3 FirstPoint;
    public bool Left;
    bool Bolay;
    private void Start()
    {
        FirstPoint = transform.localPosition;
    }
    void Update()
    {
        if (Bolay != Left)
        {
            Bolay = Left;
            if (Left)
            {
                StartCoroutine(KillMe());
            }
            if (!Left)
            {
                transform.localScale = Vector3.one;
                transform.localPosition = FirstPoint;
            }
        }
    }
    IEnumerator KillMe()
    {
        for(int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, FirstPoint - Vector3.up * 200,speed);
            transform.localScale *= 0.99f;
        }
        transform.localScale = Vector3.zero;
    }
}
