using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public float Health;
    public bool Life;
    public float LegStrenght = 10f;
    public float Speed;
    bool Walk;
    int Destinition;
    bool Randomizable = true;
    bool Jumpable = true;
    public GameObject CurrentRoom;
    public Vector2 Borders;
    bool Killed;
    float FlyPower;
    bool OneTime;
    void Update()
    {
        if (!Killed)
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
                    if(Borders!=Vector2.zero) if(transform.position.x > Borders.x && transform.position.x < Borders.y) transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * Destinition, Speed);
                    else transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * Destinition, Speed);
                }
                else
                {
                }
                if (Randomizable) StartCoroutine(Randomize());
                if (Jumpable) StartCoroutine(Jump());
                GetComponent<SpriteRenderer>().flipX = Destinition == 1;
                if (GetComponent<Rigidbody2D>().velocity.y <= 0.5f) GetComponent<Animator>().SetInteger("Act", 0);
                else GetComponent<Animator>().SetInteger("Act", 1);
            }
            if (Health <= 0 && Life && !OneTime)
            {
                OneTime = true;
                StartCoroutine(KillMe());
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            GetComponent<Collider2D>().isTrigger = true;
            if (Jumpable)
            {
                for (int i = 0; i < 3 * transform.localScale.x; i++) StartCoroutine(Jump());
            }
            if (Randomizable)
            {
                for (int i = 0; i < 3 * transform.localScale.x; i++) StartCoroutine(Randomize());
            }
        }
    }
    private void OnBecameInvisible()
    {
        if(Killed)
        {
            gameObject.SetActive(false);
        }
    }
    void Run(int NewDestinition)
    {
        Walk = true;
        Destinition = NewDestinition;
    }
    IEnumerator KillMe()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = Color.clear;
        ParticleSystem DieEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        DieEffect.transform.parent = transform.parent;
        DieEffect.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    IEnumerator Randomize()
    {
        Randomizable = false;
        Run(Random.Range(0, 2) * 2 - 1);
        FlyPower = Random.Range(LegStrenght, LegStrenght * 2);
        if (Random.Range(0, 2) == 0)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        Randomizable = true;
    }
    IEnumerator Jump()
    {
        Jumpable = false;
        GetComponent<Rigidbody2D>().velocity += Vector2.up * FlyPower;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        Jumpable = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damager>(out Damager New))
        {
            if (Life)
            {
                Health -= New.Power;
            }
            else
            {
                Killed = true;
            }
        }
    }
}
