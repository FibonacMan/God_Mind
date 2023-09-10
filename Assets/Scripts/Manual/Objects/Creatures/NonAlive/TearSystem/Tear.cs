using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name!=gameObject.name)
        {
            GameObject Spawned = Instantiate(transform.GetChild(1).gameObject, transform.position, Quaternion.identity);
            Spawned.transform.localScale = Vector3.one * 0.025f;
            Spawned = Instantiate(transform.GetChild(2).gameObject, transform.position, Quaternion.identity);
            Spawned.transform.localScale = Vector3.one;
            Destroy(gameObject);
        }
    }
}