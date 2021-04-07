using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemove : MonoBehaviour
{
	public float bulletLife = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (bulletLife > 0){
    	    bulletLife -= Time.deltaTime;
    	}
    	// once interval over, decrease immunity for ghost
    	else{
    	    Destroy(gameObject);
    	}
        
    }
}
