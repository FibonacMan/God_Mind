using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1Act : MonoBehaviour
{
    public int Type;
    bool NoRepeat;
    private void Update()
    {
        if (Type == 0)
        {
            if (!transform.GetChild(0).GetComponent<GleeMan>().InteractAgain && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission1>().MouseSpawn();
            }
        }
    }
}