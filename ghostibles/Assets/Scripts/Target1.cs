using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Target1 : MonoBehaviour
{
    public Score scoreManager;

    // check how many hits 
    public int ghostHitPts = 0;

    //Create ammunition using the prefab
    public GameObject myPrefab;

    private Transform playerTransform;

    // states to keep track of for FSM
    bool chasing = false;
    bool waiting = false;
    bool stunned = false;

    // trigger when player is in field of view
    public bool inViewCone;

    Animator anim;

    //Waypoints:https://www.youtube.com/watch?v=GIDz0DjhA4E
    public List<Transform> waypoints = new List<Transform>();//List holds all waypoints
    private Transform targetWaypoint;//Current waypoint destination
    private int targetWaypointIndex = 0;//Index of targetWaypoint
    private float minDistance = 0.1f;//Tell when ghost reached waypoint, closer than minDistance we know ghost has reached point
    private int lastWaypointIndex;
    private float ghostSpeed = 3.0f;
    private float rotationSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();  
        int randomNum = Random.Range(0,waypoints.Count);
        Debug.Log("RANDNUM "+ randomNum);
        targetWaypointIndex = randomNum;
        targetWaypoint = waypoints[targetWaypointIndex];//Start heading to a random waypoint

    }

    void Update(){
        float movementStep = ghostSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        
        //Calculate how much to turn ghosts
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget =Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);//Rotate from current position to the target 

        if (chasing){
            // turn 
            rotateGhost();
        }

        if(!waiting){
            Vector3 forward = transform.forward;
            forward.y = 0.0f;
            transform.Translate(forward * Time.deltaTime * 3, Space.World);

        }

        if (!stunned && waiting){
            CheckDistanceToWaypoint(distance);
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }
    }

    //Get a random number for the wayPoints
    void GetRandomNumber()
    {
        int currWaypointIdx = targetWaypointIndex;//Make a temp of current waypoint index
        targetWaypointIndex = Random.Range(0,waypoints.Count);//Update a new waypoint index randomly

        //Make sure the new waypoint isn't the same to the previous current waypoint destination  
        while(currWaypointIdx == targetWaypointIndex)
        {
            targetWaypointIndex = Random.Range(0,waypoints.Count);
        }
    }

    //Once we're closer than minDistance to waypoint
    void CheckDistanceToWaypoint(float currDistance)
    {
        if(currDistance <= minDistance)
        {
            GetRandomNumber();
            UpdateTargetWaypoint();
        }
    }
    //Update our next waypoint deestination
    void UpdateTargetWaypoint()
    {
        targetWaypoint = waypoints[targetWaypointIndex];
    }

      private void FixedUpdate()
    {
        // this would trigger the attack state if ghost not stunned
        if (!stunned && inViewCone){ 
            anim.SetBool("playerInSight", true);
        }
        else{
            anim.SetBool("playerInSight", false);
        }
 
    }

    // methods to be triggeres by state behaviors in animator FSM
      public void StartChasing()
      {
          chasing = true;
          GetComponent<AudioSource>().Play();
      }
    
      public void StopChasing()
      {
          chasing = false;
          GetComponent<AudioSource>().Stop();
      }
    
      private void rotateGhost()
      {
        Vector3 pos = playerTransform.position;
        pos.y = 0.0f;
        //transform.Translate(forward * Time.deltaTime * 3, Space.World);
        transform.LookAt(pos);
      }
    
      public void ToggleWaiting()
      {
          waiting  = !waiting;
      }

      public void StartWaiting(){
        waiting = true;
      }

      public void StopWaiting(){
        waiting = false;
      }

      public void StartStun()
      {
          stunned  = true;
      }

      public void StopStun()
      {
          stunned  = false;
      }

      public bool GetStun(){
        return stunned;
      }

    
    private void OnCollisionEnter(Collision collision) {
        switch(collision.gameObject.tag){
            case "Bullet":
                print("TAG " + collision.gameObject.tag);
                // decrease ghost health
                ProvidePoint();
                break;
            case "Player":
                print("TAG " + collision.gameObject.tag);
                break;
            default:
                break;
        } 
        print("TAG-outside " + collision.gameObject.tag);
    }

    private void ProvidePoint()
    {
        print("GAMETAG " + gameObject.tag);
        switch (gameObject.tag)
        {
            case "Ghost":
            // increase ghost hits
                print("GHOST "+gameObject.tag);
                ghostHitPts += 1;
                //When normal ghosts die; Boss ghost is on Movement.cs
                if(ghostHitPts == 3){
                    // drop ammo once dead
                    GameObject ammo = GameObject.Instantiate(myPrefab, transform.position, transform.rotation) as GameObject;
                    ammo.tag = "Ammo";
                    Destroy(gameObject);
                    scoreManager.AddPoint(35);
                }
                
                break;
            default:
                break;
        }
    }

     // https://docs.unity3d.com/ScriptReference/Color.html
    private void DecreaseImmunity(){
        Debug.Log("AMY STUN received");
        // triggers state from character controller
        anim.SetTrigger("playerAttacked");
    }
}

