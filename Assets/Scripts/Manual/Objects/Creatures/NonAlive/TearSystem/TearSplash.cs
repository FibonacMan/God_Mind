using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class TearSplash : MonoBehaviour
{
    private void Update()
    {
        if (transform.localScale != Vector3.zero)
        {
            StartCoroutine(Kill());
        }
    }
    IEnumerator Kill()
    {
        transform.GetChild(0).GetComponent<Light2D>().intensity = 0.675f;
        while (transform.localScale.x < 3f)
        {
            yield return new WaitForSeconds(0.025f);
            transform.localScale += Vector3.one * 0.025f * Mathf.Pow(transform.localScale.x, 0.25f);
        }
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
