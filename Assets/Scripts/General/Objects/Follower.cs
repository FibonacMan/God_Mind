using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform Target;
    public bool OnlyX;
    public Transform DistanceByTransform;
    public Vector3 Distance;
    public bool Spritable;
    public float Speed = 0.75f;
    float FirstStartY;
    private void Start()
    {
        if (OnlyX) FirstStartY = transform.position.y;
    }
    void Update()
    {
        if (DistanceByTransform != null)
        {
            Distance = DistanceByTransform.localPosition;
            transform.localEulerAngles = DistanceByTransform.localEulerAngles;
        }
        if (OnlyX) transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, FirstStartY, transform.position.z) + new Vector3(Distance.x * Mathf.Sign(Target.localScale.x), Distance.y * Mathf.Sign(Target.localScale.y)), Speed);
        else transform.position = Vector3.Lerp(transform.position, Target.position + new Vector3(Distance.x * Mathf.Sign(Target.localScale.x), Distance.y * Mathf.Sign(Target.localScale.y)), Speed);
        if (Spritable)
        {
            GetComponent<SpriteRenderer>().flipY = transform.localEulerAngles.z == 270;
        }
    }
}