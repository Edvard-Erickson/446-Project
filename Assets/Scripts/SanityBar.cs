using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SanityBar : MonoBehaviour
{
    public Slider slider;

    public void setMax(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetSanity(float sainty)
    {
        slider.value = sainty;
    }

    public float GetSanity()
    {
        return slider.value;
    }
}
