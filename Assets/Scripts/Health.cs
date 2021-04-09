using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public Text healthText;
    public int health = 150;
 

    // Start is called before the first frame update
    void Start()
    {
        //healthText.text = "Health Level: " + health;
        healthText.text = "Health Level: " + health;
    }
   
    public void UpdateHealth(int hlt)
    {
        health = hlt;      
        healthText.text = "Health Level: " + health;

    }
}
