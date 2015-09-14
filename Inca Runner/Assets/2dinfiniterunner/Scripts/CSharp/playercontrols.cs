using UnityEngine;
using System.Collections;

public class playercontrols : MonoBehaviour {

	//how fast the player walks
	public float speed = 14.0f;

	//how high the player can jump
	public float jumpHeight = 225.0f;
	//how long can jump over 
	public float maxJumpTime=.1f;
	//at what point the level resets if the player falls in a hole
	public float fallLimit = -40.0f;
	//how fast the player recovers from being pushed back
	public float recoverySpeed = 0f;
	//jump sound
	public AudioClip jumpSound;
	
	public GameObject standing;
	public GameObject sliding;
	
	//we use a raycast hit to check how far the player is away from the ground and ceiling
	private RaycastHit2D hitDown;
	private RaycastHit2D hitUp;
	//using this to ensure the jump sound doesn't play more than once at a time.
	private float jumpCounter = 0.0f;
	//we use this statement to allow jumping to happen or not.
	private bool isGrounded = false;
	//here we get the camera to reference its position.
	private GameObject cam;
	//here we get the flash so we can tell it to do something if we die
	private GameObject flash;
	//we use this to save the original speed when starting the game.
	public float origSpeed = 0.0f;

	//we use this to check to see if we're standing or not
	private bool isStanding = true;
	//access the animator for running
	private playeranimator anim;
	//get the box collider attached to player
	private BoxCollider2D boxCol;
	private bool isDead = false;
	private float soundCounter = 0.0f;
	public float gravity = 0.0f;
	private Vector3 speedVector;
	
	public float delayRoll;
	public bool rolling;
	public float beginTouchTime;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	public float minSwipeLength = 200f;
	public float endTouchTime;
	
	public static Swipe swipeDirection;
	public float levelVictory;

	public float timeTracker;
	private int doubleJump;

	public GameObject background;
	public GameObject clouds;
	public GameObject machu;

	private bool isjumped=false;

	void Start () {
		//this  prevents rotation against physics 
		transform.GetComponent<Rigidbody2D>().fixedAngle=true;
		
		boxCol = GetComponent<BoxCollider2D>();
		anim = standing.GetComponent<playeranimator>();
		//we make sure the sliding animation for the character is deactivated
		sliding.SetActive(false);
		//we find the flash object
		flash = GameObject.Find("flash");  
		//we find the main camera and pair it to cam
		cam = GameObject.Find("Main Camera");
		machu = GameObject.Find("backgroundmachu");
		background = GameObject.Find("backgroundback");
		clouds = GameObject.Find("clouds");

		//we send a message to the camera with our speed.
		cam.SendMessage("receiveSpeed", speed);

		//we set the origspeed to speed so we can keep track of it while playing
		origSpeed = speed;
		timeTracker = 0f;
		beginTouchTime = 0f;
		endTouchTime = 0f;
		doubleJump = 0;
		Input.multiTouchEnabled = false;

	}
	
