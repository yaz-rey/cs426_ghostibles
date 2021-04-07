using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Score : MonoBehaviour
{
    public Text scoreText;
    public int maxScore = 5;
 
    int total_score;
 
    // Start is called before the first frame update
    void Start()
    {
        total_score = 0;
        scoreText.text = "Score: " + total_score;
        // print("start point" + total_score);
    }
   
    public void AddPoint(int score)
    {
        // print("hitted" + score);
        total_score += score;       
        scoreText.text = "Score: " + total_score;
        // score++;
 
        // if (score != maxScore && score < maxScore)
        //     scoreText.text = "Score: " + score;
        // else if(score == maxScore)
        //     scoreText.text = "You won!";
    }
}