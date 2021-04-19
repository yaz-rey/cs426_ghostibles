using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxMusic(int music)
    {
        slider.maxValue = music;
        slider.value = music;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetMusic(int music)
    {
        slider.value = music;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
