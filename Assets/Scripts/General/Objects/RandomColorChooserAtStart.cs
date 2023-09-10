using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorChooserAtStart : MonoBehaviour
{
    public static Color[] colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.red + Color.blue,
        Color.green+Color.blue,
        (Color.red+Color.white)/2f,
        Color.red+Color.yellow,
        (Color.blue+Color.white)/2f
    };
    private void OnEnable()
    {
        colors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.red + Color.blue,
        Color.green+Color.blue,
        (Color.red+Color.white)/2f,
        Color.red+Color.yellow,
        (Color.blue+Color.white)/2f
    };
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }
    private void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }
}