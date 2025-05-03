using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaScript : MonoBehaviour
{
    public Slider slider;

    public void setMax(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetStamina(float sainty)
    {
        slider.value = sainty;
    }

    public float GetStamina()
    {
        return slider.value;
    }
}
