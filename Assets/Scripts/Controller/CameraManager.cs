using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Transform Player;
    Transform Cam;
    Vector2 Maxs;
    Vector2 Mays;
    public Transform Borders;
    Vector3 Distance = new Vector3(0, 10);
    public RectTransform[] CinematicsBorders;
    public GameObject FullBlack;
    bool PlayerLook = true;
    Vector3 LookPos;
    void Update()
    {
        Cam = Camera.main.transform;
        Maxs = new Vector2(Borders.localPosition.x, Borders.localPosition.y);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (PlayerLook) LookPos = Player.position;
        Mays = new Vector2(Borders.localScale.x, Borders.localScale.y);
        Cam.position = Vector3.Lerp(Cam.position, new Vector3(Mathf.Clamp(Player.position.x + Distance.x, Maxs.x, Maxs.y), Mathf.Clamp(Player.position.y + Distance.y, Mays.x + (Camera.main.orthographicSize - 15) * 1.5f, Mays.y), Cam.position.z) + Cam.GetChild(0).localPosition, 0.1f); //CameraPositioner
    }
    public void CinematicCamera(bool Value)
    {
        StartCoroutine(CinematicCameraIE(Value));
    }
    public void DropCamera(float Value)
    {
        StartCoroutine(Drop(Value));
    }
    public void DropCamera(bool Value)
    {
        StartCoroutine(Drop(Value));
    }
    IEnumerator CinematicCameraIE(bool Value)
    {
        if (Value)
        {
            while (CinematicsBorders[0].sizeDelta.y < 100 || CinematicsBorders[1].sizeDelta.y < 100)
            {
                yield return new WaitForSeconds(0.01f);
                CinematicsBorders[0].sizeDelta += Vector2.up;
                CinematicsBorders[1].sizeDelta += Vector2.up;
            }
        }
        else
        {
            while (CinematicsBorders[0].sizeDelta.y > 0 || CinematicsBorders[1].sizeDelta.y > 0)
            {
                yield return new WaitForSeconds(0.005f);
                CinematicsBorders[0].sizeDelta -= Vector2.up;
                CinematicsBorders[1].sizeDelta -= Vector2.up;
            }
        }
    }
    IEnumerator Drop(float Time)
    {
        while (CinematicsBorders[0].sizeDelta.y < 3000 || CinematicsBorders[1].sizeDelta.y < 3000)
        {
            yield return new WaitForSeconds(0.01f);
            CinematicsBorders[0].sizeDelta += Vector2.up * 30;
            CinematicsBorders[1].sizeDelta += Vector2.up * 30;
        }
        yield return new WaitForSeconds(Time - 1.5f);
        while (CinematicsBorders[0].sizeDelta.y > 0 || CinematicsBorders[1].sizeDelta.y > 0)
        {
            yield return new WaitForSeconds(0.005f);
            CinematicsBorders[0].sizeDelta -= Vector2.up * 30;
            CinematicsBorders[1].sizeDelta -= Vector2.up * 30;
        }
    }
    IEnumerator Drop(bool Time)
    {
        if (Time)
        {
            while (CinematicsBorders[0].sizeDelta.y < 3000 || CinematicsBorders[1].sizeDelta.y < 3000)
            {
                yield return new WaitForSeconds(0.01f);
                CinematicsBorders[0].sizeDelta += Vector2.up * 30;
                CinematicsBorders[1].sizeDelta += Vector2.up * 30;
            }
            FullBlack.SetActive(Time);
        }
        else
        {
            FullBlack.SetActive(Time);
            while (CinematicsBorders[0].sizeDelta.y > 0 || CinematicsBorders[1].sizeDelta.y > 0)
            {
                yield return new WaitForSeconds(0.005f);
                CinematicsBorders[0].sizeDelta -= Vector2.up * 30;
                CinematicsBorders[1].sizeDelta -= Vector2.up * 30;
            }
        }
    }
    public void ZoomTheCamera(float TimeToZoom,float TimeToWait, float ZoomDistance)
    {
        StartCoroutine(Zoom(TimeToZoom, TimeToWait, ZoomDistance));
    }
    IEnumerator Zoom(float TimeToZoom, float TimeToWait, float ZoomDistance)
    {
        float FirstSize = Camera.main.orthographicSize;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(TimeToZoom/100);
            Camera.main.orthographicSize = 0.1f * (ZoomDistance - Camera.main.orthographicSize) + Camera.main.orthographicSize;
        }
        yield return new WaitForSeconds(TimeToWait);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(TimeToZoom / 200);
            Camera.main.orthographicSize = 0.1f * (FirstSize - Camera.main.orthographicSize) + Camera.main.orthographicSize;
        }
    }
    public void LookAt(Vector3 Position, float Time, float ZoomDistance)
    {
        StartCoroutine(Look(Position, Time, ZoomDistance));
    }
    IEnumerator Look(Vector3 Position, float Time, float ZoomDistance)
    {
        PlayerLook = false;
        LookPos = Position;
        ZoomTheCamera(0.5f, Time - 0.5f, ZoomDistance);
        yield return new WaitForSeconds(Time);
        PlayerLook = true;
    }
    public void CrackTheCamera(int Count, float Time, float Range)
    {
        StartCoroutine(Crack(Count, Time, Range));
    }
    public void CrackTheCamera(int Count, float Time, float Range, Vector2 Power)
    {
        StartCoroutine(Crack(Count, Time, Range, Power));
    }
    public void CrackTheCamera(int Count, float Time, float Range, Vector2 Power, int NoNegativeY)
    {
        StartCoroutine(Crack(Count, Time, Range, Power, NoNegativeY));
    }
    IEnumerator Crack(int Count, float Time, float Range)
    {
        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(Time);
            Distance += Vector3.right * Random.Range(-Range * 0.75f, Range * 0.75f) + Vector3.up * Random.Range(-Range * 0.75f, Range * 0.75f);
            Distance = new Vector3(Mathf.Clamp(Distance.x, -Range, Range), Mathf.Clamp(Distance.y, Range, 10 + Range));
        }
        Distance = new Vector3(0, 10);
    }
    IEnumerator Crack(int Count, float Time, float Range, Vector2 Power)
    {
        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(Time);
            Distance += Vector3.right * Random.Range(-Range * 0.75f, Range * 0.75f) * Power.x + Vector3.up * Random.Range(-Range * 0.75f, Range * 0.75f) * Power.y;
            Distance = new Vector3(Mathf.Clamp(Distance.x, -Range, Range), Mathf.Clamp(Distance.y, Range, 10 + Range));
        }
        Distance = new Vector3(0, 10);
    }
    IEnumerator Crack(int Count, float Time, float Range, Vector2 Power, int NoNegativeY)
    {
        for (int i = 0; i < Count; i++)
        {
            yield return new WaitForSeconds(Time);
            Distance += Vector3.right * Random.Range(-Range * 0.75f, Range * 0.75f) * Power.x + Vector3.up * Random.Range(-Range * 0.75f * NoNegativeY, Range * 0.75f) * Power.y;
            Distance = new Vector3(Mathf.Clamp(Distance.x, -Range, Range), Mathf.Clamp(Distance.y, Range, 10 + Range));
        }
        Distance = new Vector3(0, 10);
    }
}