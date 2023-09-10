using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CrazyText : MonoBehaviour
{
    public bool Closy;
    void Update()
    {
        if (!Closy) Trigger();
        else
        {
            RectTransform TR = GetComponent<RectTransform>();
            Vector3 CamPos = Input.mousePosition;
            float valueX = 2 * TR.sizeDelta.x / (TR.sizeDelta.x + TR.sizeDelta.y);
            float valueY = 2 * TR.sizeDelta.y / (TR.sizeDelta.x + TR.sizeDelta.y);
            Vector2 Dist = new Vector2(Mathf.Abs((CamPos.x - TR.position.x) / valueX), Mathf.Abs((CamPos.y - TR.position.y) / valueY));
            if (Dist.x * Dist.x + Dist.y * Dist.y <= Mathf.Pow((TR.sizeDelta.x + TR.sizeDelta.y) / 2, 2))
            {
                Trigger();
            }
        }
    }
    public void Trigger()
    {
        GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        var TextInfo = GetComponent<TextMeshProUGUI>().textInfo;
        for (int i = 0; i < TextInfo.characterCount; i++)
        {
            var CharInfo = TextInfo.characterInfo[i];
            var verts = TextInfo.meshInfo[CharInfo.materialReferenceIndex].vertices;
            Vector3 Origin = Vector3.zero;
            for (int j = 0; j < 4; j++)
            {
                Origin += verts[CharInfo.vertexIndex + j] / 4;
            }
            for (int j = 0; j < 4; j++)
            {
                var point = verts[CharInfo.vertexIndex + j];
                verts[CharInfo.vertexIndex + j] = new Vector3(point.x, point.y + Random.Range(-2, 2) * 3);
            }
        }
        for (int i = 0; i < TextInfo.meshInfo.Length; i++)
        {
            var MeshInfo = TextInfo.meshInfo[i];
            MeshInfo.mesh.vertices = MeshInfo.vertices;
            GetComponent<TextMeshProUGUI>().UpdateGeometry(MeshInfo.mesh, i);
        }
    }
}