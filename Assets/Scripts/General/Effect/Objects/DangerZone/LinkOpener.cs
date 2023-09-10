using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    public static void GoLink(string link)
    {
        Application.OpenURL(link);
    }
    public static void Shut()
    {
        Application.Quit();
    }
}
