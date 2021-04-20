using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//For End Scene
using UnityEngine.SceneManagement;//Restarting the scene


public class CharacterController1 : MonoBehaviour
{
	// public BulletCount bulletCountManager;
	public float speed = 25.0f;
	public float rotationSpeed = 90;
	public float force = 700f;

	public int jumpCount = 0; 
    public int maxJumps = 3;

	public GameObject cannon;
	public GameObject bullet;

	public int bulletCount = 10;
	public int musicCount = 8;//For music bar: max value

	Rigidbody rb;
	Transform t;
	Animator anim;

	// gun attack
	float attackInterval = 0.2f;
	bool startTime = false;

	// weapons: gun, instr
	string weapon = "gun"; 

	public int health = 100;

	private 

	// stun attack
    float stunInterval = 2;
    bool stun = false;

	//Audio clips
	public AudioSource gunShot;
	public AudioSource guitarClip;
	public AudioSource losingLaugh;
	

	public AudioClip doorOpen; 

	private bool guitarPlaying = false;
	private bool gunBlast = false;

	private bool doorOpening = false;

	private bool hasGem = false;


	//public BulletCount bulletManager;
	public WeaponIconManager wiManager;

	public AudioSource audio;

	public AudioSource background;
	public AudioClip ambientMansion;

	public AudioClip bossMusic;

	public AudioSource self;
	public AudioClip pain;
	public AudioClip laugh;
	public AudioClip cool;

	public AudioClip footStep;



	public AudioSource collis;
	public AudioClip gemCollect;
	public AudioClip crystalCollect;
	public AudioClip ammoCollect;

	public AudioSource steps;


	public AudioSource[] sounds;
	public AudioSource permaGun;
	public AudioSource winningMusic;

	//Game over scene, hopefully
	public Image endScene;
	public Text gameOver;
	public Button restart;
	public bool gameIsOver = false;

	//Won game scene, hopefully
	public Image winScene;
	public Text wonGame;
	public Button replay;
	public bool gameWon = false;
	
	public HealthBar healthBar;

	public BulletBar bulletBar;

	public MusicBar musicBar;
	public bool preventPlayingSong = false;

	private bool fightingBoss = false;

	private GameObject sphereField;

	//Timer for music playing, hopefully
	float timer = 0;
	float waitTime = 1;
	public Material musicField;//https://www.youtube.com/watch?v=IQ7qnMv01Vs
	//public event EventHandler lost;

	// Start is called before the first frame update

	private void Step() {
		steps.Play();
	}
	void Start()
	{
		sphereField = transform.GetChild(5).gameObject;
		sphereField.SetActive(false);
		jumpCount = maxJumps;
		restart.onClick.AddListener(RestartButton);//Doesn't really work, but I left it!
		replay.onClick.AddListener(RestartButton);
		rb = GetComponent<Rigidbody>();
		t = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		//https://answers.unity.com/questions/175995/can-i-play-multiple-audiosources-from-one-gameobje.html
		//https://www.youtube.com/watch?v=jFjg2iwuF1s
		sounds = GetComponents<AudioSource>();
		gunShot = sounds[0];
		
		//musicField.SetColor("Color_f41dcbf5ce4e4", Color.white);
		guitarClip = sounds[1];

		audio = sounds[2];

		background = sounds[3];
		self = sounds[4];
		collis = sounds[5];
		losingLaugh = sounds[6];
		winningMusic = sounds[7];
		steps = sounds[8];


		//Start of with no sounds
		gunShot.Stop();
		guitarClip.Stop();
		audio.Stop();
		losingLaugh.Stop();
		winningMusic.Stop();

		// Sets maximum value for health bar 
		healthBar.setMaxHealth(health);
		// Sets maximum value for bullet bar
		bulletBar.setMaxBullets(bulletCount);
		//Sets maximum value for music bar
		musicBar.SetMaxMusic(musicCount);
	}

