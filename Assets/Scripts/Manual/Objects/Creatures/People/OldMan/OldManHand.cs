using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManHand : MonoBehaviour
{
    private void Update()
    {
        if (FindObjectOfType<OldMan>().HandCatched) GameObject.FindGameObjectWithTag("Player").transform.position = transform.position - Vector3.right;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<OldMan>().HandCatched = true;
        }
    }
}
