// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior is the base class from which every Unity Script Derives
public class MusicMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float rotationSpeed = 90;
    public float force = 700f;
    public int jumpCount = 0; 
    public int maxJumps = 1;

    public int health = 30;

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

        // https://docs.unity3d.com/ScriptReference/Input.html
        if (Input.GetButtonDown("Fire1")){
            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        }

        if (health == 0){
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }
    // https://answers.unity.com/questions/862880/disable-jumping-more-than-once.html
    // Used tag to identity different grounds in which to allow jumping 
    // Limiting jumping more than once 
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
}