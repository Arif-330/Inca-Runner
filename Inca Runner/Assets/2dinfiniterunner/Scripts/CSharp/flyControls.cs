using UnityEngine;
using System.Collections;

public class flyControls : MonoBehaviour {

	//how fast the player walks
	public float speed = 14.0f;
	//how high the player can jump
	public float jumpHeight = 8.0f;
	//at what point the level resets if the player falls in a hole
	public float fallLimit = -10;
	//jump sound
	public AudioClip jumpSound;
	
	//here we get the camera to reference its position.
	private GameObject cam;
	//here we get the flash so we can tell it to do something if we die
	private GameObject flash;
	//used when player dies so functions only call once
	private bool isDead = false;
	//used for mobile jumping
	private bool jumped = false;
	
	void Start () {
		//we find the flash object
		flash = GameObject.Find("flash");
		//we find the main camera and pair it to cam
		cam = GameObject.Find("Main Camera");
		//we send a message to the camera with our speed.
		cam.SendMessage("receiveSpeed", speed);
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
	}
	
	void Update () {
		
		//here we apply the speed to the character
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
		
		#if UNITY_WEBPLAYER || UNITY_STANDALONE
		//Keyboard Controls for web versions (Same as Standalone because they both deal with keyboard)
		//if the player presses w or space, and the player is standing and can jump, we allow him to jump.
		if(Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow)){
			//we add speed to the jump
			if(!jumped){
				jumped = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
				GetComponent<AudioSource>().PlayOneShot(jumpSound);
			}
		}else{
			jumped = false;
		}
		#endif
		
		#if UNITY_IOS || UNITY_ANDROID
		//iOS Controls (same as Android because they both deal with screen touches)
		//if the touch count on the screen is higher than 0, then we allow stuff to happen to control the player.
		if(Input.touchCount > 0){
			//if the touch is on the top half of the screen, we do jump stuff
			if(!jumped){
				jumped = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
				GetComponent<AudioSource>().PlayOneShot(jumpSound);
			}
		}else{
			jumped = false;
		}
		#endif
		
		//here we check to see if the player fell or when too far to the left
		if(transform.position.y < fallLimit || transform.position.y > 9.0f){
			if(!isDead){
				isDead = true;
				doDeath();
			}
		}
		
		//end of function update
	}
	
	void doDeath () {
		isDead = true;
		//we check to see if flash is there, then we send him a message that its a game over.
		if(flash != null){
			//here we send the message
			flash.SendMessage("gameOverFlash", SendMessageOptions.DontRequireReceiver);
		}
		//and destroy the player
		Destroy(gameObject);
	}
}
