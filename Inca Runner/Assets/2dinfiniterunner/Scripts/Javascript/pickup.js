#pragma strict

//we use these 8 textures to animate the pickup object
var pickupAnimation:Sprite[];
//frame rate of animation
var frameRate:float = 8.0;
//we have 2 sounds for when its picked up or missed
var pickupSound:AudioClip;
var lostSound:AudioClip;

//private variables that we use to help animate and keep track of when its picked up or missed
private var counter:float = 0.0;
private var rend:SpriteRenderer;
private var i:int = 0;
private var player:GameObject;
private var scoreGUI:GameObject;
private var lostMultiplyer = false;
private var grabbedPickup = false;

function Start () {
	rend = GetComponent(SpriteRenderer);
	//we find the player and apply it to the variable player to keep track of where he is
	player = GameObject.Find("Player");
	//we find the scoreGUI object to send him messages based on picking it up or missing it for showing multiplyers and changing how fast the score counts up
	scoreGUI = GameObject.Find("GUI/score");
}

function Update () {
	//we use this counter to keep track of time for animating the pickup
	counter += Time.deltaTime*frameRate;
	//now we apply the animation based on counter, i and the length of the sprite array
	if(counter > i && i < pickupAnimation.Length){
		rend.sprite = pickupAnimation[i];
		i += 1;
	}
	//reset counter and i so animation can loop seamlessly
	if(counter > pickupAnimation.Length){
		counter = 0.0;
		i = 0;
	}

	//if the player is not null, we check to see where he is
	if(player != null){
		//if the player is past the pickup, then we send a message to the scoreGUI that it was missed and multiplyer needs to be reset
		if(player.transform.position.x > transform.position.x + 3 && lostMultiplyer == false && grabbedPickup == false){
			scoreGUI.SendMessage("lostMultiplyer", SendMessageOptions.DontRequireReceiver);
			//we play the lost sound
			GetComponent.<AudioSource>().PlayOneShot(lostSound);
			//and set lostmultiplyer to true so that this can't happen again.
			lostMultiplyer = true;
		}
	}

}

//if we hit a trigger we check to see what hit it.
function OnTriggerEnter2D (other : Collider2D) {
	//if the player hit the pickup, then we do stuff
	if(other.tag == "Player" && grabbedPickup == false){
		//we set grabbedpickup to true so it can't happen more than once
		grabbedPickup = true;
		//then start the getmultiplyer function
		getMultiplyer();
	}
}

function getMultiplyer () {
	//we send a message to the scoreGUI so it will add 1 to the multiplyer
	scoreGUI.SendMessage("addMultiplyer", SendMessageOptions.DontRequireReceiver);
	//play the pickup sound
	GetComponent.<AudioSource>().PlayOneShot(pickupSound);
	//turn off the renderer
	GetComponent.<Renderer>().enabled = false;
	//wait for a bit so the sound can play
	yield WaitForSeconds(0.25);
	//then destroy the object.
	Destroy(gameObject);
}