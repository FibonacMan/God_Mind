using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Intro : MonoBehaviour
{
    public VideoPlayer Vieo;
    public GameObject MyCanvas;
    public GameObject SpiderMan;
    public GameObject VideoBack;
    public GameObject RoomBacks;
    public GameObject LefTTop;
    private void Start()
    {
        StartCoroutine(GameOpened());
    }
    IEnumerator GameOpened()
    {
        yield return new WaitForSeconds(5);
        Vieo.Play();
        yield return new WaitUntil(() => !Vieo.isPlaying);
        Vieo.gameObject.SetActive(false);
        MyCanvas.SetActive(true);
        SpiderMan.SetActive(true);
        VideoBack.SetActive(false);
        RoomBacks.SetActive(true);
        LefTTop.transform.localScale = Vector3.zero;
    }
}
