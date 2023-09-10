using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<GratiasMovement>().AttackTrue = true;
            gameObject.SetActive(false);
        }
    }
}
