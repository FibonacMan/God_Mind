using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSizer : MonoBehaviour
{
    public Transform SizeByMe;
    public Vector2 Gap;
    void Update()
    {
        if(SizeByMe!=null)GetComponent<AudioSource>().volume = Gap.x / Mathf.Clamp(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position),Gap.x,Gap.y) * SizeByMe.transform.localPosition.x;
        else GetComponent<AudioSource>().volume = Gap.x / Mathf.Clamp(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position), Gap.x, Gap.y);
    }
}
