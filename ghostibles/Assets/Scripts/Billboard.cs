using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    // Called after regular Update function so it's perfect so camera does all it's movement then we point towards it
    void LateUpdate()
    {   
        // Billboard effect
        transform.LookAt(transform.position + cam.forward);
    }
}
