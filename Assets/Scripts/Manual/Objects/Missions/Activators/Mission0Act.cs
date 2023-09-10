using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission0Act : MonoBehaviour
{
    public int Type;
    bool NoRepeat;
    private void Update()
    {
        if (Type == -1)
        {
            if (transform.tag == "Untagged" && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().StartBitch();
            }
        }
        if (Type == -2)
        {
            if (transform.GetChild(0).GetComponent<Speaker>().Finished && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().NoReal();
            }
        }
        else if (Type == 0)
        {
            if (GetComponent<SpriteRenderer>().color == Color.clear && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().Kill();
            }
        }
        else if (Type == 2)
        {
            if (GetComponent<Speaker>().Finished && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().Fuck();
            }
        }
        else if (Type == 3)
        {
            if (transform.position.y < -15 && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().GateOpen();
            }
        }
        else if (Type == 4)
        {
            if (GetComponent<Speaker>().SpeakIndex >=7 && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().AwakeOldMan();
            }
        }
        else if (Type == 5)
        {
            if (transform.position.y < -15 && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().DigWithShovel();
            }
        }
        else if (Type == 6)
        {
            if (transform.GetChild(0).GetComponent<Speaker>().Finished && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().UnDigWithShovel();
            }
        }
        else if (Type == 7)
        {
            if (transform.GetChild(0).GetComponent<Speaker>().Finished && !NoRepeat)
            {
                NoRepeat = true;
                FindObjectOfType<Mission0>().Finish();
            }
        }
    }
}