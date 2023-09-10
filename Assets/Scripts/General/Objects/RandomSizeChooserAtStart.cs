using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSizeChooserAtStart : MonoBehaviour
{
    void OnEnable()
    {
        transform.localScale *= Random.Range(0.75f, 1.25f);
    }
}
