using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class RoomManager : MonoBehaviour
{
    public Transform Borders;
    public Transform Rooms;
    Vector3[] XMaxs;
    Vector3[] YMaxs;
    float[] RoomLightIntensities;
    float[] RoomLightRadius;
    float[] RoomVingentRatios;
    float[] RoomTempeture;
    float[] RoomHeight;
    float[] RoomBloom;
    float[] RoomSat;
    bool[] RoomChangable;
    public TextAsset Manager;
    public TextAsset[] Dreams;
    Transform RoomerActive;
    GameObject NowlyRoom;
    private void Start()
    {
        RoomerActive = GameObject.FindGameObjectWithTag("Clef").transform;
        string[] Management = Manager.text.Split('\n');
        XMaxs = new Vector3[Management.Length];
        YMaxs = new Vector3[Management.Length];
        RoomLightIntensities = new float[Management.Length];
        RoomLightRadius = new float[Management.Length];
        RoomVingentRatios = new float[Management.Length];
        RoomTempeture = new float[Management.Length];
        RoomHeight = new float[Management.Length];
        RoomBloom = new float[Management.Length];
        RoomChangable = new bool[Management.Length];
        RoomSat = new float[Management.Length];
        Debug.Log(string.Join("\n",Management));
        Debug.Log(Management[11].Split(' ')[0].Split(',')[0]);
        Debug.Log(Management[11].Split(' ')[0].Split(',')[1]);
        Debug.Log(Management[11].Split(' ')[1].Split(',')[0]);
        Debug.Log(Management[11].Split(' ')[1].Split(',')[1]);
        for (int i = 0; i < Management.Length; i++)
        {
            string[] Values = Management[i].Split(' ');
            if (Management[i][0] != 'D')
            {
                XMaxs[i] = new Vector3(Transformation.StringToFloat(Values[0].Split(',')[0].ToString()), Transformation.StringToFloat(Values[0].Split(',')[1].ToString()));
                YMaxs[i] = new Vector3(Transformation.StringToFloat(Values[1].Split(',')[0].ToString()), Transformation.StringToFloat(Values[1].Split(',')[1].ToString()));
                RoomLightIntensities[i] = Transformation.StringToFloat(Values[2]) / 100f;
                RoomLightRadius[i] = Transformation.StringToFloat(Values[3]) / 100f;
                RoomVingentRatios[i] = Transformation.StringToFloat(Values[4]) / 100f;
                RoomTempeture[i] = Transformation.StringToFloat(Values[5]) / 100f;
                RoomHeight[i] = Transformation.StringToFloat(Values[6]) / 100f;
                RoomBloom[i] = Transformation.StringToFloat(Values[7]) / 100f;
                RoomSat[i] = Transformation.StringToFloat(Values[8]);
            }
            RoomChangable[i] = Management[i][0] == 'D';
        }
    }
    void Update()
    {
        if (RoomerActive.transform.position.x > 0)
        {
            for (int i = 0; i < Rooms.childCount; i++)
            {
                Rooms.GetChild(i).gameObject.SetActive(Rooms.GetChild(i).name.Substring(4) == transform.localPosition.x.ToString());
                if (Rooms.GetChild(i).name.Substring(4) == transform.localPosition.x.ToString()) NowlyRoom = Rooms.GetChild(i).gameObject;
            }
            Borders.localPosition = XMaxs[(int)transform.localPosition.x];
            Borders.localScale = YMaxs[(int)transform.localPosition.x];
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Light2D>().intensity = RoomLightIntensities[(int)transform.localPosition.x];
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Light2D>().pointLightOuterRadius = RoomLightRadius[(int)transform.localPosition.x];
            GameObject.FindGameObjectWithTag("Player").transform.parent.GetChild(2).GetChild(0).localPosition = Vector3.up * RoomHeight[(int)transform.localPosition.x];
            if (FindObjectOfType<Volume>().profile.TryGet<Vignette>(out Vignette Vig))
            {
                Vig.intensity.value = RoomVingentRatios[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<WhiteBalance>(out WhiteBalance WhiBla))
            {
                WhiBla.temperature.value = RoomTempeture[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<Bloom>(out Bloom Blo))
            {
                Blo.intensity.value = RoomBloom[(int)transform.localPosition.x];
            }
            if (RoomChangable[(int)transform.localPosition.x] && Dreams[(int)transform.localPosition.x].text.Split('\n').Length > (int)NowlyRoom.transform.Find("Mare").localPosition.x && (int)NowlyRoom.transform.Find("Mare").localPosition.x>0)
            {
                string[] Managment = Dreams[(int)transform.localPosition.x].text.Split('\n')[(int)NowlyRoom.transform.Find("Mare").localPosition.x].Split(' ');
                XMaxs[(int)transform.localPosition.x] = new Vector3(Transformation.StringToFloat(Managment[0].Split(',')[0]), Transformation.StringToFloat(Managment[0].Split(',')[1]));
                YMaxs[(int)transform.localPosition.x] = new Vector3(Transformation.StringToFloat(Managment[1].Split(',')[0]), Transformation.StringToFloat(Managment[1].Split(',')[1]));
                RoomLightIntensities[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[2]) / 100f;
                RoomLightRadius[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[3]) / 100f;
                RoomVingentRatios[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[4]) / 100f;
                RoomTempeture[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[5]) / 100f;
                RoomHeight[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[6]) / 100f;
                RoomBloom[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[7]) / 100f;
                RoomSat[(int)transform.localPosition.x] = Transformation.StringToFloat(Managment[8]);
            }
            if (FindObjectOfType<Volume>().profile.TryGet<ChromaticAberration>(out ChromaticAberration Viga))
            {
                Viga.active = RoomChangable[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<LensDistortion>(out LensDistortion WhiBlaa))
            {
                WhiBlaa.active = RoomChangable[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<FilmGrain>(out FilmGrain Bloa))
            {
                Bloa.active = RoomChangable[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<DepthOfField>(out DepthOfField Blos))
            {
                Blos.active = RoomChangable[(int)transform.localPosition.x];
            }
            if (FindObjectOfType<Volume>().profile.TryGet<ColorAdjustments>(out ColorAdjustments Sat))
            {
                Sat.saturation.value = RoomSat[(int)transform.localPosition.x];
            }
        }
    }
}