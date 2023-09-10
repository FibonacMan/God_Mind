using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : MonoBehaviour
{
    public float Size;
    public SpriteRenderer Back;
    bool Changable = true;
    public Color Mood;
    void Update()
    {
        Back.color = Mood;
        if (Changable) StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        Changable = false;
        Back.sprite = Transformation.TextureToSprite(Transformation.RandomizedTexture(Color.gray, new Vector2(180, 120), Random.Range(0, 100), Size));
        yield return new WaitForSeconds(0.1f);
        Changable = true;
    }
}