	// Update is called once per frame
	void Update(){
		if (Input.GetKeyDown(KeyCode.Return))
        {
        	GameObject start = GameObject.Find("StartScreen");
        	if (start.active == true){
        		start.SetActive(false);
        	}

            Debug.Log("Return key was pressed.");
        }
		if(!gameIsOver || !gameWon){
			if (Input.GetKey(KeyCode.Alpha1)){
				wiManager.ChooseGun();
				weapon = "gun";
				Debug.Log("Weapon: GUN");
				stun = false;
				guitarClip.Stop();

			}
			if (Input.GetKey(KeyCode.Alpha2)){
				wiManager.ChooseInstr();
				weapon = "instr";
				Debug.Log("Weapon: INSTRUMENT");
			}

			if (Input.GetKey(KeyCode.W)) 
			{
				rb.velocity += this.transform.forward * speed * Time.deltaTime;
				anim.SetBool("Walk", true);
			}
			else if (!Input.GetKey(KeyCode.W))
			{
				anim.SetBool("Walk", false);
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
				if (jumpCount > 0){
				    rb.AddForce(t.up * force);
				    jumpCount -= 1;
				}
			}

			if (Input.GetButtonDown("Fire1") && weapon.Equals("gun")){
				if(bulletCount > 0){
					gunShot = sounds[0];
					gunShot.Play();//Play gun shot sound as long as we have bullets
				}
				
				anim.SetTrigger("Shoot");
				startTime = true;
			}
			if(fightingBoss) {
				if (background.isPlaying && background.clip != bossMusic){
    				background.Stop();
    				background.volume = 0.7f;
    				background.clip = bossMusic;
    				background.Play();
    			}
			}
		}
		//When player dies
		if(Input.GetKeyDown(KeyCode.R) && health <= 0){
			RestartButton();
		}
		//When player wins
		if(Input.GetKeyDown(KeyCode.R) && gameWon){
			RestartButton();
		}
		if(gameWon){
			//winningMusic.Play();
		}
		//Win scene: when boss killed & two big gems collected. Might change to include smaller bits
		if(GameObject.FindGameObjectsWithTag("Gem").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0){
			
			winScene.gameObject.SetActive(true);
			replay.gameObject.SetActive(true);
			gunShot.Stop();
			guitarClip.Stop();
			audio.Stop();
			losingLaugh.Stop();
			// winningMusic = sounds[7];
			// winningMusic.Play();
			gameWon = true;
			gameIsOver = true;
			PlayMusic();
		}

		if(!gameIsOver){
			//Turn off the gun sound
			if (Input.GetButtonDown("Fire1") && weapon.Equals("gun") == false){
			
				gunShot.Stop();//Play gun shot sound as long as we have bullets
				
			}
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
					bulletBar.setBullets(bulletCount);
					print(bulletCount + " Left");
				}
				startTime = false;
				attackInterval = 0.2f;

			}
		}

		// https://answers.unity.com/questions/797799/help-with-on-off-toggle-c.html
        if (Input.GetKeyDown(KeyCode.L) && weapon.Equals("instr")){
			timer = 4f;//Staart the timer
            if(stun){
                // disable attack
                Debug.Log("Disabled Attack");
                stun = false;
				
				guitarClip.Stop();

				Debug.Log("Stop Music");
				sphereField.SetActive(false);

				musicField.SetColor("Color_f41dcbf5ce4e46109e0abba8c903de3a", Color.white);//Turn music field back to white
				//Disable animation

            }
            else{
				
                // enable attack
                Debug.Log("Enabled Attack");

				//Turn music field to cyan when playing
				sphereField.SetActive(true);
				musicField.SetColor("Color_f41dcbf5ce4e46109e0abba8c903de3a", new Color32(0x1B, 0xC6, 0xE2, 0xFF));//https://answers.unity.com/questions/1395578/how-can-i-use-hex-color.html
				


				guitarClip = sounds[1];
				//Allows playing as long musicCount didn't reach 0 or if the music bar is going up
				if(!preventPlayingSong || musicCount > 0){
					stun = true;
					guitarClip.Play();
				}
				Debug.Log("Play Music");
				// musicCount = musicCount - 2;
				// musicBar.SetMusic(musicCount);

				

            }
			// if(timer > waitTime ){

			// 		timer = timer - waitTime;//Subtract 1 second
			// 		musicCount = musicCount - 2;
			// 		musicBar.SetMusic(musicCount);//Subtract two seconds out of 8
			// 	}
			// 	if(musicCount == 0){
			// 		guitarClip.Stop();//Stop after 4 seconds elapse
			// 	}
        
        }
		// if(musicCount == 0){
		// 	Debug.Log("MUSIC=Count = 0? " + musicCount);
		// 		guitarClip.Stop();//Stop after 4 seconds elapse
		// 	}
		// if(timer > 0 && stun && musicCount > 0){

		// 			timer -= Time.deltaTime;//Subtract 1 second
		// 			Debug.Log("music-count " + musicCount);
		// 			musicCount = musicCount - 2;
		// 			musicBar.SetMusic(musicCount);//Subtract two seconds out of 8
		// 		}
		
		if(!stun && musicCount <= 8 && preventPlayingSong){
			if(timer > 0){
				timer -= Time.deltaTime;
			}
			else{
				timer = 1;
				musicCount = musicCount + 2;
				musicBar.SetMusic(musicCount);

				if(musicCount == 8){preventPlayingSong = false;}
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
                stunInterval = 2;

				//For music bar
				musicCount = musicCount - 2;
				musicBar.SetMusic(musicCount);//Subtract two seconds out of 8

				if(musicCount <= 0){
					guitarClip.Stop(); 
					preventPlayingSong = true;
					stun = false;
					sphereField.SetActive(false);
					musicField.SetColor("Color_f41dcbf5ce4e46109e0abba8c903de3a", Color.white);
					
				}//Stops playing once music bar reaches 0
            }
        }
        else{
        	anim.SetBool("Play", false);
        	// can add if statements about health level here
        }
	}

	public int playWin = 0;
	public void PlayMusic(){
		background.Stop();
		while(playWin < 5){
			winningMusic.Play();
			playWin = playWin + 1;
		}
		
		//winningMusic.Play();
	}
	// https://answers.unity.com/questions/862880/disable-jumping-more-than-once.html
    // Used tag to identity different grounds in which to allow jumping 
    private void OnCollisionEnter(Collision collision){
    	jumpCount = maxJumps;
    	if (collision.gameObject.tag == "Mansion"){
    		if (background.isPlaying && background.clip != ambientMansion){
    			background.Stop();
    			background.volume = 0.4f;
    			background.clip = ambientMansion;
    			background.Play();
    		}
    		
    	}
        // check if stunned ghost is trying to push against you
        if (collision.gameObject.tag == "Ghost"){
        	bool val = collision.gameObject.GetComponent<Target1>().GetStun(); 
            if (health > 0 && !val){
            	self.clip = pain;
            	self.Play();
                health = health - 5;
                healthBar.setHealth(health);
            }
			//Player dies, game over scene displays
			if(health <= 0){
				//Destroy(gameObject); Causes some kind of camera error, restart button won't work
				endScene.gameObject.SetActive(true);//Makie the end scene active once you die
				restart.gameObject.SetActive(true);//Also the restart button
				losingLaugh.Play();
				background.Stop();
				Camera.main.transform.parent = null;//Oh this fixes camera error, restart button suddenly works - who would of thought
				gameIsOver = true;
				//Destroy(gameObject);DESTOYS AMY ,BUT THEN CAN'T CLICK TO RESTART, WE LOSE OBJECT WITH THE SCRIPT'S CODE I'M ASSUMMING
			
			}
        }
        // boss 
        if (collision.gameObject.tag == "Boss"){
            if (health > 0){
            	self.clip = pain;
            	self.Play();

                health = health - 15;
                healthBar.setHealth(health);
            }
			//Player dies, game over scene displays
			if(health <= 0){
				//Destroy(gameObject); Causes some kind of camera error, restart button won't work
				endScene.gameObject.SetActive(true);
				restart.gameObject.SetActive(true);
				losingLaugh.Play();
				Camera.main.transform.parent = null;
				gameIsOver = true;
				//Destroy(gameObject); DESTOYS AMY ,BUT THEN CAN'T CLICK TO RESTART, W LOSE OBJECT I'M ASSUMMING
			
			}
        }
		if (collision.gameObject.tag == "Gem") {
			fightingBoss = true;
			self.clip = laugh;
			self.Play();
			collis.clip = gemCollect;
			collis.Play();
			hasGem = true;
		}
		if (collision.gameObject.tag == "Crystal"){
			self.clip = laugh;
			self.Play();
			collis.clip = crystalCollect;
			collis.Play();
			if (health > 0){
				health += 10;
				healthBar.setHealth(health);
			}
		}
		if (collision.gameObject.tag == "Ammo"){
			self.clip = cool;
			self.Play();
			collis.clip = ammoCollect;
			collis.Play();
			bulletCount += 2;
			Debug.Log("Number of bullets now: " + bulletCount);
			bulletBar.setBullets(bulletCount);

		}
		if(collision.gameObject.tag == "Door") {
			audio = sounds[2];
			audio.Play();
		}
		else {
			Debug.Log("AUDIO STOP");
			audio.Stop();
		}

		
    }

	//Restart button: https://www.youtube.com/watch?v=K4uOjb5p3Io
	public void RestartButton()
	{
		SceneManager.LoadScene("SampleScene");
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

