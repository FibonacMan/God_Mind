using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public AudioSource SoundEffect;
    public Color FirstColor;
    public float Health;
    public float Speed;
    public float WalkDist;
    Vector3[] PosToWalk = new Vector3[2];
    public Vector3 FirstPos;
    bool Left;
    private void OnEnable()
    {
        Left = new bool[2] { true, false }[(int)Random.Range(0, 2)];
    }
    void Update()
    {
        PosToWalk[0] = FirstPos + Vector3.right * WalkDist;
        PosToWalk[1] = FirstPos - Vector3.right * WalkDist;
        if (Left)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, PosToWalk[0] + Vector3.right * WalkDist, Speed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, PosToWalk[1] - Vector3.right * WalkDist, Speed);
        }
        GetComponent<SpriteRenderer>().flipX = Left;
        if (Vector3.Distance(FirstPos, transform.localPosition) > WalkDist)
        {
            Left = !Left;
        }
        if (Health <= 0)
        {
            ParticleSystem Dead = transform.GetChild(0).GetComponent<ParticleSystem>();
            Dead.transform.parent = null;
            Dead.Play();
            FindObjectOfType<Mission1>().KilledMouse();
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damager>(out Damager New))
        {
            Health -= New.Power;
            StartCoroutine(Damaged());
        }
    }
    IEnumerator Damaged()
    {
        SoundEffect.Play();
        SpriteRenderer MyMe = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 7; i++)
        {
            MyMe.color = Color.white - MyMe.color + Color.black;
            yield return new WaitForSeconds(0.1f);
        }
        MyMe.color = FirstColor;
    }
}
