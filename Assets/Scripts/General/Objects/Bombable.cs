using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombable : MonoBehaviour
{
    public float Durability = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bomb")
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2(Mathf.Sign(transform.position.x - collision.transform.position.x), Mathf.Sign(transform.position.y - collision.transform.position.y)) * Mathf.PI * Mathf.PI * Mathf.PI / Durability / 2;
        }
    }
}
