using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{
    public GameObject Target;
    void Update()
    {
        float LookingDr = Mathf.Rad2Deg * Mathf.Atan2(Target.transform.position.y - transform.position.y, Target.transform.position.x - transform.position.x);
        transform.eulerAngles = new Vector3(0, 0, LookingDr);
    }
}
