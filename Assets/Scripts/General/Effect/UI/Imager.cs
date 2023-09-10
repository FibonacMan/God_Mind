using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Imager : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<Image>().sprite!=null) GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<Image>().rectTransform.rect.width, (GetComponent<Image>().rectTransform.rect.width / GetComponent<Image>().sprite.rect.width) * GetComponent<Image>().sprite.rect.height);
    }
}