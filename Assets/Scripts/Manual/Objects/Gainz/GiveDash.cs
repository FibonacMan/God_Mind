using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDash : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<GratiasMovement>().DashTrue = true;
            gameObject.SetActive(false);
        }
    }
}
