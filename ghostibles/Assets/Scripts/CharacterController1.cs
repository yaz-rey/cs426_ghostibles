using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController1 : MonoBehaviour
{
	// public BulletCount bulletCountManager;
	public float speed = 25.0f;
	public float rotationSpeed = 90;
	public float force = 700f;

	public GameObject cannon;
	public GameObject bullet;

	public int bulletCount = 10;

	Rigidbody rb;
	Transform t;
	Animator anim;

	// gun attack
	float attackInterval = 0.2f;
	bool startTime = false;

	// weapons: gun, instr
	string weapon = "gun"; 

	public int health = 150;

	// stun attack
    public float stunInterval = 1;
    private bool stun = false;

	//Audio clips
	public AudioSource gunShot;
	public AudioSource guitarClip;

	private bool guitarPlaying = false;
	private bool gunBlast = false;

	private bool hasGem = false;


	public BulletCount bulletManager;
	public Health healthManager;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		t = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		//https://answers.unity.com/questions/175995/can-i-play-multiple-audiosources-from-one-gameobje.html
		//https://www.youtube.com/watch?v=jFjg2iwuF1s
		AudioSource[] sounds = GetComponents<AudioSource>();
		gunShot = sounds[0];
		
		guitarClip = sounds[1];
		//Start of with no sounds
		gunShot.Stop();
		guitarClip.Stop();
	}

	// Update is called once per frame
	void Update(){

		if (Input.GetKey(KeyCode.Alpha1)){
			weapon = "gun";
			Debug.Log("Weapon: GUN");
		}
		if (Input.GetKey(KeyCode.Alpha2)){
			weapon = "instr";
			Debug.Log("Weapon: INSTRUMENT");
		}

		if (Input.GetKey(KeyCode.W)){
			rb.velocity += this.transform.forward * speed * Time.deltaTime;
		}

		
		else if (Input.GetKey(KeyCode.S)){
			rb.velocity -= this.transform.forward * speed * Time.deltaTime;
		}


		if (Input.GetKey(KeyCode.D)){
			anim.SetBool("RightTurn",true);
			t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
			
		}
		if(Input.GetKey(KeyCode.D) == false){
			anim.SetBool("RightTurn", false);
		}
		//Deleted else if for this so the player can move left or not if 'A' is being clicked on
		if (Input.GetKey(KeyCode.A)){
			anim.SetBool("LeftTurn",true);
			t.rotation *= Quaternion.Euler(0, - rotationSpeed * Time.deltaTime, 0);
			
		}
		if(Input.GetKey(KeyCode.A) == false){
			anim.SetBool("LeftTurn", false);
		}

		if (Input.GetKeyDown(KeyCode.Space)){
			rb.AddForce(t.up * force);
		}

		if (Input.GetButtonDown("Fire1") && weapon.Equals("gun")){
			if(bulletCount > 0){
				gunShot.Play();//Play gun shot sound as long as we have bullets
			}
			
			anim.SetTrigger("Shoot");
			startTime = true;
		}
		//Turn off the gun sound
		if (Input.GetButtonDown("Fire1") && weapon.Equals("gun") == false){
		
			gunShot.Stop();//Play gun shot sound as long as we have bullets
			
		}

		// align shooting with the animation
		if (startTime){
			if (attackInterval > 0){
				attackInterval -= Time.deltaTime;

			}
			else{
				if(bulletCount == 0){
					//bulletCountManager.MinusBullet(bulletCount);
					print("Zero bullets Left");
				}
				else{
					GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
					newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
					newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
					bulletCount--;
					bulletManager.UpdateBullets(bulletCount);
					print(bulletCount + " Left");
				}
				startTime = false;
				attackInterval = 0.2f;

			}

		}

		// https://answers.unity.com/questions/797799/help-with-on-off-toggle-c.html
        if (Input.GetKeyDown(KeyCode.L) && weapon.Equals("instr")){
			
            if(stun){
                // disable attack
                Debug.Log("Disabled Attack");
                stun = false;
				
				guitarClip.Stop();
				Debug.Log("Stop Music");
				anim.SetBool("Play",false);
				//Disable animation

            }
            else{
                // enable attack
                Debug.Log("Enabled Attack");
				stun = true; 
				
				guitarClip.Play();
				Debug.Log("Play Music");
            }
        
        }

        if (stun){
        	anim.SetBool("Play", true);
            GetComponent<MeshRenderer>().material.color = Color.cyan;
            // check time 
            if (stunInterval > 0){
                stunInterval -= Time.deltaTime;
                Debug.Log("Changing time: " + stunInterval);

            }
            // once interval over, decrease immunity for ghost
            else{
                Debug.Log("1 second elapsed - attack ghosts");
                CheckGhosts(transform.position, 14);
                stunInterval = 1;
            }
        }
        else{
        	anim.SetBool("Play", false);
        	// can add if statements about health level here
        }
	}


	// https://answers.unity.com/questions/862880/disable-jumping-more-than-once.html
    // Used tag to identity different grounds in which to allow jumping 
    private void OnCollisionEnter(Collision collision){
        // check if stunned ghost is trying to push against you
        if (collision.gameObject.tag == "Ghost"){
        	bool val = collision.gameObject.GetComponent<Target1>().GetStun(); 
            if (health > 0 && !val){
                health = health - 5;
                healthManager.UpdateHealth(health);
            }
        }
        // boss 
        if (collision.gameObject.tag == "Boss"){
            if (health > 0){
                health = health - 15;
                healthManager.UpdateHealth(health);
            }
        }
		if (collision.gameObject.tag == "Gem") {
			hasGem = true;
		}
		if (collision.gameObject.tag == "Ammo"){
			bulletCount += 2;
			Debug.Log("Number of bullets now: " + bulletCount);
			bulletManager.UpdateBullets(bulletCount);

		}
    }

    // https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
    void CheckGhosts(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
        	// stun ghosts
            if (hitCollider.gameObject.tag == "Ghost"){
                // https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html
                hitCollider.SendMessage("DecreaseImmunity");
            }
            
        }
    }

	public bool HasGem()
	{
		return hasGem;
	}


}

