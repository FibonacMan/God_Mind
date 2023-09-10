using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public int SpeakType;
    public bool Speech;
    public bool SectionUp;
    public bool ControlEnd = true;
    public int MaxRepeat = 0;
    public float ExtraWaitTime = 0;
    public bool Cinematic;
    public Vector2[] Characters;
    public Vector3[] Positions;
    public bool[] Craziness;
    bool InteractAgain = true;
    public bool Finished;
    public int SpeakIndex;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && InteractAgain)
        {
            transform.position -= Vector3.up * 100;
            StartCoroutine(Speak());
        }
    }
    IEnumerator Speak()
    {
        InteractAgain = false;
        float TimeStart = Time.time;
        yield return new WaitUntil(()=>GameObject.FindGameObjectWithTag("Clef").transform.localPosition.x==1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (SpeakType == 1)
        {
            FindObjectOfType<InteractManager>().NoEscape();
        }
        else if (SpeakType == 2)
        {
            FindObjectOfType<InteractManager>().NoInteract();
        }
        else if (SpeakType == 3)
        {
            FindObjectOfType<InteractManager>().NoMove();
        }
        if (Characters.Length > 1 && Speech)
        {
            if (FindObjectOfType<OptionsManager>().Language == "English") FindObjectOfType<GratiasInteract>().CouldBe("Press E to Continue", true);
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") FindObjectOfType<GratiasInteract>().CouldBe("Devam etmek için e'ye basýn", true);
        }
        if (Cinematic && !FindObjectOfType<GratiasMovement>().GodMode) FindObjectOfType<CameraManager>().CinematicCamera(true);
        float Posy = GameObject.FindGameObjectWithTag("Msiz").transform.localPosition.x;
        if (Characters.Length > 1)
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = 0.025f * (Vector3.right * 0.1f - GameObject.FindGameObjectWithTag("Msiz").transform.localPosition) + GameObject.FindGameObjectWithTag("Msiz").transform.localPosition;
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            if (FindObjectOfType<GratiasMovement>().GodMode) { break; }
            SpeakIndex = i;
            TimeStart = Time.time;
            if (Positions[i].z == 1) Positions[i] = new Vector3(transform.position.x, Positions[i].y, 0);
            else if (Positions[i].z == 2) Positions[i] = new Vector3(transform.position.x, transform.position.y, 0) + Vector3.up * 100;
            else if (Positions[i].z == 3) Positions[i] = new Vector3(Positions[i].x, Positions[i].y, 0) + GameObject.FindGameObjectWithTag("Player").transform.position;
            else if (Positions[i].z == 4) Positions[i] = new Vector3(Positions[i].x, Positions[i].y, 0) + GameObject.FindGameObjectWithTag("Girl").transform.position;
            else if (Positions[i].z == 5) Positions[i] = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(Positions[i].x, Positions[i].y, 0) + Vector3.up * 100;
            if (Characters.Length > 1 && Speech)
            {
                if(FindObjectOfType<OptionsManager>().Language == "English") FindObjectOfType<GratiasInteract>().CouldBe("Press E to Continue", true);
                if (FindObjectOfType<OptionsManager>().Language == "Turkish") FindObjectOfType<GratiasInteract>().CouldBe("Devam etmek için e'ye basýn", true);
            }
            if (i > 0 && Speech) yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
            GetComponent<AudioSource>().clip = FindObjectOfType<GratiasInteract>().NoneSpeech[(int)Characters[i].x];
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1.25f);
            if (i < Characters.Length - 1 || Characters.Length == 1)
            {
                if (Speech)
                {
                    if (Characters.Length > 1)
                    {
                        if (FindObjectOfType<OptionsManager>().Language == "English") FindObjectOfType<GratiasInteract>().CouldBe("Press E to Continue", true);
                        if (FindObjectOfType<OptionsManager>().Language == "Turkish") FindObjectOfType<GratiasInteract>().CouldBe("Devam etmek için e'ye basýn", true);
                    }
                    if (Craziness.Length > i) FindObjectOfType<GratiasInteract>().GiveASpeak(Craziness[i], (int)Characters[i].x, (int)Characters[i].y, Positions[i], Speech);
                    else FindObjectOfType<GratiasInteract>().GiveASpeak((int)Characters[i].x, (int)Characters[i].y, Positions[i], false);
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
                }
                else
                {
                    if (Craziness.Length > i) FindObjectOfType<GratiasInteract>().GiveASpeak(Craziness[i], (int)Characters[i].x, (int)Characters[i].y, Positions[i], false);
                    else FindObjectOfType<GratiasInteract>().GiveASpeak((int)Characters[i].x, (int)Characters[i].y, Positions[i], false);
                    yield return new WaitUntil(() => !GetComponent<AudioSource>().isPlaying || Time.time - TimeStart > 15);
                }
            }
            else
            {
                if (Craziness.Length > i) FindObjectOfType<GratiasInteract>().GiveASpeak(Craziness[i], (int)Characters[i].x, (int)Characters[i].y, Positions[i], false);
                else FindObjectOfType<GratiasInteract>().GiveASpeak((int)Characters[i].x, (int)Characters[i].y, Positions[i], false);
                yield return new WaitUntil(() => !GetComponent<AudioSource>().isPlaying || Time.time - TimeStart > 15);
            }
        }
        Finished = true;
        if (Characters.Length > 1)
        {
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = 0.035f * (Vector3.right * Posy - GameObject.FindGameObjectWithTag("Msiz").transform.localPosition) + GameObject.FindGameObjectWithTag("Msiz").transform.localPosition;
            }
            yield return new WaitForSeconds(ExtraWaitTime);
        }
        else yield return new WaitForSeconds(0.1f + ExtraWaitTime);
        if (Characters.Length > 1 && Speech)
        {
            if (FindObjectOfType<OptionsManager>().Language == "English") FindObjectOfType<GratiasInteract>().CouldBe("Press E to Continue", false);
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") FindObjectOfType<GratiasInteract>().CouldBe("Devam etmek için e'ye basýn", false);
        }
        if (SectionUp) FindObjectOfType<MissionManager>().UpdateTheSection();
        if (ControlEnd) FindObjectOfType<InteractManager>().AllOpened();
        if (Cinematic && !FindObjectOfType<GratiasMovement>().GodMode) FindObjectOfType<CameraManager>().CinematicCamera(false);
        if (MaxRepeat <= 0) gameObject.SetActive(false);
        else
        {
            MaxRepeat -= 1;
            transform.position += Vector3.up * 100;
            InteractAgain = true;
        }
    }
}