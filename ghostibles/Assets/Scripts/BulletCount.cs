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
        bullet.text = "Bullets Left: " + maxBullet;
        // print("start point" + total_score);
    }

    public void UpdateBullets(int count){
        maxBullet = count;
        bullet.text = "Bullets Left: " + maxBullet;

    }

    //Add bullet. Called from Target.cs
    public void AddBullet(int count){
        print("AT ADDBULLET" + maxBullet);
        maxBullet += count;
        print("MAXB "+ maxBullet);
        //Start();
    }

    public void MinusBullet(int bullets)
    {
              
        bullet.text = "Bullets Left: " + bullets;
    }
}
