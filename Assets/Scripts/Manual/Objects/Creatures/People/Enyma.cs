using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enyma : MonoBehaviour
{
    public float Speed;
    public Vector3 Destination;
    public float DeltaSpeed;
    public bool Ghost;
    void Update()
    {
        DeltaSpeed = Mathf.Clamp(Speed / Vector3.Distance(transform.position, Destination), 0.01f, 1);
        if(Ghost) GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        else GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Destination != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Destination.x, Destination.y), DeltaSpeed);
        }
        GetComponent<SpriteRenderer>().flipX = !Transformation.FloatToBoolian(transform.position.x - Destination.x);
        if (Vector3.Distance(transform.position, Destination) < 3f || Destination == Vector3.zero)
        {
            SetIntForPlayer(0);
        }
        else
        {
            SetIntForPlayer(1);
        }
    }
    void SetIntForPlayer(int Int, string Name) //EnymaAnim
    {
        GetComponent<Animator>().SetInteger(Name, Int);
    }
    void SetIntForPlayer(int Int) //EnymaAnim
    {
        GetComponent<Animator>().SetInteger("Act", Int);
    }
}