	void Update () {

		soundCounter += Time.deltaTime;
		timeTracker += Time.deltaTime;

		hitDown = Physics2D.Raycast(transform.position, -Vector2.up);
		//Debug.Log ("name of hit down : "+hitDown.collider.name);
		if(hitDown.distance < transform.localScale.y + 0.15 && !hitDown.collider.isTrigger){
			gravity = -0.5f;
			if(!isGrounded){
				doubleJump = 0;
				isStanding = true;
				isGrounded = true;
			}
			anim.setGrounded(true);
		}else{
			if(isGrounded){
				isStanding = false;
				isGrounded = false;
			}
			anim.setGrounded(false);
		}
		
		//if the player gets behind the camera too much, we add a bit of speed so he recovers.
		if(cam.transform.position.x > transform.position.x + 1 && isGrounded == true){
			//we add recovery speed to origspeed and make that the new speed
			speed = origSpeed + recoverySpeed;
		}
		
		//NEW LINE FOR FASTER SLIDING RECOVERY
		//if(cam.transform.position.x > transform.position.x + 1 && isStanding == false){
			//we add recovery speed to origspeed and make that the new speed
			//speed = origSpeed + recoverySpeed; 
		//}
		
		//if the player gets too far ahead he'll slow down instead of speed up
		if(cam.transform.position.x < transform.position.x - 1 && speed == origSpeed && isGrounded){
			speed = origSpeed - recoverySpeed;
		}
		
		//if the player is back to the middle, we make the speed normal again.
		if(cam.transform.position.x > transform.position.x - 1 && cam.transform.position.x < transform.position.x + 1 && speed != origSpeed){
			speed = origSpeed;
		}

		//here we apply the speed to the character
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
		
		hitUp = Physics2D.Raycast (transform.position, Vector2.up);
		
		#if UNITY_WEBPLAYER || UNITY_STANDALONE

		if(Input.GetMouseButton(0)){
			
			
			//Touch t = Input.GetTouch(0);
		
			if (Input.GetMouseButtonDown(0)) {
				
				if(isStanding == true && jumpCounter < .10f || doubleJump == 1){
					isStanding = false;
					//firstPressPos = new Vector2(t.position.x, t.position.y);
					gravity = GetComponent<Rigidbody2D>().velocity.y;
					//we add speed to the jump
					float currentSpeed = GetComponent<Rigidbody2D>().velocity.x;
					if(doubleJump == 1){
						GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 5f,  15f);
					}else {
						GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 5f,  15f);
					}
					doubleJump++;
				}
				
			} else if (Input.GetMouseButtonUp(0)) {
				
				
				
			} else{
				if(isStanding == false){
					gravity = GetComponent<Rigidbody2D>().velocity.y;
					GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 2f, gravity);
				}
			}
			
			
			
			
		}else{
			
			/*
			//if the touch count is 0, we add velocity down for the variable jump height, depending on how long the player holds jump
			if(beginTouchTime < endTouchTime ){
				gravity = GetComponent<Rigidbody2D>().velocity.y;
				speed = origSpeed;
			}else {
				gravity -= 150*Time.deltaTime;
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gravity);
			}*/
			
			gravity -= 95*Time.deltaTime;
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gravity);
			
			//now we check the raycast up to see if the player is in a sliding area or not
			if(hitUp.distance < 0.4f && isStanding == true && hitUp.collider.isTrigger == false){
				doSlide();
			}
			
			//if the player is not in a sliding area but is still down and not touching the screen, we force him back up.
			if(hitUp.distance > 0.4f && isStanding == false){
				doStand();
			}
			
			beginTouchTime += Time.deltaTime;
		}
		
		#endif
		
		#if UNITY_IOS || UNITY_ANDROID
		//iOS Controls (same as Android because they both deal with screen touches)
		//if the touch count on the screen is higher than 0, then we allow stuff to happen to control the player.

		if(isGrounded){
			jumpCounter = 0.0f;
		}else{
			jumpCounter = 15.0f;
		}



		/* Double Tap
		foreach(Touch touch in Input.touches){}
			
			Touch t = Input.GetTouch(0);
			
			if (t.tapCount == 2) {}
		*/

		/*
		if(Input.touches.Length > 0){


			Touch t = Input.GetTouch(0);


			if (t.phase == TouchPhase.Began) {

				if(isStanding == true){
					firstPressPos = new Vector2(t.position.x, t.position.y);
					beginTouchTime = 0f;
					endTouchTime = 0f;
				}

			} else if (t.phase == TouchPhase.Ended) {
				//if (t.tapCount == 1) {

				currentSwipe = new Vector3(t.position.x - firstPressPos.x, t.position.y - firstPressPos.y);
				
				// Make sure it was a legit swipe, not a tap
				if (!(currentSwipe.magnitude < minSwipeLength)) {

				currentSwipe.Normalize();
				float distance = currentSwipe.y;
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
	
					
					gravity = GetComponent<Rigidbody2D>().velocity.y;
					//if(beginTouchTime <= .25f){
						//if the player can jump and is standing, then we do the jump
						if(isStanding == true && jumpCounter < .10f){
							endTouchTime = beginTouchTime;
							//we add speed to the jump
							float currentSpeed = GetComponent<Rigidbody2D>().velocity.x;
							GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 15f,  40f * distance);
							//if(soundCounter > 0.25f){
								GetComponent<AudioSource>().PlayOneShot(jumpSound);
								soundCounter = 0.0f;
							//}
							jumpCounter = 0.15f;
							beginTouchTime = 0f;
						}
					//}
					}
				//}
				}

				if(rolling){
					beginTouchTime = 0f;
					endTouchTime = 0f;
					doStand ();
				}  

			}

			beginTouchTime += Time.deltaTime;

			if(beginTouchTime >= .17f && isGrounded){
				doSlide();
			}
			
		
		}*/




		if(Input.touches.Length > 0){
			
			
			Touch t = Input.GetTouch(0);
			
			
			if (t.phase == TouchPhase.Began) {

				//if(isStanding == true && jumpCounter < .10f || doubleJump == 1){
				if(isStanding == true || doubleJump == 1){
					isStanding = false;
					firstPressPos = new Vector2(t.position.x, t.position.y);
					gravity = GetComponent<Rigidbody2D>().velocity.y;
					Debug.Log("from touch input ");
					//we add speed to the jump
					float currentSpeed = GetComponent<Rigidbody2D>().velocity.x;
					if(doubleJump == 1){
						//GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 5f,  15f);
					}else {
						isjumped=true;

						//start a timer to count jumping time 

						StartCoroutine(countJumpingTime(maxJumpTime));
						GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 5f,  15f);
					}
					doubleJump++;
				}
				
			} else if (t.phase == TouchPhase.Stationary) {
				if (isjumped) {
					Debug.Log ("from stationary phase ");
					GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 5f,  15f);
				}


			} else{
				if(isStanding == false){
					gravity = GetComponent<Rigidbody2D>().velocity.y;
					GetComponent<Rigidbody2D>().velocity = new Vector2(speed + 2f, gravity);
				}
			}


		
			
		}else{

			/*
			//if the touch count is 0, we add velocity down for the variable jump height, depending on how long the player holds jump
			if(beginTouchTime < endTouchTime ){
				gravity = GetComponent<Rigidbody2D>().velocity.y;
				speed = origSpeed;
			}else {
				gravity -= 150*Time.deltaTime;
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gravity);
			}*/

			gravity -= 95*Time.deltaTime;
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, gravity);
			//float speedX=GetComponent<Rigidbody2D>().velocity.x;
			//cam.SendMessage("receiveSpeed", 5f);

			//transform.eulerAngles=new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0f);

			//now we check the raycast up to see if the player is in a sliding area or not
			//if(hitUp.distance < 0.4f && isStanding == true && hitUp.collider.isTrigger == false){
			//	doSlide();
			//}

			//if the player is not in a sliding area but is still down and not touching the screen, we force him back up.
			//if(hitUp.distance > 0.4f && isStanding == false){
			//	doStand();
			//}

			beginTouchTime += Time.deltaTime;
		}
		#endif



		//here we check to see if the player fell or when too far to the left
		if(transform.position.y < fallLimit || transform.position.x < cam.transform.position.x - 14 || transform.position.x > cam.transform.position.x + 14){
			if(!isDead){
				isDead = true;
				doDeath();
			}
		}


	}

	IEnumerator countJumpingTime(float time){
		yield return new WaitForSeconds(time);
		if (isjumped) {
			isjumped=false;
		}
	}

	/*
	public void DetectSwipe ()
	{
		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch(0);
			
			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2(t.position.x, t.position.y);
			}
			
			if (t.phase == TouchPhase.Ended) {
				secondPressPos = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				
				// Make sure it was a legit swipe, not a tap
				if (currentSwipe.magnitude < minSwipeLength) {
					swipeDirection = Swipe.Point;
					return;
				}
				
				currentSwipe.Normalize();
				
				// Swipe up
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Up;
					// Swipe down
				} else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					swipeDirection = Swipe.Down;
					// Swipe left
				} else if (currentSwipe.x < 0  && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Left;
					// Swipe right
				} else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f &&  currentSwipe.y < 0.5f) {
					swipeDirection = Swipe.Right;
				}
				
			}
		} else {
			swipeDirection = Swipe.None;   
		}
	}*/

	void doStand () {
		isStanding = true;
		rolling = false;
		standing.SetActive(true);
		sliding.SetActive(false);
		boxCol.size = new Vector2(0.75f,2.0f);
		boxCol.offset = new Vector2(0,0);
	}
	
	void doSlide () {
		isStanding = false;
		rolling = true;
		sliding.SetActive(true);
		standing.SetActive(false);
		boxCol.size = new Vector2(0.75f,0.9f);
		boxCol.offset = new Vector2(0,-0.55f);
	}
	
	void doDeath () {
		isDead = true;
		cam.SendMessage("receiveSpeed", 0f);
		machu.SendMessage("receiveSpeed", 0f);
		background.SendMessage("receiveSpeed", 0f);
		clouds.SendMessage("receiveSpeed", 0f);

		//we check to see if flash is there, then we send him a message that its a game over.
		if(flash != null){
			//here we send the message
			flash.SendMessage("gameOverFlash", SendMessageOptions.DontRequireReceiver);
		}
		//and destroy the player
		Destroy(gameObject);
	}

	void doWin () {
		//isDead = true;
		//we check to see if flash is there, then we send him a message that its a game over.
		if(flash != null){
			//here we send the message
			flash.SendMessage("victory", SendMessageOptions.DontRequireReceiver);
		}
		//and destroy the player
		//Destroy(gameObject);
	}

}
