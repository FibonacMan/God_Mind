using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnceToldText : MonoBehaviour
{
    int Listy = 1;
    float Constant;
    bool OneTime = true;
    public Transform Chars;
    public Vector3 Position;
    public bool Speech;
    private void Start()
    {
        //GeneralSpawn

        if (GameObject.FindGameObjectsWithTag("Told").Length > 1) //TextSpawned
        {
            GetComponent<TextMeshProUGUI>().text = " " + GetComponent<TextMeshProUGUI>().text.Split(':')[1];  //SetTextMain


            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<TextMeshProUGUI>().text.Length * 15, GetComponent<RectTransform>().sizeDelta.y);  //SetTextMainSize


            int Plus = 0;
            while (GameObject.Find("Told" + (GameObject.FindGameObjectsWithTag("Told").Length + Plus).ToString()) != null)  //TextCounter
            {
                Plus += 1;
            }


            GetComponent<RectTransform>().position=Camera.main.WorldToScreenPoint(Position);


            GetComponent<RectTransform>().localScale = Vector3.one; //ActivateMe


            gameObject.name = "Told" + (GameObject.FindGameObjectsWithTag("Told").Length + Plus).ToString(); //SetMyNameForCounting
            //GeneralSpawn
        }
    }
    private void Update()
    {
        if (GetComponent<RectTransform>().localScale.z == 1)
        {
            GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(Position);
            if (Listy == 1) Constant = 5f;
            else if (Listy == -1) Constant = 3f;
            GetComponent<TextMeshProUGUI>().color = GetComponent<TextMeshProUGUI>().color + Color.black / (GetComponent<TextMeshProUGUI>().text.Length * 15f) * Constant * Listy;
            if (GetComponent<TextMeshProUGUI>().color.a >= 1 && OneTime && !Speech)
            {
                StartCoroutine(Wait());
            }
            if (Speech && Input.GetKeyDown(KeyCode.E))
            {
                gameObject.tag = "Untagged";
                Destroy(gameObject);
            }
        }
    }
    IEnumerator Wait()
    {
        OneTime = false;
        yield return new WaitForSeconds(GetComponent<TextMeshProUGUI>().text.Split(' ').Length / 1.5f);
        Listy = -1;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            GetComponent<TextMeshProUGUI>().color = GetComponent<TextMeshProUGUI>().color + Color.black / (GetComponent<TextMeshProUGUI>().text.Length * 15f) * Constant * Listy;
            GetComponent<TextMeshProUGUI>().color = new Color(GetComponent<TextMeshProUGUI>().color.r, GetComponent<TextMeshProUGUI>().color.g, GetComponent<TextMeshProUGUI>().color.b, Mathf.Clamp(GetComponent<TextMeshProUGUI>().color.a, 0, 1));
            if (GetComponent<TextMeshProUGUI>().color.a <= 0) break;
        }
        Destroy(gameObject);
    }
}