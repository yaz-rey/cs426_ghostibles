using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    
    public void setBullets(int bulletCount) {
        slider.value = bulletCount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxBullets(int bulletCount) {
        
        slider.maxValue = bulletCount;
        slider.value = bulletCount;

        fill.color = gradient.Evaluate(1f);
    }

}
