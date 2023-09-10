using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Tear;
    public Vector3 SpawnPosition;
    public float SpawnRadius;
    public float WaitTime;
    bool Spawnable = true;
    private void OnEnable()
    {
        Spawnable = true;
    }
    void Update()
    {
        if (transform.GetChild(0).localPosition.x == 1 && Spawnable) StartCoroutine(Spawn());
        
    }
    IEnumerator Spawn()
    {
        Spawnable = false;
        float Angle = Random.Range(0f, Mathf.PI);
        float Dist = Random.Range(0f, SpawnRadius);
        GameObject Spawned = Instantiate(Tear, new Vector3(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Dist + SpawnPosition, Quaternion.identity);
        Spawned.transform.localScale = Vector3.one;
        Spawned.name = Tear.name;
        yield return new WaitForSeconds(WaitTime);
        Spawnable = true;
    }
}
