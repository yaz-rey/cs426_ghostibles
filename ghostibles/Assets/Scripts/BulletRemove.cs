using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemove : MonoBehaviour
{
	public float bulletLife = 3;

    public Material disappearMaterial;
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
            // var renderer = gameObject.GetComponentInChildren<Renderer>();
            // renderer.material = disappearMaterial;

    	    // Invoke("KillEnemy", 0.5f);
            Destroy(gameObject);
    	}
        
    }

    // void KillEnemy()
    // {
    //     Destroy(gameObject);
    // }
}
