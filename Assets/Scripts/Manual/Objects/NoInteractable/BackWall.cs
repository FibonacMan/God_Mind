using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWall : MonoBehaviour
{
    int Size;
    Transform[] Walls;
    bool Loadable;
    public GameObject Activity;
    void Start()
    {
        Size = transform.childCount;
        Walls = new Transform[Size];
        for (int i = 0; i < Size; i++)
        {
            Walls[i] = transform.GetChild(i);
            Loadable = true;
        }
    }
    private void Update()
    {
        if (Activity.activeSelf)
        {
            if (Loadable) StartCoroutine(Load());
        }
        else
        {
            Loadable = true;
            UnLoad();
        }
    }
    IEnumerator Load()
    {
        Loadable = false;
        for (int i = 0; i < Size; i++)
        {
            if (Vector3.Distance(Walls[i].position, Camera.main.transform.position) < Camera.main.orthographicSize * 5 * Mathf.Clamp(Size / 500, 1, Mathf.Infinity)) Walls[i].gameObject.SetActive(true);
            else Walls[i].gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(Size / 225);
        Loadable = true;
    }
    void UnLoad()
    {
        for (int i = 0; i < Size; i++)
        {
            Walls[i].gameObject.SetActive(false);
        }
    }
}