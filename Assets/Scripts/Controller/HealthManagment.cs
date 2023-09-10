using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManagment : MonoBehaviour
{
    public GameObject HealthLevels;
    public float BasedHealth;
    public float MaxHealth;
    public ParticleSystem BloodEffect;
    void Update()
    {
        BasedHealth = Mathf.Clamp(BasedHealth, 0, MaxHealth);
        if (BasedHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("Dead").transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<InteractManager>().NoMove();
        }
    }
    public void DecreaseHealth(float Value)
    {
        BloodEffect.Play();
        BasedHealth -= Value;
        for (int i = 0; i < Value; i++)
        {
            int Range = (int)Mathf.Floor((BasedHealth + Value - i) / (MaxHealth / 25f)) - (int)Mathf.Floor(Mathf.Floor((BasedHealth + Value - i) / (MaxHealth / 25f)) / 5) * 5;
            Debug.Log((int)Mathf.Floor(Mathf.Floor((BasedHealth + Value - i) / (MaxHealth / 25f)) / 5) + "" + Range);
            HealthLevels.transform.GetChild(4 - Mathf.Clamp((int)Mathf.Floor(Mathf.Floor((BasedHealth + Value - i) / (MaxHealth / 25f)) / 5),0,4)).GetChild(Range).GetComponent<HeartUI>().Left=true;
        }
    }
    public void FullHealth()
    {
        BloodEffect.Play();
        BasedHealth = 100;
        for (int i = 0; i < 100; i++)
        {
            int Range = (int)Mathf.Floor((BasedHealth - 100 + i) / (MaxHealth / 25f)) - (int)Mathf.Floor(Mathf.Floor((BasedHealth - 100 + i) / (MaxHealth / 25f)) / 5) * 5;
            Debug.Log((int)Mathf.Floor(Mathf.Floor((BasedHealth - 100 + i) / (MaxHealth / 25f)) / 5) + "" + Range);
            HealthLevels.transform.GetChild(4 - Mathf.Clamp((int)Mathf.Floor(Mathf.Floor((BasedHealth - 100 + i) / (MaxHealth / 25f)) / 5), 0, 4)).GetChild(Range).GetComponent<HeartUI>().Left = false;
        }
    }
}