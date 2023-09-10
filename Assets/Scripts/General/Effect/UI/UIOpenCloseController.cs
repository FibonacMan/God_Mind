using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpenCloseController : MonoBehaviour
{
    public bool Open;
    public void Minimal(Transform Target)
    {
        if (Open)
        {
            Target.localScale = Vector3.one;
        }
        else
        {
            Target.localScale = Vector3.zero;
        }
    }
}
