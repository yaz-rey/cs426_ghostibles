using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
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
    	// check whether player collided 
        if (collision.gameObject.tag == "Player"){
        	Destroy(gameObject);
        }
    }
}
