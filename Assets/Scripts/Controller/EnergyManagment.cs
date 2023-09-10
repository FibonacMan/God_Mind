using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyManagment : MonoBehaviour
{
    public float EnergyRatio;
    public float RatioDominant;
    public Scrollbar EnergyLevels;
    public float BasedEnergy;
    public float MaxEnergy;
    void Update()
    {
        BasedEnergy = Mathf.Clamp(BasedEnergy, 0, MaxEnergy);
        RatioDominant = EnergyRatio * EnergyRatio * EnergyRatio * EnergyRatio * EnergyRatio * MaxEnergy;
        if (BasedEnergy < MaxEnergy)
        {
            if (BasedEnergy > EnergyRatio * MaxEnergy)
            {
                BasedEnergy += 0.1f;
            }
            else if (BasedEnergy > EnergyRatio * EnergyRatio * MaxEnergy)
            {
                BasedEnergy += 0.075f;
            }
            else if (BasedEnergy > EnergyRatio * EnergyRatio * EnergyRatio * MaxEnergy)
            {
                BasedEnergy += 0.05f;
            }
            else if (BasedEnergy >= 0)
            {
                BasedEnergy += 0.0375f;
            }
        }
        EnergyLevels.size = 0.1f * (Mathf.Clamp(BasedEnergy / MaxEnergy, 0f, 1f) - EnergyLevels.size) + EnergyLevels.size;
    }
    public void DecreaseEnergy(float Value)
    {
        BasedEnergy -= Value;
        EnergyLevels.size -= Value / 2 / MaxEnergy;
    }
    public bool IsEnergyEnough()
    {
        return BasedEnergy > RatioDominant;
    }
}