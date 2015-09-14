#pragma strict

//how fast the player walks
var speed:float = 14.0;
//how high the player can jump
var jumpHeight:float = 8.0;
//at what point the level resets if the player falls in a hole
var fallLimit:float = -10;
//jump sound
var jumpSound:AudioClip;

//here we get the camera to reference its position.
private var cam:GameObject;
//here we get the flash so we can tell it to do something if we die
private var flash:GameObject;
//used when player dies so functions only call once
private var isDead = false;
//used for mobile jumping
private var jumped = false;

function Start () {
	//we find the flash object
	flash = GameObject.Find("flash");
	//we find the main camera and pair it to cam
	cam = GameObject.Find("Main Camera");
	//we send a message to the camera with our speed.
	cam.SendMessage("receiveSpeed", speed);
	GetComponent.<Rigidbody2D>().velocity.y = jumpHeight;
}

function Update () {

	//here we apply the speed to the character
	GetComponent.<Rigidbody2D>().velocity.x = speed;

	#if UNITY_WEBPLAYER || UNITY_STANDALONE
	//Keyboard Controls for web versions (Same as Standalone because they both deal with keyboard)
	//if the player presses w or space, and the player is standing and can jump, we allow him to jump.
	if(Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow)){
		//we add speed to the jump
		GetComponent.<Rigidbody2D>().velocity.y = jumpHeight;
		GetComponent.<AudioSource>().PlayOneShot(jumpSound);
	}
	#endif

	#if UNITY_IOS || UNITY_ANDROID
	//iOS Controls (same as Android because they both deal with screen touches)
	//if the touch count on the screen is higher than 0, then we allow stuff to happen to control the player.
	if(Input.touchCount > 0){
		for(var touch1 : Touch in Input.touches) {
			//if the touch is on the top half of the screen, we do jump stuff
			if(!jumped){
				jumped = true;
				GetComponent.<Rigidbody2D>().velocity.y = jumpHeight;
				GetComponent.<AudioSource>().PlayOneShot(jumpSound);
			}
		}
	}else{
		jumped = false;
	}
	#endif

	//here we check to see if the player fell or when too far to the left
	if(transform.position.y < fallLimit || transform.position.y > 9.0){
		if(!isDead){
			isDead = true;
			doDeath();
		}
	}

//end of function update
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