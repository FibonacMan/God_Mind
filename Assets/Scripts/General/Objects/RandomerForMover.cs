using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyParallax;
public class RandomerForMover : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<SpriteMovement>().speed *= Random.Range(0.5f, 1.5f);
    }
}
