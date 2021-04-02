// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hello test message here
// MonoBehavior is the base class from which every Unity Script Derives
public class MusicMovement : MonoBehaviour
{
    public float speed = 50.0f;
    public float rotationSpeed = 100;
    public float force = 700f;
    public int jumpCount = 0; 
    public int maxJumps = 1;

    public int health = 30;
    public float attackInterval = 2;

    private bool attack = false;

    public GameObject cannon;
    public GameObject bullet;

    Rigidbody rb;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumps;
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime represents the time that passed since the last frame
        //the multiplication below ensures that GameObject moves constant speed every frame
        if (Input.GetKey(KeyCode.W))
            rb.velocity += this.transform.forward * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            rb.velocity -= this.transform.forward * speed * Time.deltaTime;

        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, - rotationSpeed * Time.deltaTime, 0);
        
        if (Input.GetKeyDown(KeyCode.Space)){
            if (jumpCount > 0){
                rb.AddForce(t.up * force);
                jumpCount -= 1;
            }

        }

        // https://answers.unity.com/questions/797799/help-with-on-off-toggle-c.html
        if (Input.GetKeyDown(KeyCode.L)){
            if(attack){
                // disable attack
                Debug.Log("Disabled Attack");
                attack = false;
            }
            else{
                // enable attack
                Debug.Log("Enabled Attack");
                attack = true; 
            }
        
        }

        if (attack){
            GetComponent<MeshRenderer>().material.color = Color.cyan;

            // check time 
            if (attackInterval > 0){
                attackInterval -= Time.deltaTime;
                Debug.Log("Changing time: " + attackInterval);

            }
            // once interval over, decrease immunity for ghost
            else{
                Debug.Log("2 seconds elapsed - attack ghosts");
                CheckGhosts(transform.position, 12);
                attackInterval = 2;
            }
        }
        else{
            if (health == 30){
                GetComponent<MeshRenderer>().material.color = Color.white;
            }
            if(health == 20){
                GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            if(health == 10){
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        

        /*// https://docs.unity3d.com/ScriptReference/Input.html
        if (Input.GetButtonDown("Fire1")){
            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        }*/
        
        if (health == 0){
            Destroy(gameObject);
        }
        // press a button
        // time - for every two seconds remove 10 immunity 


    }
    // https://answers.unity.com/questions/862880/disable-jumping-more-than-once.html
    // Used tag to identity different grounds in which to allow jumping 
    // Limiting jumping more than once 
    // NOT IN USE
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "AllowJump"){
            jumpCount = maxJumps;
        }
        if (collision.gameObject.tag == "Target"){
            if (health > 0){
                health = health - 10;
            }
        }
    }

    // https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
    void CheckGhosts(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Target"){
                // https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html
                hitCollider.SendMessage("DecreaseImmunity");
            }
            
        }
    }
}