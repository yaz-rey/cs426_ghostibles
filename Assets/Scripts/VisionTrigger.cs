using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTrigger : MonoBehaviour{ 
	//http://gamecodeschool.com/unity/simple-patrol-chase-ai-tutorial-with-unity-2d/

	Target1 ghost;

	void Start(){
		// https://docs.unity3d.com/ScriptReference/GameObject.GetComponentInParent.html
		ghost = gameObject.GetComponentInParent(typeof(Target1)) as Target1;
	}


	void OnTriggerEnter(Collider o){
		if (o.gameObject.tag == "Player"){
			Debug.Log("Character Found");
		    ghost.inViewCone = true;
		}
	 }
       
    void OnTriggerExit(Collider o)
    {
    
        if (o.gameObject.tag == "Player"){
        	Debug.Log("Character Not Seen");
            ghost.inViewCone = false;
        }
    }
}
