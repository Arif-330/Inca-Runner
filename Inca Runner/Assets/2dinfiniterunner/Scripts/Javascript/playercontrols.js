#pragma strict

//how fast the player walks
var speed:float = 14.0;
//how high the player can jump
var jumpHeight:float = 8.0;
//at what point the level resets if the player falls in a hole
var fallLimit:float = -10;
//how fast the player recovers from being pushed back
var recoverySpeed:float = 1.5;
//jump sound
var jumpSound:AudioClip;

var standing:GameObject;
var sliding:GameObject;

//we use a raycast hit to check how far the player is away from the ground and ceiling
private var hitDown:RaycastHit2D;
private var hitUp:RaycastHit2D;
//using this to ensure the jump sound doesn't play more than once at a time.
private var jumpCounter:float = 0.0;
//we use this statement to allow jumping to happen or not.
private var isGrounded = false;
//here we get the camera to reference its position.
private var cam:GameObject;
//here we get the flash so we can tell it to do something if we die
private var flash:GameObject;
//we use this to save the original speed when starting the game.
private var origSpeed:float = 0.0;
//we use this to check to see if we're standing or not
private var isStanding = true;
//access the animator for running
private var anim:playeranimator;
//get the box collider attached to player
private var boxCol:BoxCollider2D;
private var isDead = false;
private var soundCounter:float = 0.0;

function Start () {
	boxCol = GetComponent(BoxCollider2D);
	anim = standing.GetComponent(playeranimator);
	//we make sure the sliding animation for the character is deactivated
	sliding.SetActive(false);
	//we find the flash object
	flash = GameObject.Find("flash");
	//we find the main camera and pair it to cam
	cam = GameObject.Find("Main Camera");
	//we send a message to the camera with our speed.
	cam.SendMessage("receiveSpeed", speed);
	//we set the origspeed to speed so we can keep track of it while playing
	origSpeed = speed;
}

