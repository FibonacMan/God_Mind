using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    public string Language = "English";
    public int LanguageID;
    public AudioMixerGroup MainMixer;
    public Scrollbar MasterVolume;
    public Dropdown LanguageDrop;
    public TextAsset[] UILanguages;
    public TextAsset[] CreditLanguages;
    public Text[] UIs;
    public Text[] Credits;
    private void Start()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English: Language = "English"; LanguageID = 0; break;
            case SystemLanguage.Turkish: Language = "Turkish"; LanguageID = 1; break;
        }
        foreach(AudioSource Nowly in FindObjectsOfType<AudioSource>())
        {
            Nowly.outputAudioMixerGroup = MainMixer;
        }
        MainMixer.audioMixer.SetFloat("Master", Mathf.Log10(MasterVolume.value) * 80 + 15);
    }
    public void UpdateMusics()
    {
        foreach (AudioSource Nowly in FindObjectsOfType<AudioSource>())
        {
            Nowly.outputAudioMixerGroup = MainMixer;
        }
    }
    public void SetSettings()
    {
        Language = LanguageDrop.captionText.text;
        LanguageID = LanguageDrop.value;
        MainMixer.audioMixer.SetFloat("Master", Mathf.Log10(MasterVolume.value) * 80 + 15);
        for(int i = 0; i < UIs.Length; i++)
        {
            UIs[i].text = UILanguages[LanguageID].text.Split('\n')[i];
        }
        for (int i = 0; i < 2; i++)
        {
            Credits[i].text = CreditLanguages[LanguageID].text;
        }
    }
}
