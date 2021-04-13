using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
        private Gradient gradient;
    [SerializeField]
    private Image fill;

    public void setHealth(int health) {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        
        // Sets health bar at max (green)
        fill.color = gradient.Evaluate(1f);
    }
}
