using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractManager : MonoBehaviour
{
    public bool Interactable = true;
    public GameObject[] Panels;
    public string[] ContentsEnglish;
    public string[] ContentsTurkish;
    public string[] Contents;
    public Scrollbar Bossbar;
    public Color[] BossColors;
    public Vector3[] Positions;



    void Update()
    {
        if (FindObjectOfType<OptionsManager>().Language == "English") Contents = ContentsEnglish;
        if (FindObjectOfType<OptionsManager>().Language == "Turkish") Contents = ContentsTurkish;
    }



    public void AllOpened()
    {
        Interactable = true;
        FindObjectOfType<GratiasInteract>().enabled = Interactable;
        FindObjectOfType<GratiasInteract>().DoorOpenable = true;
        FindObjectOfType<GratiasMovement>().enabled = true;
    }



    public void NoEscape()
    {
        Interactable = true;
        FindObjectOfType<GratiasInteract>().enabled = Interactable;
        FindObjectOfType<GratiasInteract>().DoorOpenable = false;
        FindObjectOfType<GratiasMovement>().enabled = true;
    }



    public void NoInteract()
    {
        Interactable = false;
        FindObjectOfType<GratiasInteract>().enabled = Interactable;
        FindObjectOfType<GratiasInteract>().DoorOpenable = false;
        FindObjectOfType<GratiasMovement>().enabled = true;
    }



    public void NoMove()
    {
        Interactable = false;
        FindObjectOfType<GratiasInteract>().enabled = Interactable;
        FindObjectOfType<GratiasInteract>().DoorOpenable = false;
        FindObjectOfType<GratiasMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 6);
    }



    public void NoMoveWithInteract()
    {
        Interactable = false;
        FindObjectOfType<GratiasInteract>().DoorOpenable = false;
        FindObjectOfType<GratiasMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("Act", 6);
    }



    public void OpenBoss(int ID)
    {
        GameObject.FindGameObjectWithTag("Left").transform.localScale = Vector3.one * 0.75f;
        Bossbar.gameObject.SetActive(true);
        Color BossColor = Bossbar.image.color;
        Color HandleColor = Bossbar.handleRect.transform.GetComponent<Image>().color;
        Vector2 BossLevels = new Vector2(Transformation.MinColor(BossColor), Transformation.MaxColor(BossColor));
        Vector2 HandleLevels = new Vector2(Transformation.MinColor(HandleColor), Transformation.MaxColor(HandleColor));
        Bossbar.image.color = BossLevels.x * (Color.white - Color.black) + (BossLevels.y - BossLevels.x) * (BossColors[ID] - Color.black) + Color.black;
        Bossbar.handleRect.transform.GetComponent<Image>().color = HandleLevels.x * (Color.white - Color.black) + (HandleLevels.y - HandleLevels.x) * (BossColors[ID] - Color.black) + Color.black;
    }



    public void CloseBoss()
    {
        GameObject.FindGameObjectWithTag("Left").transform.localScale = Vector3.zero;
        Bossbar.gameObject.SetActive(false);
    }



    public  void StartAgain()
    {
        FindObjectOfType<OldMan>().Health = FindObjectOfType<OldMan>().MaxHealth;
        FindObjectOfType<HealthManagment>().FullHealth();
        GameObject.FindGameObjectWithTag("Player").transform.position = Positions[0];
        FindObjectOfType<OldMan>().transform.position = Positions[1];
        GameObject.FindGameObjectWithTag("Dead").transform.GetChild(0).gameObject.SetActive(false);
        FindObjectOfType<InteractManager>().AllOpened();
    }
}
