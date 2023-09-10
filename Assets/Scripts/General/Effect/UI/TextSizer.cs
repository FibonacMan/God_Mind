using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextSizer : MonoBehaviour
{
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, GetComponent<Text>().text.Split(' ').Length * GetComponent<Text>().fontSize / 10f);
    }
}
