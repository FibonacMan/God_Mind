using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aphrodite : MonoBehaviour
{
    public float Size;
    public SpriteRenderer Back;
    bool Changable = true;
    bool DreamsComeTrue = true;
    public Color Mood;
    public Color[] MaxAndMinColor;
    public float MaxX;
    public AudioSource[] SoundEffects;
    public AudioSource LastSound;
    bool OneTime;
    public GameObject[] Saxophone;
    void Update()
    {
        transform.localPosition = Mathf.Floor((GameObject.FindGameObjectWithTag("Player").transform.position.x) / MaxX * 5) * Vector3.right;
        Mood = (MaxAndMinColor[0] - MaxAndMinColor[1]) * (MaxX - GameObject.FindGameObjectWithTag("Player").transform.position.x) / MaxX + MaxAndMinColor[1] + Color.black;
        if (Changable) StartCoroutine(Change());
        if (DreamsComeTrue)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = Mood;
            Back.color = Mood;
            GameObject.FindGameObjectWithTag("Char").transform.GetChild(0).localPosition = Vector3.right * (0.1f * (MaxX - GameObject.FindGameObjectWithTag("Player").transform.position.x) / MaxX + 0.025f);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = Color.red;
            GameObject.FindGameObjectWithTag("Char").transform.GetChild(0).localPosition = Vector3.right * 0.065f;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > 750)
        {
            foreach (AudioSource audio in SoundEffects)
            {
                if (audio.volume > 0) audio.volume -= 0.001f;
            }
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > 145f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(152.5f, -2.25f) + Vector3.forward * -10,0.025f);
            GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.Lerp(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(145f, -0.75f), 0.025f);
            if (!OneTime)
            {
                GameObject.FindGameObjectWithTag("Play").transform.localPosition = Vector3.zero;
                StartCoroutine(Finish());
            }
            else
            {
                if(LastSound!=null) if (LastSound.volume > 0) LastSound.volume -= 0.01f;
            }
        }
    }
    IEnumerator Change()
    {
        Changable = false;
        Back.sprite = Transformation.TextureToSprite(Transformation.RandomizedTexture(Color.gray, new Vector2(180, 120), Random.Range(0, 100), Size));
        yield return new WaitForSeconds(0.1f);
        Changable = true;
    }
    IEnumerator Finish()
    {
        OneTime = true;
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(147.5f, -0.75f);
        yield return new WaitForSeconds(5);
        if (LastSound != null) LastSound.transform.SetParent(null);
        DreamsComeTrue = false;
        yield return new WaitForSeconds(0.35f);
        GameObject.FindGameObjectWithTag("Play").transform.localPosition = Vector3.right;
        GameObject.FindGameObjectWithTag("Player").transform.position = GratiasInteract.Home;
        Camera.main.transform.position = Vector3.zero + Vector3.forward * -10;
        FindObjectOfType<MissionManager>().UpdateTheSection();
        foreach (GameObject Saxo in Saxophone) Saxo.SetActive(true);
        GameObject.FindGameObjectWithTag("Norm").transform.localPosition = Vector3.zero;
    }
}
