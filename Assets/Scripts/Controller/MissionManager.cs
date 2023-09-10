using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class MissionManager : MonoBehaviour
{
    public GameObject[] Missioner;
    public GameObject Sectioner;
    public int NowlyMission;
    public int NowlySection;
    public int MID;
    public string[] MissionNamesENG;
    public string[] MissionNamesTR;
    public string[] MissionNames;
    public TextMeshProUGUI text;
    private void Update()
    {
        if (FindObjectOfType<OptionsManager>().Language == "English") MissionNames = MissionNamesENG;
        if (FindObjectOfType<OptionsManager>().Language == "Turkish") MissionNames = MissionNamesTR;
    }
    public GameObject SectionGhostier()
    {
        return Sectioner.transform.GetChild(NowlySection).gameObject;
    }
    private void Start()
    {
        SetSection(0);
    }
    public void UpdateTheMission()
    {
        NowlyMission++;
        Missioner[NowlyMission - 1].SetActive(false);
        Missioner[NowlyMission].SetActive(true);
    }
    public void UpdateTheMissionInGame()
    {
        MID++;
        text.text = MissionNames[MID];
    }
    public void UpdateTheSection()
    {
        NowlySection++;
        for (int i = 0; i < NowlySection; i++)
        {
            Sectioner.transform.GetChild(i).gameObject.SetActive(false);
        }
        Sectioner.transform.GetChild(NowlySection).gameObject.SetActive(true);
    }
    public void SetSection(int Value)
    {
        for (int i = 0; i < Sectioner.transform.childCount; i++)
        {
            Sectioner.transform.GetChild(i).gameObject.SetActive(i == Value);
        }
    }
    public static void MoveSomething(GameObject ObjectToMove, Vector3 Position)
    {
        ObjectToMove.transform.position = Position;
    }
    public static void SwitchTag(GameObject ObjectToMove, string Tag)
    {
        ObjectToMove.tag = Tag;
    }
}
