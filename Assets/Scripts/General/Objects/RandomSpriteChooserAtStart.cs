using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteChooserAtStart : MonoBehaviour
{
    public Sprite[] Sprites;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
    }
}
