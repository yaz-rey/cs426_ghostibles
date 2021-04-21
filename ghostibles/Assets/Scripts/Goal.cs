using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
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
            // https://answers.unity.com/questions/1143629/destroy-multiple-gameobjects-with-tag-c.html
            // destroy ghosts if only one gem left 
            // if(GameObject.FindGameObjectsWithTag("Gem").Length == 0) {
            //     GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
            //     foreach(GameObject ghost in ghosts){
            //        GameObject.Destroy(ghost);
            //     }
            //  }
            scoreManager.AddPoint(100);
        	Destroy(gameObject);
        }
    }


}
