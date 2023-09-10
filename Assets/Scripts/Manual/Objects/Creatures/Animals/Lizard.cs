using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{

    public float Speed;
    bool Walk;
    Vector3 Destinition;
    bool Randomizable = true;
    bool Jumpable = true;
    public GameObject CurrentRoom;
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = CurrentRoom.activeSelf;
        GetComponent<Collider2D>().enabled = CurrentRoom.activeSelf;
        if (!CurrentRoom.activeSelf)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).localPosition = Vector3.right * 1000;
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).localPosition = Vector3.zero;
            }
            if (Walk)
            {
                transform.position = Vector3.Lerp(transform.position, Destinition, Speed);
                GetComponent<Animator>().SetInteger("Act", 1);
            }
            else
            {
                GetComponent<Animator>().SetInteger("Act", 0);
            }
            if (Randomizable) StartCoroutine(Randomize());
            if (Jumpable) StartCoroutine(Jump());
            GetComponent<SpriteRenderer>().flipX = transform.position.x < Destinition.x;
        }
    }
    void Run(Vector3 NewDestinition)
    {
        Walk = true;
        Destinition = NewDestinition;
    }
    IEnumerator Randomize()
    {
        Randomizable = false;
        Run(transform.position + Vector3.right * (Random.Range(0, 2) * 2 - 1) * 50);
        if (Random.Range(0, 2) == 0)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }
        yield return new WaitForSeconds(5);
        Randomizable = true;
    }
    IEnumerator Jump()
    {
        Jumpable = false;
        GetComponent<Rigidbody2D>().velocity += Vector2.up * 10;
        yield return new WaitForSeconds(Random.Range(3f, 7.5f));
        Jumpable = true;
    }
}
