using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	// magic influenced
	public int health = 30;
	// music influenced
	public int immunity = 30;

    private void Start(){
    }

    // check when to destroy objects 
    private void Update(){
    	if (health == 0){
            GetComponent<MeshRenderer>().material.color = Color.red;
    		// Destroy(gameObject);
    	}
    	/*if (immunity == 0){
            GetComponent<MeshRenderer>().material.color = Color.cyan;
    		// Destroy(gameObject);
    	}*/

    }

    // https://docs.unity3d.com/ScriptReference/Color.html
    private void DecreaseImmunity(){
        if (immunity > 0){
            immunity = immunity - 10;
        }
        if (immunity == 20){
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        if (immunity == 10){
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        if (immunity == 0){
            GetComponent<MeshRenderer>().material.color = Color.black;
            Destroy(gameObject);
        }
    }

    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision) {
    	// check attack 
        if (collision.gameObject.tag == "MagicAttack"){
        	if (health > 0){
        		health = health - 10;
        	}
        }
        if (collision.gameObject.tag == "MusicAttack"){
        	if (immunity > 0){
        		immunity = immunity - 10;
        	}
        	
        }

        //DecreaseImmunity();
    }
}