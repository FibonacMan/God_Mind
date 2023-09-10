using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
public class PostProcessingEffects : MonoBehaviour
{
    Transform RoomerActive;
    Vignette vignette = null;
    FilmGrain film = null;
    LensDistortion lens = null;
    Bloom bloom = null;
    ColorAdjustments ColorAd = null;
    public AudioSource TypeWriter;
    private void Start()
    {
        RoomerActive = GameObject.FindGameObjectWithTag("Clef").transform;
        if (FindObjectOfType<Volume>().profile.TryGet<Vignette>(out Vignette vignette0))
        {
            vignette = vignette0;
        }
        if (FindObjectOfType<Volume>().profile.TryGet<Bloom>(out Bloom bloom0))
        {
            bloom = bloom0;
        }
        if (FindObjectOfType<Volume>().profile.TryGet<FilmGrain>(out FilmGrain flim0))
        {
            film = flim0;
        }
        if (FindObjectOfType<Volume>().profile.TryGet<LensDistortion>(out LensDistortion lens0))
        {
            lens = lens0;
        }
        if (FindObjectOfType<Volume>().profile.TryGet<ColorAdjustments>(out ColorAdjustments color))
        {
            ColorAd = color;
        }
    }
    public void Madness( int Content, float Time,bool SoundTrue)
    {
        StopAllCoroutines();
        StartCoroutine(GetMad(Content,Time,SoundTrue));
    }
    public void Madnessy(float Time, int Content)
    {
        StopAllCoroutines();
        StartCoroutine(GetMady(Time,Content));
    }
    public void Madness(int Content)
    {
        StopAllCoroutines();
        StartCoroutine(GetMad(Content));
    }
    public void SecMadness(int Content, float Timy, bool SoundTrue)
    {
        StopAllCoroutines();
        StartCoroutine(SecGetMad(Content, Timy, SoundTrue));
    }
    public void Poem(int Content, float Timy, bool SoundTrue, bool Sec)
    {
        StopAllCoroutines();
        StartCoroutine(WriteAPoem(Content, Timy, SoundTrue, Sec));
    }
    IEnumerator WriteAPoem(int Content, float Timy, bool SoundTrue, bool Sec)
    {
        FindObjectOfType<GratiasMovement>().NoMove = true;
        GetRoomer(0);
        float FirstIntensity = ColorAd.saturation.value;
        ColorAd.saturation.value = -100;
        string[] Postals = FindObjectOfType<InteractManager>().Contents[Content].Split('/');
        for (int j = 0; j < Postals.Length; j++)
        {
            FindObjectOfType<GratiasMovement>().SetIntForPlayer(6);
            if (FindObjectOfType<GratiasMovement>().GodMode) break;
            Transformation.Content().text = "";
            string Post = Postals[j];
            for (int i = 0; i < Post.Length; i++)
            {
                if (SoundTrue)
                {
                    TypeWriter.pitch = Random.Range(0.75f, 1.25f);
                    TypeWriter.Play();
                }
                yield return new WaitForSeconds(0.025f);
                Transformation.Content().text += Post[i];
            }
            yield return new WaitForSeconds(Timy);
            if (FindObjectOfType<OptionsManager>().Language == "English") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "press e to continue";
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Devam etmek için e'ye basýn";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        ColorAd.saturation.value = FirstIntensity;
        GetRoomer(1);
        Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        if(Sec) FindObjectOfType<MissionManager>().UpdateTheSection();
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = false;
    }
    IEnumerator SecGetMad(int Content, float Timy, bool SoundTrue)
    {
        FindObjectOfType<GratiasMovement>().NoMove = true;
        FindObjectOfType<CameraManager>().CrackTheCamera(40, 0.25f, 3);
        FindObjectOfType<CameraManager>().DropCamera(true);
        string[] Postals = FindObjectOfType<InteractManager>().Contents[Content].Split('/');
        for (int j = 0; j < Postals.Length; j++)
        {
            if (FindObjectOfType<GratiasMovement>().GodMode) break;
            Transformation.Content().text = "";
            string Post = Postals[j];
            for (int i = 0; i < Post.Length; i++)
            {
                if (SoundTrue)
                {
                    TypeWriter.pitch = Random.Range(0.75f, 1.25f);
                    TypeWriter.Play();
                }
                yield return new WaitForSeconds(0.025f);
                Transformation.Content().text += Post[i];
            }
            yield return new WaitForSeconds(Timy);
            if (FindObjectOfType<OptionsManager>().Language == "English") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "press e to continue";
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Devam etmek için e'ye basýn";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        FindObjectOfType<CameraManager>().DropCamera(false);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<MissionManager>().UpdateTheSection();
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = false;
    }
    IEnumerator GetMad(int Content, float Timy, bool SoundTrue)
    {
        GetRoomer(0);
        FindObjectOfType<GratiasMovement>().NoMove = true;
        FindObjectOfType<CameraManager>().CrackTheCamera(40, 0.25f, 3);
        FindObjectOfType<CameraManager>().DropCamera(true);
        string[] Postals = FindObjectOfType<InteractManager>().Contents[Content].Split('/');
        for (int j = 0; j < Postals.Length; j++)
        {
            if (FindObjectOfType<GratiasMovement>().GodMode) break;
            Transformation.Content().text = "";
            string Post = Postals[j];
            for (int i = 0; i < Post.Length; i++)
            {
                if (SoundTrue)
                {
                    TypeWriter.pitch = Random.Range(0.75f, 1.25f);
                    TypeWriter.Play();
                }
                yield return new WaitForSeconds(0.025f);
                Transformation.Content().text += Post[i];
            }
            yield return new WaitForSeconds(Timy);
            if (FindObjectOfType<OptionsManager>().Language == "English") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "press e to continue";
            if (FindObjectOfType<OptionsManager>().Language == "Turkish") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Devam etmek için e'ye basýn";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        FindObjectOfType<CameraManager>().DropCamera(false);
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = false;
        GetRoomer(1);
    }
    IEnumerator GetMady(float Time, int Content)
    {
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = true;
        FindObjectOfType<CameraManager>().CrackTheCamera(40, 0.25f, 3);
        FindObjectOfType<CameraManager>().DropCamera(true);
        string[] Postals = FindObjectOfType<InteractManager>().Contents[Content].Split('/');
        for (int j = 0; j < Postals.Length; j++)
        {
            if (FindObjectOfType<GratiasMovement>().GodMode) break;
            string Post = Postals[j];
            for (int i = 0; i < Post.Length; i++)
            {

                TypeWriter.pitch = Random.Range(0.75f, 1.25f);
                TypeWriter.Play();
                yield return new WaitForSeconds(0.025f);
                Transformation.Content().text += Post[i];
            }
        }
        yield return new WaitForSeconds(Time);
        FindObjectOfType<CameraManager>().DropCamera(false);
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = false;
    }
    IEnumerator GetMad(int Content)
    {
        GetRoomer(0);
        Transformation.Content().text = "";
        FindObjectOfType<GratiasMovement>().NoMove = true;
        if (FindObjectOfType<OptionsManager>().Language == "English") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "press e to continue";
        if (FindObjectOfType<OptionsManager>().Language == "Turkish") Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Devam etmek için e'ye basýn";
        FindObjectOfType<CameraManager>().CrackTheCamera(40, 0.25f, 3);
        FindObjectOfType<CameraManager>().DropCamera(true);
        string[] Postals = FindObjectOfType<InteractManager>().Contents[Content].Split('/');
        for (int j = 0; j < Postals.Length; j++)
        {
            if (FindObjectOfType<GratiasMovement>().GodMode) break;
            string Post = Postals[j];
            for (int i = 0; i < Post.Length; i++)
            {
                TypeWriter.pitch = Random.Range(0.75f, 1.25f);
                TypeWriter.Play();
                yield return new WaitForSeconds(0.025f);
                Transformation.Content().text += Post[i];
            }
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Transformation.Content().text = "";
        Transformation.Content().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        FindObjectOfType<CameraManager>().DropCamera(false);
        FindObjectOfType<GratiasMovement>().NoMove = false;
        GetRoomer(1);
    }
    public void Close(float Time)
    {
        StartCoroutine(Closed(Time));
    }
    IEnumerator Closed(float Time)
    {
        GetRoomer(0);
        float FirstIntensity = vignette.intensity.value;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.025f * Time);
            vignette.intensity.value += 0.05f;
        }
        for (int i = 0; i < 20; i++)
        {
            if (vignette.intensity.value > FirstIntensity)
            {
                yield return new WaitForSeconds(0.025f * Time);
                vignette.intensity.value -= 0.05f;
            }
            else break;
        }
        GetRoomer(1);
    }
    public void Harm(float Time) { StartCoroutine(Harmed(Time)); }
    IEnumerator Harmed(float Time)
    {
        GetRoomer(0);
        float FirstIntensity = vignette.intensity.value;
        Color BloodColor = new Color(54.12f / 120, 1.18f / 120, 1.18f / 120,1);
        for (int i = 0; i < 100; i++)
        {
            if (vignette.intensity.value < 1f || Vector4.Distance(vignette.color.value, BloodColor) > 0.1f)
            {
                yield return new WaitForSeconds(0.025f * Time);
                vignette.intensity.value += 0.01f;
                vignette.color.value = Vector4.Lerp(vignette.color.value, BloodColor, 0.1f);
            }
            else break;
        }
        for (int i = 0; i < 100; i++)
        {
            vignette.color.value = Vector4.Lerp(vignette.color.value, Color.black, 0.1f);
            if (vignette.intensity.value > FirstIntensity)
            {
                yield return new WaitForSeconds(0.005f * Time);
                vignette.intensity.value -= 0.01f;
            }
            else break;
        }
        vignette.color.value = Color.black;
        GetRoomer(1);
    }
    IEnumerator Shot(Color Type,float Time)
    {
        GetRoomer(0);
        float FirstIntensity = ColorAd.saturation.value;
        Color FirstColor = ColorAd.colorFilter.value;
        ColorAd.saturation.value = 100;
        ColorAd.colorFilter.value = Type;
        yield return new WaitForSeconds(Time);
        ColorAd.saturation.value = FirstIntensity;
        ColorAd.colorFilter.value = FirstColor;
        GetRoomer(1);
    }
    IEnumerator Shot(Color Type)
    {
        GetRoomer(0);
        float FirstIntensity = ColorAd.saturation.value;
        Color FirstColor = ColorAd.colorFilter.value;
        ColorAd.saturation.value = 100;
        ColorAd.colorFilter.value = Type;
        yield return new WaitForSeconds(0.25f);
        ColorAd.saturation.value = FirstIntensity;
        ColorAd.colorFilter.value = FirstColor;
        GetRoomer(1);
    }
    Color ShotChoosed(string ShotName)
    {
        if (ShotName == "Red")
        {
            return Color.red;
        }
        else if (ShotName == "Purple")
        {
            return Color.blue / 2 + Color.red / 2;
        }
        else if (ShotName == "Blue")
        {
            return Color.cyan;
        }
        return Color.black;
    }
    public void Shut(string ShotName, float Time)
    {
        StartCoroutine(Shot(ShotChoosed(ShotName), Time));
    }
    public void Shut(string ShotName)
    {
        StartCoroutine(Shot(ShotChoosed(ShotName)));
    }
    void GetRoomer(float Value)
    {
        RoomerActive.localPosition = Vector3.right * Value;
    }
}
