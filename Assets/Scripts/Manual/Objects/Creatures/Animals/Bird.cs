using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float Speed;
    bool Walk;
    int Destinition;
    bool Randomizable = true;
    bool Jumpable = true;
    public GameObject CurrentRoom;
    public Vector3 House;
    float FlyPower;
    bool Sleeping;
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = CurrentRoom.activeSelf;
        GetComponent<Collider2D>().enabled = CurrentRoom.activeSelf;
        if (!CurrentRoom.activeSelf || Sleeping)
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
            if (!Sleeping)
            {
                if (Walk)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * Destinition, Speed);
                }
                else
                {
                }
                if (Randomizable) StartCoroutine(Randomize());
                if (Jumpable) StartCoroutine(Jump());
                GetComponent<SpriteRenderer>().flipX = Destinition == 1;
                GetComponent<Animator>().SetInteger("Act", (int)Mathf.Abs(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y)));
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, House, Speed / Vector3.Distance(transform.localPosition, House));
                if(Vector3.Distance(transform.localPosition, House)>1) GetComponent<Animator>().SetInteger("Act", 1);
                else GetComponent<Animator>().SetInteger("Act", 0);
                GetComponent<SpriteRenderer>().flipX = Mathf.Sign(transform.localPosition.x - House.x) == 1;
            }
        }
    }
    void Run(int NewDestinition)
    {
        Walk = true;
        Destinition = NewDestinition;
    }
    IEnumerator Randomize()
    {
        Randomizable = false;
        Run(Random.Range(0,2) * 2 - 1);
        FlyPower = Random.Range(10f, 20f);
        if (Random.Range(0, 2) == 0)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }
        if (Random.Range(0f, 1f) < 0.25f)
        {
            StartCoroutine(Sleep());
        }
        yield return new WaitForSeconds(3);
        Randomizable = true;
    }
    IEnumerator Sleep()
    {
        Sleeping = true;
        yield return new WaitForSeconds(10f);
        Sleeping = false;
    }
    IEnumerator Jump()
    {
        Jumpable = false;
        GetComponent<Rigidbody2D>().velocity += Vector2.up * FlyPower;
        yield return new WaitForSeconds(2);
        Jumpable = true;
    }
}
