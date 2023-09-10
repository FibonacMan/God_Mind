using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContentSizer : MonoBehaviour
{
    public RectTransform[] ObjectsForSize;
    private void Update()
    {
        float Sizer=0;
        foreach(RectTransform NowRect in ObjectsForSize)
        {
            Sizer += NowRect.sizeDelta.y;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, Sizer);
    }
}
