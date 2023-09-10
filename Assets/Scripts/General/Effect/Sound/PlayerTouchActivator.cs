using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchActivator : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(PlayIt());
        }
    }
    IEnumerator PlayIt()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Collider2D>().enabled = false;
        if (GetComponent<AudioSource>().loop)
        {
            Destroy(GetComponent<Collider2D>());
        }
        else
        {
            yield return new WaitWhile(() => GetComponent<AudioSource>().isPlaying);
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
