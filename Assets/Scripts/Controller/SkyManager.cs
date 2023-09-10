using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;
public class SkyManager : MonoBehaviour
{
    [Header("All")]
    public int[] BlackBacks;
    public int[] NaturalBacks;
    public int[] DreamBacks;
    public int[] RedBacks;
    public int[] SunBacks;
    [Header("Day")]
    public Color DayColor;
    public Color DarkDayColor;
    public Vector2[] GapForDayColor;
    [Header("Red")]
    public Color[] RedColors;
    [Header("All")]
    public int Size;
    int Room;
    bool ReturnInUpdate;
    GameObject Back;
    bool Returnable;
    [Header("Sun")]
    public GameObject Sun;
    private void Update()
    {
        if (ReturnInUpdate && Returnable)
        {
            StartCoroutine(PlayInReturn());
        }
    }
    public void Skyler()
    {
        Room = (int)GameObject.FindGameObjectWithTag("Norm").transform.localPosition.x;
        Play(Room);
    }
    public void Play(int RoomID)
    {
        Returnable = true;
        Back = GameObject.FindGameObjectWithTag("Back");
        if (Array.Exists(BlackBacks, element => element == RoomID))
        {
            Back.GetComponent<SpriteRenderer>().color = Color.black;
            GameObject.FindGameObjectWithTag("Star").GetComponent<Light2D>().intensity = 0.2f;
            ReturnInUpdate = false;
        }
        else if (Array.Exists(NaturalBacks, element => element == RoomID))
        {
            Back.GetComponent<SpriteRenderer>().color = DayColor;
            GameObject.FindGameObjectWithTag("Star").GetComponent<Light2D>().intensity = 0.35f;
            ReturnInUpdate = true;
        }
        else if (Array.Exists(DreamBacks, element => element == RoomID))
        {
            Back.GetComponent<SpriteRenderer>().color = Color.clear;
            GameObject.FindGameObjectWithTag("Star").GetComponent<Light2D>().intensity = 0.15f;
            ReturnInUpdate = false;
        }
        else if (Array.Exists(RedBacks, element => element == RoomID))
        {
            Back.GetComponent<SpriteRenderer>().color = RedColors[(int)Mathf.Clamp(GameObject.FindGameObjectWithTag("Time").transform.localPosition.y, 0, RedColors.Length - 1)];
            GameObject.FindGameObjectWithTag("Star").GetComponent<Light2D>().intensity = 0.1f;
            ReturnInUpdate = true;
        }
        Sun.SetActive(Array.Exists(SunBacks, element => element == RoomID));
    }
    IEnumerator PlayInReturn()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Returnable = false;
        if (Array.Exists(NaturalBacks, element => element == Room))
        {
            Vector2 Dist=GapForDayColor[Array.Find(RedBacks, element => element == Room)];
            if (Player.transform.position.x < Dist.x) Back.GetComponent<SpriteRenderer>().color = DayColor;
            else if (Player.transform.position.x > Dist.y) Back.GetComponent<SpriteRenderer>().color = DarkDayColor;
            else Back.GetComponent<SpriteRenderer>().color = DarkDayColor * (Player.transform.position.x - Dist.x) / (Dist.y - Dist.x) + DayColor * (Dist.y - Player.transform.position.x) / (Dist.y - Dist.x);
            Back.GetComponent<SpriteRenderer>().sprite = Transformation.TextureToSprite(Transformation.RandomizedTexture(Color.gray, new Vector2(100, 100), Player.transform.position.x / 100, Size));
        }
        else if (Array.Exists(RedBacks, element => element == Room))
        {
            Back.GetComponent<SpriteRenderer>().color = RedColors[(int)Mathf.Clamp(GameObject.FindGameObjectWithTag("Time").transform.localPosition.y, 0, RedColors.Length - 1)];
            Back.GetComponent<SpriteRenderer>().sprite = Transformation.TextureToSprite(Transformation.RandomizedTexture(Color.gray, new Vector2(100, 100), Player.transform.position.x / 100, Size));
        }
        yield return new WaitForSeconds(0.1f);
        Returnable = true;
    }
}