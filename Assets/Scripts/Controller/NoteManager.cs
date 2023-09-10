using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class NoteManager : MonoBehaviour
{
    public Text Paper;
    public Text PaperTitle;
    public bool[] MyNotes;
    public Transform ButtonTransform;
    public void GetNote(int NoteID)
    {
        Array.Resize(ref MyNotes, transform.GetChild(0).GetComponent<TeAsKeeper>().Assets.Length);
        MyNotes[NoteID] = true;
        ButtonTransform.GetChild(NoteID).GetChild(0).GetComponent<Image>().color = Color.clear;
    }
    public void SetNote(int NoteID)
    {
        if (MyNotes.Length > NoteID)
        {
            if (MyNotes[NoteID])
            {
                Paper.text = transform.GetChild(0).GetComponent<TeAsKeeper>().Assets[NoteID].text;
                PaperTitle.text = transform.GetChild(1).GetComponent<TeAsKeeper>().Assets[NoteID].text;
            }
        }
    }
}