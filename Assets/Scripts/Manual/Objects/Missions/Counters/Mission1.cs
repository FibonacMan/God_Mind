using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    int MouseIndex=0;
    public GameObject Key;
    public GameObject CellarDoor;
    bool OneTime;
    public GameObject MainUI;
    public GameObject Credits;
    private void Update()
    {
        if (FindObjectOfType<MissionManager>().SectionGhostier().name == "Section10")
        {
            CellarDoor.tag = "Door";
        }
        if (FindObjectOfType<MissionManager>().SectionGhostier().name == "Section11" && !OneTime)
        {
            OneTime = true;
            MainUI.SetActive(false);
            Credits.SetActive(true);
        }
    }
    public void KilledMouse()
    {
        MouseIndex++;
        if (MouseIndex >= 3)
        {
            Key.SetActive(true);
        }
    }
    public GameObject[] Mice;
    public void MouseSpawn()
    {
        foreach(GameObject Mouse in Mice)
        {
            Mouse.SetActive(true);
        }
    }
}
