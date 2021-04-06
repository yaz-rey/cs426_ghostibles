using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Target1 : MonoBehaviour
{
    public Score scoreManager;
    //public int bossHitPts = 0;
    //public int knightHitPts = 0;
    public int ghostHitPts = 0;

    public int immunity = 30;

    //private float timer = 0.0F;
    //private float waitTime = 10.0f;
    //public int rounds = 0;//Keep track of Knigh ghost's transformation

    //Create ammunition using the prefab
    public GameObject myPrefab;
    //public BulletCount bCount = new BulletCount();//Increase bCount
    //public CharacterController1 cCount = new CharacterController1();

    private Transform playerTransform;
    bool chasing = false;
    bool waiting = false;
    //private float distanceFromTarget;
    public bool inViewCone;

    // Where is it going and how fast?
   //Vector3 direction;
    //private float walkSpeed = 2f;
    //private int currentTarget;    
    private Transform[] waypoints = null;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        anim = GetComponent<Animator>();  
    }

    void Update(){
        if (chasing){
            //direction = playerTransform.position - transform.position;
            rotateZombie();
        }

        if(!waiting){
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
            //transform.Translate(walkSpeed * direction * Time.deltaTime, Space.World);

        }
    }

      private void FixedUpdate()
    {
        /*if (inViewCone){
            anim.SetTrigger("Attack");
        }*/
        //Debug.Log("HI HERE IS INVIEWCONE in " + gameObject.name + ": " + inViewCone);
        // Give the values to the FSM (animator)
        //distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        //animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
        anim.SetBool("playerInSight", inViewCone);
 
    }

    public void Chase()
      {
          // Load the direction of the player
          //direction = playerTransform.position - transform.position;
          rotateZombie();

      }
    
      public void StopChasing()
      {
          chasing = false;
      }
    
      private void rotateZombie()
      {
          //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
          transform.LookAt(playerTransform);

          //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
         //direction = direction.normalized;
      }
    
      public void StartChasing()
      {
          chasing = true;
      }
    
    
      public void ToggleWaiting()
      {
          waiting  = !waiting;
      }

    
    private void OnCollisionEnter(Collision collision) {
         print("START " +gameObject.name);
        // print(gameObject.tag);


        switch(collision.gameObject.tag){
           /* case "Friendly":
                print("TAG " + collision.gameObject.tag);
                // print("Collision Plane");
                break;*/
            case "Bullet":
                print("TAG " + collision.gameObject.tag);
                //anim.SetTrigger("Attack");
                ProvidePoint();
                break;
            case "Player":
                print("TAG " + collision.gameObject.tag);
                break;
            default:
/*                print("something wrong");
                //bCount.AddBullet();
                bCount = new BulletCount();
                print("Add-bCount");
                bCount.AddBullet();

                cCount = new CharacterController1();
                cCount.AddBullet();
                Destroy(myPrefab);*/
                break;
        } 
        print("TAG-outside " + collision.gameObject.tag);
/*        if(collision.gameObject.name == "Ammo"){
            bCount = new BulletCount();
            print("Add-bCount");
            bCount.AddBullet();
            //Destroy(myPrefab);
        }*/
        // Destroy(gameObject);
    }

    private void ProvidePoint()
    {
        print("GAMETAG " + gameObject.tag);
        switch (gameObject.tag)
        {
            /*
            case "Boss":
                print("BOSS "+ gameObject.tag);
                //gameObject.tag = "Friendly";
                //Begin to damage boss, change colors: TODO make it more complex later
                //There are 3 stages of damage for the boss
                if(bossHitPts == 0){
                    //Change an object's color: https://answers.unity.com/questions/209573/how-to-change-material-color-of-an-object.html
                    //ENDING UP USING THESE 2 ONE INSTEAD TO CHANGE EMISSIVE COLOR: https://answers.unity.com/questions/1211937/change-color-in-c-with-rgb-values.html AND https://answers.unity.com/questions/1019974/how-to-access-emission-color-of-a-material-in-scri.html
                    gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(199F/255F, 36F/255F, 177F/255F));
                    bossHitPts += 1;
                }

                if(bossHitPts == 3){
                    //Change an object's size: https://docs.unity3d.com/ScriptReference/Transform-localScale.html
                    transform.localScale = new Vector3(10F, 10F, 10F);
                    //Code below creates minor bug, it changes the ghost's position. If I just do teh above code, it changes size, but part of the ghost goes underneath
                    //So this bottom code keeps it at surface, kinda but moves it around
                    //https://answers.unity.com/questions/156989/how-get-position-from-a-game-object.html
                    transform.localPosition = GameObject.Find(gameObject.tag).transform.position;//Keep it on the surface,else it goes under the plane
                }
                if(bossHitPts == 5){
                    Destroy(gameObject);
                    scoreManager.AddPoint(100);
                }
                else{
                    bossHitPts += 1;
                }
                
                break;
            case "Knight":
                print("KNIGHT "+ gameObject.tag);
                if(knightHitPts == 0){
                    gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0F/255F, 51F/255F, 102F/255F));
                    knightHitPts += 1;
                }
                
                else if(knightHitPts == 2){
                    timer += Time.deltaTime;
                    knightHitPts += 1;
                    //transform.localScale = new Vector3(2F, 2F, 2F);
                    print("K==2 B4 loop");
                    //Knight ghosts transforms
                    // while(rounds < 5){
                    //     print("WHILE");
                    // //Timer reference: https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
                    //     if(timer > waitTime){
                    //         timer = timer - waitTime;
                    //         rounds += 1;
                    //         print("IF");
                    //         print("TIMER "+ timer);
                    //         transform.localScale = new Vector3(Random.Range(0.25F, 2.0F), 10, Random.Range(0.25F,2.0F));
                            

                    //     }
                    //     transform.localScale = new Vector3(Random.Range(0.25F, 2.0F), Random.Range(0.25F,2.0F), Random.Range(0.25F,2.0F));
                    //     timer += Time.deltaTime;
                    // }//End of loop

                    transform.localScale = new Vector3(10F, 10F, 10F);
                    //transform.localPosition = GameObject.Find(gameObject.tag).transform.position;//Keep it on the surface,else it goes under the plane

                }

                else if(knightHitPts == 4){
                    //Creating prefab: https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
                    myPrefab = Instantiate(myPrefab, new Vector3(5F,5F,5F), Quaternion.identity);
                    myPrefab.tag = "Ammo";
                    Destroy(gameObject);
                    scoreManager.AddPoint(50);
                    //bCount.AddBullet();
                }

                else{
                    knightHitPts += 1;
                }
                
                break;*/
            case "Ghost":
                print("GHOST "+gameObject.tag);
                ghostHitPts += 1;

                if(ghostHitPts == 3){
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
}

