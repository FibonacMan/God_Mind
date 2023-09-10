using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    float LookingDr;
    bool WalkTrue;
    bool Rotatable = true;
    bool Walkable = true;
    public Vector2[] Borders;
    public float Speed;
    private void OnEnable()
    {
        Rotatable = true;
        Walkable = true;
    }
    private void Update()
    {
        transform.GetChild(0).localEulerAngles = new Vector3(transform.GetChild(0).localEulerAngles.x, transform.GetChild(0).localEulerAngles.y, (LookingDr - transform.GetChild(0).localEulerAngles.z) * 0.1f + transform.GetChild(0).localEulerAngles.z);
        if (WalkTrue)
        {
            Vector3 PossiblePos = Vector3.Lerp(transform.localPosition, transform.GetChild(0).up + transform.localPosition, Speed);
            transform.localPosition = new Vector3(Mathf.Clamp(PossiblePos.x, Borders[0].x, Borders[1].x), Mathf.Clamp(PossiblePos.y, Borders[0].y, Borders[1].y));
        }
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3f)
        {
            LookingDr = Mathf.Rad2Deg * (Mathf.Atan2(GameObject.FindGameObjectWithTag("Player").transform.position.y - transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x) + Mathf.PI);
            WalkTrue = true;
        }
        else if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 10f)
        {
            if (Rotatable)
            {
                StartCoroutine(CanYouRotate());
            }
            if (Walkable)
            {
                StartCoroutine(Walk());
            }
        }
        else WalkTrue = false;
    }
    IEnumerator CanYouRotate()
    {
        Rotatable = false;
        yield return new WaitForSeconds(1f);
        if (Random.Range(0f, 1f) < 0.3f) LookingDr = Random.Range(0f, 360f);
        Rotatable = true;
    }
    IEnumerator Walk()
    {
        Walkable = false;
        WalkTrue = !WalkTrue;
        yield return new WaitForSeconds(Random.Range(1f,2f));
        Walkable = true;
    }
}
