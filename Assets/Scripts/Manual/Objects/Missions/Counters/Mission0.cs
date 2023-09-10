using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class Mission0 : MonoBehaviour
{
    bool Sitted;
    public Text EText;
    public GameObject[] Doors;
    public GameObject[] LastDoors;
    public Vector3 EnymaDest;
    public GameObject Border;
    int MadCount;
    bool GetFucked;
    public AudioSource CrazySound;
    public Animator OldMan;
    public AudioSource Lalu;
    public AudioSource CloseMusic;
    bool OneTransform;
    public Sprite[] OldManSpritesForScale;
    public float[] OldManScaleUp;
    bool OneTimedOldMan;
    public AudioSource SmashSound;
    public AudioSource ShovelSound;
    bool LightMaker = true;
    float DigTime;
    public GameObject[] Siblings;
    public GameObject[] Chairs;
    bool SectionSettedTo1;
    public Vector3 PosFor14thRoom;
    bool OneTimeForSection1 = true;
    bool MakeItReal;
    public GameObject Nutrix;
    public GameObject Dad;
    public GameObject BirdSpeech;
    private void Update()
    {
        if (GetFucked && Vector3.Distance(FindObjectOfType<Enyma>().transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 7.5f)
        {
            CrazySound.volume -= 0.01f;
            FindObjectOfType<Enyma>().Speed = 0.05f;
            FindObjectOfType<Enyma>().Destination = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.right * 20;
        }
        if (OldMan.GetInteger("Act") != 0 && Vector3.Distance(OldMan.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 20 && !OneTransform)
        {
            OneTransform = true;
            TransformOldMan();
        }
        if (!OneTimedOldMan)
        {
            for (int i = 0; i < OldManSpritesForScale.Length; i++)
            {
                if (OldMan.GetComponent<SpriteRenderer>().sprite == OldManSpritesForScale[i])
                {
                    OldMan.transform.localScale = OldManScaleUp[i] * new Vector3(Mathf.Sign(OldMan.transform.localScale.x), Mathf.Sign(OldMan.transform.localScale.y));
                    Debug.LogWarning("Hey");
                }
            }
        }
        if (OldMan.GetInteger("Act") == 5 && OldMan.GetComponent<SpriteRenderer>().sprite == OldManSpritesForScale[OldManSpritesForScale.Length - 2])
        {
            OneTimedOldMan = true;
            OldMan.SetInteger("Act", 0);
        }
        if (LightMaker && SectionSettedTo1)
        {
            GameObject.FindGameObjectWithTag("Star").GetComponent<Light2D>().intensity = DigTime * 0.02f;
        }
        if(FindObjectOfType<MissionManager>().SectionGhostier().name == "Section*3")
        {
            if (OneTimeForSection1)
            {
                OneTimeForSection1 = false;
                GameObject.FindGameObjectWithTag("Player").transform.position = GratiasInteract.Home;
            }
        }
        if (FindObjectOfType<MissionManager>().SectionGhostier().name == "Section1")
        {
            SectionSettedTo1 = true;
        }
        if (MakeItReal)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = PosFor14thRoom;
            Camera.main.transform.position = new Vector3(275, -2.5f, -10);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 6);
        }
    }
    public void NoReal()
    {
        MakeItReal = false;
        foreach (GameObject God in Siblings)
        {
            God.SetActive(true);
        }
        Nutrix.SetActive(true);
        Dad.SetActive(false);
    }
    public void StartBitch()
    {
        MakeItReal = true;
        BirdSpeech.SetActive(true);
    }
    public void DigWithShovel()
    {
        StartCoroutine(Dig());
    }
    public void UnDigWithShovel()
    {
        LightMaker = false;
        Doors[0].tag = "Door";
        Doors[1].tag = "Door";
    }
    IEnumerator Dig()
    {
        LightMaker = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 6);
        float FullTime = 10f;
        float[] WaitTimes = new float[10];
        for (int i = 0; i < 9; i++)
        {
            WaitTimes[i] = Random.Range(FullTime / 15f, FullTime / 7.5f);
            FullTime -= WaitTimes[i];
        }
        WaitTimes[9] = FullTime;
        for (int i = 0; i < 10; i++)
        {
            ShovelSound.pitch = Random.Range(0.75f, 1.25f);
            ShovelSound.Play();
            DigTime = i + 1;
            yield return new WaitForSeconds(5 * WaitTimes[i]);
        }
    }
    public void KilledOldMan()
    {
        StartCoroutine(KillHim());
    }
    IEnumerator KillHim()
    {
        FindObjectOfType<InteractManager>().CloseBoss();
        FindObjectOfType<PostProcessingEffects>().SecMadness(1,16.5f,false);
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = Vector3.zero;
        CrazySound.gameObject.SetActive(false);
        Lalu.gameObject.SetActive(false);
        OldMan.transform.GetChild(4).gameObject.SetActive(true);
        OldMan.transform.GetChild(4).SetParent(null);
        OldMan.gameObject.SetActive(false);
        for (int i = 0; i < 15 / SmashSound.clip.length; i++)
        {
            SmashSound.pitch = Random.Range(0.75f, 1.25f);
            SmashSound.Play();
            yield return new WaitWhile(() => SmashSound.isPlaying);
        }
        GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = Vector3.one;
    }
    public void Sit()
    {
        if (!Sitted && FindObjectOfType<MissionManager>().SectionGhostier().name=="Section2")
        {
            Sitted = true;
            FindObjectOfType<MissionManager>().UpdateTheSection();
        }
    }
    public void Kill()
    {
        StartCoroutine(Killed());
    }
    public void Fuck()
    {
        GetFucked = true;
        GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.localScale.x), GameObject.FindGameObjectWithTag("Player").transform.localScale.y);
    }
    public void GateOpen()
    {
        StartCoroutine(Open());
    }
    public void AwakeOldMan()
    {
        OldMan.SetInteger("Act", 4);
    }
    public void TransformOldMan()
    {
        GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = Vector3.one;
        Lalu.Play();
        Lalu.playOnAwake = true;
        CloseMusic.mute = true;
        //FindObjectOfType<CameraManager>().LookAt(OldMan.transform.position, 2, 10);
        //StartCoroutine(TransformOldManIE());
        OldMan.SetInteger("Act", 5);
        OldMan.GetComponent<OldMan>().Work = true;
        FindObjectOfType<InteractManager>().OpenBoss(0);
    }
    IEnumerator TransformOldManIE()
    {
        FindObjectOfType<InteractManager>().NoMoveWithInteract();
        yield return new WaitForSeconds(3);
        FindObjectOfType<InteractManager>().AllOpened();
    }
    IEnumerator Open()
    {
        GetFucked = false;
        yield return new WaitForSeconds(Time.deltaTime * 10);
        FindObjectOfType<Enyma>().Destination = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.right * 500;
        FindObjectOfType<Enyma>().Speed = 1f;
    }
    IEnumerator Killed()
    {
        foreach(GameObject God in Siblings)
        {
            God.SetActive(false);
        }
        foreach (GameObject God in Chairs)
        {
            God.tag="Goal";
        }
        yield return new WaitUntil(() => FindObjectOfType<GratiasMovement>().Layer() == 0);
        FindObjectOfType<InteractManager>().NoMoveWithInteract();
        Border.SetActive(false);
        FindObjectOfType<Enyma>().Destination = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.right * 15;
        FindObjectOfType<MissionManager>().UpdateTheSection();
        GameObject.FindGameObjectWithTag("Player").transform.localScale = new Vector3(-Mathf.Abs(GameObject.FindGameObjectWithTag("Player").transform.localScale.x), GameObject.FindGameObjectWithTag("Player").transform.localScale.y);
    }
    public void Finish()
    {
        FindObjectOfType<PostProcessingEffects>().SecMadness(2, 0, true);
        StartCoroutine(Fin());
    }
    IEnumerator Fin()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.FindGameObjectWithTag("Player").transform.position = GratiasInteract.Home;
        FindObjectOfType<MissionManager>().UpdateTheMission();
        EText.color = Color.clear;
    }
    public void StartTheGame()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(275, -14.8850002f, 0);
    }
}