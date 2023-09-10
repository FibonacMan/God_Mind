using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearitorByRoom : MonoBehaviour
{
    Vector3 firstScale;
    public int Room;
    private void Start()
    {
        firstScale = transform.localScale;
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Room").transform.GetChild(Room).gameObject.activeSelf)
        {
            transform.localScale = firstScale;
            if (TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                if (audioSource.playOnAwake && !audioSource.isPlaying) audioSource.Play();
            }
        }
        else
        {
            transform.localScale = Vector3.zero;
            if (TryGetComponent<AudioSource>(out AudioSource audioSource)) if (audioSource.playOnAwake && audioSource.isPlaying) audioSource.Stop();
        }
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = GameObject.FindGameObjectWithTag("Room").transform.GetChild(Room).gameObject.activeSelf;
        }
    }
}
