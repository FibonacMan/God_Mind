using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float Power;
    public float EffectTime = 0.75f;
    private void OnEnable()
    {
        StartCoroutine(KillMe());
    }
    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(EffectTime);
        Destroy(gameObject);
    }
}
