using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private CharacterController1 player;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && player.HasGem())
        {
            agent.destination = player.gameObject.transform.position;
        }

        if(Vector3.Distance(lastPosition, transform.position) < 0.0001)
        {
            animator.SetFloat("vel", 0);
            print("setting 0");
        }
        else
        {
            animator.SetFloat("vel", 1);
            print("setting 1");
        }
        
        lastPosition = transform.position;
    }
}
