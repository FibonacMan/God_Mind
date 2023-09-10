using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveNewMissionInGame : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<MissionManager>().UpdateTheMissionInGame();
            gameObject.SetActive(false);
        }
    }
}
