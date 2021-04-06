using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Score scoreManager;
    public GameObject ammunition;
    public int health = 60;
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
        // https://stackoverflow.com/questions/40752083/how-to-find-child-of-a-gameobject-or-the-script-attached-to-child-gameobject-via
        // https://forum.unity.com/threads/make-gameobject-disappear.381096/
        // get renderer to disable and then enable
        transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(player && player.HasGem())
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
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

        if (health == 0){
            GameObject ammo = GameObject.Instantiate(ammunition, transform.position, transform.rotation) as GameObject;
            ammo.tag = "Ammo";
            GameObject ammo2 = GameObject.Instantiate(ammunition, transform.position + new Vector3(0.5f, 0f, 0f), transform.rotation) as GameObject;
            ammo2.tag = "Ammo";
            scoreManager.AddPoint(50);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet"){
            if (health > 0){
                health -= 10;
            }
        }
    }
}
