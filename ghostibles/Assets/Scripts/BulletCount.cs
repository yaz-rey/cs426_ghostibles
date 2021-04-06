using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BulletCount : MonoBehaviour
{
    public Text bullet;
    public int maxBullet = 10;
 
    // Start is called before the first frame update
    void Start()
    {
        // first bullet message
        bullet.text = "Bullets Left: " + maxBullet;
    }

    // called by Character Controller to update number of bullets
    public void UpdateBullets(int count){
        maxBullet = count;
        bullet.text = "Bullets Left: " + maxBullet;

    }
}
