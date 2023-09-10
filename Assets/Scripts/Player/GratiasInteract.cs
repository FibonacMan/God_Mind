using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class GratiasInteract : MonoBehaviour
{
    public bool DoorOpenable = true;
    public TextMeshProUGUI InteractTextSpeak;
    public Text InteractText;
    bool Looked;
    bool Look;
    bool Use;
    Transform Usage;
    Transform LookID;
    string FirstLayer;
    int[] LookedIDs = new int[0];
    bool Room;
    int NewRoomID;
    public Transform RoomGlobal;
    public TextAsset[] AllSpeaksEnglish;
    public TextAsset[] AllSpeaksTurkish;
    public TextAsset[] AllSpeaks;
    public Color[] OwnerColor;
    public AudioClip[] NoneSpeech;
    bool Can;
    void Update()
    {
        if (FindObjectOfType<OptionsManager>().Language == "English") AllSpeaks = AllSpeaksEnglish;
        if (FindObjectOfType<OptionsManager>().Language == "Turkish") AllSpeaks = AllSpeaksTurkish;
        if (Look && !Looked)
        {
            if (LookID != null)
            {
                if (LookID.parent.GetComponent<SpriteRenderer>().sortingOrder < GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder) LookID.parent.GetComponent<SpriteRenderer>().sortingLayerName = "Interact";
                else LookID.parent.GetComponent<SpriteRenderer>().sortingLayerName = "InteractUp";
            }
            InteractText.color += Color.white / 45f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Looked = true;
                Array.Resize(ref LookedIDs, LookedIDs.Length + 1);
                LookedIDs[LookedIDs.Length - 1] = (int)LookID.transform.localScale.z;
            }
        }
        else if (!Room && !Use && !Can)
        {
            if (LookID != null) LookID.parent.GetComponent<SpriteRenderer>().sortingLayerName = FirstLayer;
            InteractText.color -= Color.white / 200f;
        }
        if (Look && Looked)
        {
            Look = false;
            if (LookID != null) GiveASpeak(1, (int)LookID.transform.localScale.z, LookID.transform.position);
        }
        InteractText.color = new Color(1, 1, 1, Mathf.Clamp(InteractText.color.a, 0, 1));
        if (Room)
        {
            InteractText.color += Color.white / 45f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(EnterTheDoor());
            }
        }
        if (Use)
        {
            InteractText.color += Color.white / 45f;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (InteractText.text.Split(' ')[InteractText.text.Split(' ').Length - 1] == "Sit")
                {
                    Usage.gameObject.layer = 6;
                    FindObjectOfType<GratiasMovement>().enabled = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    GameObject.FindGameObjectWithTag("Player").transform.position = Usage.GetChild(1).position;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 6);
                    if (FindObjectOfType<Mission0>() != null)
                    {
                        FindObjectOfType<Mission0>().Sit();
                    }
                }
                else if (InteractText.text.Split(' ')[InteractText.text.Split(' ').Length - 1] == "Up")
                {
                    Usage.gameObject.layer = 3;
                    FindObjectOfType<GratiasMovement>().enabled = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                Usage.GetChild(0).localPosition *= -1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Look" && !Transformation.IsIntInArray(LookedIDs, (int)collision.transform.localScale.z))
        {
            FirstLayer = collision.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
        }
        if (collision.tag == "Gate")
        {
            StartCoroutine(GateOpener(collision.transform));
        }
        if (collision.tag == "Open")
        {
            if (collision.transform.TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                audioSource.Play();
            }
        }
        if (collision.tag == "Port")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = collision.transform.GetChild(0).localPosition;
        }
        if (collision.tag == "Tell")
        {
            GiveASpeak(collision.name, (int)collision.transform.localScale.z, collision.transform.position);
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "Note")
        {
            collision.gameObject.SetActive(false);
            TakeTheNote((int)collision.transform.localScale.z);
        }
        if (collision.tag == "Harm" && !FindObjectOfType<GratiasMovement>().NoMove)
        {
            FindObjectOfType<HealthManagment>().DecreaseHealth(collision.GetComponent<Harmer>().Power);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Look" && !Transformation.IsIntInArray(LookedIDs, (int)collision.transform.localScale.z))
        {
            Look = true;
            LookID = collision.transform;
            if(FindObjectOfType<OptionsManager>().Language=="English")InteractText.text = "Press E to Look";
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Incelemek icin e'ye basin";
        }
        if (collision.tag == "Door" && !Look && DoorOpenable)
        {
            Room = true;
            NewRoomID = (int)collision.transform.localScale.z;
            if (collision.gameObject.layer != 3)
            {
                if (FindObjectOfType<OptionsManager>().Language == "English") InteractText.text = "Press E to Left The Room";
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Cikmak icin e'ye basin";
            }
            else
            {
                if (FindObjectOfType<OptionsManager>().Language == "English") InteractText.text = "Press E to Enter The Room";
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Girmek icin e'ye basin";
            }
        }
        if (collision.tag == "Goal")
        {
            Use = true;
            if (collision.gameObject.layer != 3 && collision.gameObject.layer != 6)
            {
                if (FindObjectOfType<OptionsManager>().Language == "English") InteractText.text = "Press E to Use";
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Kullanmak icin e'ye basin";
            }
            else if (collision.gameObject.layer == 6)
            {
                if (FindObjectOfType<OptionsManager>().Language == "English") InteractText.text = "Press E to Stand Up";
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Kalkmak icin e'ye basin";
            }
            else
            {
                if (FindObjectOfType<OptionsManager>().Language == "English") InteractText.text = "Press E to Sit";
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") InteractText.text = "Oturmak icin e'ye basin";
            }
            Usage = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Look")
        {
            Look = false;
            Looked = false;
            StartCoroutine(WaitAndDis(0));
        }
        if (collision.tag == "Door" && DoorOpenable)
        {
            Room = false;
        }
        if (collision.tag == "Goal")
        {
            Use = false;
        }
    }
    IEnumerator WaitAndDis(int Type)
    {
        yield return new WaitForSeconds(0.01f);
        if (Type == 0) LookID = null;
    }
    public void GiveASpeak(string Teller, int Line, Vector3 Position)
    {
        InteractTextSpeak.text = Teller + ":" + Transformation.FindByName(AllSpeaks, Teller).text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Transformation.FindIndexByName(AllSpeaks, Teller)];
        Spawned.GetComponent<OnceToldText>().Position = Position;
    }
    public void GiveASpeak(int Teller, int Line, Vector3 Position)
    {
        InteractTextSpeak.text = AllSpeaks[Teller].name + ":" + AllSpeaks[Teller].text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Teller];
        Spawned.GetComponent<OnceToldText>().Position = Position;
    }
    public void GiveASpeak(string Teller, int Line, Vector3 Position, bool Speech)
    {
        InteractTextSpeak.text = Teller + ":" + Transformation.FindByName(AllSpeaks, Teller).text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Transformation.FindIndexByName(AllSpeaks, Teller)];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        Spawned.GetComponent<OnceToldText>().Speech = Speech;
    }
    public void GiveASpeak(int Teller, int Line, Vector3 Position, bool Speech)
    {
        InteractTextSpeak.text = AllSpeaks[Teller].name + ":" + AllSpeaks[Teller].text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Teller];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        Spawned.GetComponent<OnceToldText>().Speech = Speech;
    }
    public void GiveASpeak(bool Crazy,string Teller, int Line, Vector3 Position)
    {
        InteractTextSpeak.text = Teller + ":" + Transformation.FindByName(AllSpeaks, Teller).text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Transformation.FindIndexByName(AllSpeaks, Teller)];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        if (Crazy) Spawned.AddComponent<CrazyText>();
    }
    public void GiveASpeak(bool Crazy, int Teller, int Line, Vector3 Position)
    {
        InteractTextSpeak.text = AllSpeaks[Teller].name + ":" + AllSpeaks[Teller].text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Teller];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        if (Crazy) Spawned.AddComponent<CrazyText>();
    }
    public void GiveASpeak(bool Crazy, string Teller, int Line, Vector3 Position, bool Speech)
    {
        InteractTextSpeak.text = Teller + ":" + Transformation.FindByName(AllSpeaks, Teller).text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Transformation.FindIndexByName(AllSpeaks, Teller)];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        Spawned.GetComponent<OnceToldText>().Speech = Speech;
        if (Crazy) Spawned.AddComponent<CrazyText>();
    }
    public void GiveASpeak(bool Crazy, int Teller, int Line, Vector3 Position, bool Speech)
    {
        InteractTextSpeak.text = AllSpeaks[Teller].name + ":" + AllSpeaks[Teller].text.Split('\n')[Line];
        GameObject Spawned = Instantiate(InteractTextSpeak.gameObject, InteractTextSpeak.transform.parent);
        Spawned.GetComponent<TextMeshProUGUI>().color = OwnerColor[Teller];
        Spawned.GetComponent<OnceToldText>().Position = Position;
        Spawned.GetComponent<OnceToldText>().Speech = Speech;
        if (Crazy) Spawned.AddComponent<CrazyText>();
    }
    public void TakeTheNote(int NoteID)
    {
        GameObject.FindObjectOfType<NoteManager>().GetNote(NoteID);
    }
    IEnumerator GateOpener(Transform collision)
    {
        collision.tag = "Untagged";
        if (collision.localPosition.z >= 0)
        {
            if(collision.gameObject.layer!=3)FindObjectOfType<PostProcessingEffects>().Madness((int)collision.localPosition.z,0,true);
            else FindObjectOfType<PostProcessingEffects>().SecMadness((int)collision.localPosition.z, 0, true);
            yield return new WaitForSeconds(0.1f);
            Debug.Log(Transformation.RoomActivity());
            yield return new WaitUntil(() => Transformation.RoomActivity());
        }
        yield return new WaitForSeconds(0.75f);
        RoomGlobal.localPosition = Vector3.right * collision.localScale.z;
        FindObjectOfType<SkyManager>().Skyler();
        collision.gameObject.SetActive(false);
    }
    IEnumerator EnterTheDoor()
    {
        FindObjectOfType<OptionsManager>().UpdateMusics();
        FindObjectOfType<EffectManager>().GratiasEffects[3].Play();
        FindObjectOfType<InteractManager>().NoMoveWithInteract();
        FindObjectOfType<PostProcessingEffects>().Close(0.2f);
        yield return new WaitForSeconds(0.1f);
        RoomGlobal.localPosition = Vector3.right * NewRoomID;
        FindObjectOfType<SkyManager>().Skyler();
        yield return new WaitUntil(() => Transformation.RoomActivity());
        FindObjectOfType<InteractManager>().AllOpened();
    }
    public void CanBe(string texty, float Timey)
    {
        StartCoroutine(CanNotBe(texty, Timey));
    }
    IEnumerator CanNotBe(string texty, float Timey)
    {
        Can = true;
        InteractText.text = texty;
        float Passed = 0;
        while(InteractText.color.a<1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Passed += Time.deltaTime;
            InteractText.color += Color.white / 45f;
        }
        yield return new WaitForSeconds(Timey - Passed);
        Can = false;
    }
    public void CouldBe(string texty, bool Switch)
    {
        StartCoroutine(CanNotBe(texty, Switch));
    }
    IEnumerator CanNotBe(string texty, bool Switch)
    {
        Can = Switch;
        float Passed = 0;
        if (Switch)
        {
            InteractText.text = texty;
            while (InteractText.color.a < 1)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                Passed += Time.deltaTime;
                InteractText.color += Color.white / 45f;
            }
        }
        else
        {
            while (InteractText.color.a >0)
            {
                Debug.Log(Can);
                yield return new WaitForSeconds(Time.deltaTime);
                Passed += Time.deltaTime;
                InteractText.color -= Color.white / 200f;
            }
        }
    }
    public static Vector3 Home = new Vector3(0, -14, 0);
}