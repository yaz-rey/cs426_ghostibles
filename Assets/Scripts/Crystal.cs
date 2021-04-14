using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    public Score scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision) {
    	// check player that caused attack 
        if (collision.gameObject.tag == "Player"){
            scoreManager.AddPoint(70);
        	Destroy(gameObject);
        }
    }
}