function Update () {

	soundCounter += Time.deltaTime;

	hitDown = Physics2D.Raycast(transform.position, -Vector2.up);

	if(hitDown.distance < transform.localScale.y+0.15 && !hitDown.collider.isTrigger){
		if(!isGrounded){
			isGrounded = true;
		}
		anim.setGrounded(true);
	}else{
		if(isGrounded){
			isGrounded = false;
		}
		anim.setGrounded(false);
	}

	//if the player gets behind the camera too much, we add a bit of speed so he recovers.
	if(cam.transform.position.x > transform.position.x + 1 && isStanding == true){
		//we add recovery speed to origspeed and make that the new speed
		speed = origSpeed + recoverySpeed;
	}

	//NEW LINE FOR FASTER SLIDING RECOVERY
	if(cam.transform.position.x > transform.position.x + 1 && isStanding == false){
		//we add recovery speed to origspeed and make that the new speed
		speed = origSpeed + recoverySpeed;
	}

	//if the player gets too far ahead he'll slow down instead of speed up
	if(cam.transform.position.x < transform.position.x - 1 && speed == origSpeed){
		speed = origSpeed - recoverySpeed;
	}

	//if the player is back to the middle, we make the speed normal again.
	if(cam.transform.position.x > transform.position.x - 1 && cam.transform.position.x < transform.position.x + 1 && speed != origSpeed){
		speed = origSpeed;
	}

	//here we apply the speed to the character
	GetComponent.<Rigidbody2D>().velocity.x = speed;

	hitUp = Physics2D.Raycast (transform.position, Vector2.up);

	#if UNITY_WEBPLAYER || UNITY_STANDALONE
	//Keyboard Controls for web versions (Same as Standalone because they both deal with keyboard)
	//if the player presses w or space, and the player is standing and can jump, we allow him to jump.
	if(Input.GetKey("w") || Input.GetKey("space")){
		if(isStanding == true && jumpCounter < 0.15){
			//we add speed to the jump
			GetComponent.<Rigidbody2D>().velocity.y = jumpHeight;
			if(soundCounter > 0.25){
				GetComponent.<AudioSource>().PlayOneShot(jumpSound);
				soundCounter = 0.0;
			}
			jumpCounter = 0.15;
		}
	//if the player is not pressing jump with w or space, we add velocity down. this helps make variable jump heights depending on how long the player holds the button.
	}else{
		GetComponent.<Rigidbody2D>().velocity.y -= 32*Time.deltaTime;
	}

	if(isGrounded){
		jumpCounter = 0.0;
	}else{
		jumpCounter += Time.deltaTime;
	}

	if(Input.GetKey("s")){
		if(isStanding){
			//this turns off isstanding so we can check it while the player is sliding
			doSlide();
		}
	}
	//if the player lets go of s, we get him to stand back up with the same functions as above
	if(Input.GetKeyUp("s")){
		doStand();
	}
	if(!Input.GetKey("s")){
		//if the player is in a sliding area, we force him to stay down
		if(hitUp.distance < 0.4 && isStanding == true && hitUp.collider.isTrigger == false){
			doSlide();
		}
		//if the player is not in a sliding area but is still down and not touching the screen, we force him back up.
		if(hitUp.distance > 0.4 && isStanding == false){
			doStand();
		}
	}

	#endif

	#if UNITY_IOS || UNITY_ANDROID
	//iOS Controls (same as Android because they both deal with screen touches)
	//if the touch count on the screen is higher than 0, then we allow stuff to happen to control the player.
	if(isGrounded){
		jumpCounter = 0.0;
	}else{
		jumpCounter += Time.deltaTime;
	}
	if(Input.touchCount > 0){
		for(var touch1 : Touch in Input.touches) {
			//if the touch is on the top half of the screen, we do jump stuff
			if(touch1.position.y > Screen.height/2){
				//if the player can jump and is standing, then we do the jump
				if(isStanding == true && jumpCounter < 0.15){
					//we add speed to the jump
					GetComponent.<Rigidbody2D>().velocity.y = jumpHeight;
					if(soundCounter > 0.25){
						GetComponent.<AudioSource>().PlayOneShot(jumpSound);
						soundCounter = 0.0;
					}
					jumpCounter = 0.15;
					isGrounded = false;
				}
			}
			//if the touch position is on the bottom half of the screen, we do slide stuff
			if(touch1.position.y < Screen.height/2){
				GetComponent.<Rigidbody2D>().velocity.y -= 32*Time.deltaTime;
				doSlide();
			}else{
				//if its not on the bottom half, we make him stand.
				doStand();
			}
		}
	}else{
		//if the touch count is 0, we add velocity down for the variable jump height, depending on how long the player holds jump
		GetComponent.<Rigidbody2D>().velocity.y -= 32*Time.deltaTime;
		//now we check the raycast up to see if the player is in a sliding area or not
		if(hitUp.distance < 0.4 && isStanding == true && hitUp.collider.isTrigger == false){
			doSlide();
		}
		//if the player is not in a sliding area but is still down and not touching the screen, we force him back up.
		if(hitUp.distance > 0.4 && isStanding == false){
			doStand();
		}
	}
	#endif

	//here we check to see if the player fell or when too far to the left
	if(transform.position.y < fallLimit || transform.position.x < cam.transform.position.x - 13){
		if(!isDead){
			isDead = true;
			doDeath();
		}
	}

//end of function update
}

function doStand () {
	isStanding = true;
	standing.SetActive(true);
	sliding.SetActive(false);
	boxCol.size = Vector2(0.75,2);
	boxCol.offset = Vector2(0,0);
}

function doSlide () {
	isStanding = false;
	sliding.SetActive(true);
	standing.SetActive(false);
	boxCol.size = Vector2(0.75,0.9);
	boxCol.offset = Vector2(0,-0.55);
}

function doDeath () {
	isDead = true;
	//we check to see if flash is there, then we send him a message that its a game over.
	if(flash != null){
		//here we send the message
		flash.SendMessage("gameOverFlash", SendMessageOptions.DontRequireReceiver);
	}
	//and destroy the player
	Destroy(gameObject);
}