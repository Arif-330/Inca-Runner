using UnityEngine;
using System.Collections;

public class pickup : MonoBehaviour {

	//we use these 8 textures to animate the pickup object
	public Sprite[] pickupAnimation;
	//frame rate of animation
	public float frameRate = 8.0f;
	//we have 2 sounds for when its picked up or missed
	public AudioClip pickupSound;
	public AudioClip lostSound;
	
	//private variables that we use to help animate and keep track of when its picked up or missed
	private float counter = 0.0f;
	private SpriteRenderer rend;
	private int i = 0;
	private GameObject player;
	private GameObject scoreGUI;
	private bool lostMultiplyer = false;
	private bool grabbedPickup = false;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		//we find the player and apply it to the variable player to keep track of where he is
		player = GameObject.Find("Player");
		//we find the scoreGUI object to send him messages based on picking it up or missing it for showing multiplyers and changing how fast the score counts up
		scoreGUI = GameObject.Find("GUI/score");
	}
	
	void Update () {
		//we use this counter to keep track of time for animating the pickup
		counter += Time.deltaTime*frameRate;
		//now we apply the animation based on counter, i and the length of the sprite array
		if(counter > i && i < pickupAnimation.Length){
			rend.sprite = pickupAnimation[i];
			i += 1;
		}
		//reset counter and i so animation can loop seamlessly
		if(counter > pickupAnimation.Length){
			counter = 0.0f;
			i = 0;
		}
		
		//if the player is not null, we check to see where he is
		if(player != null){
			//if the player is past the pickup, then we send a message to the scoreGUI that it was missed and multiplyer needs to be reset
			if(player.transform.position.x > transform.position.x + 3 && lostMultiplyer == false && grabbedPickup == false){
				scoreGUI.SendMessage("lostMultiplyer", SendMessageOptions.DontRequireReceiver);
				//we play the lost sound
				GetComponent<AudioSource>().PlayOneShot(lostSound);
				//and set lostmultiplyer to true so that this can't happen again.
				lostMultiplyer = true;
			}
		}
		
	}
	
	//if we hit a trigger we check to see what hit it.
	void OnTriggerEnter2D (Collider2D other) {
		//if the player hit the pickup, then we do stuff
		if(other.tag == "Player" && grabbedPickup == false){
			//we set grabbedpickup to true so it can't happen more than once
			grabbedPickup = true;
			//remove pick up object from screen 
			//this.gameObject.SetActive (false);
			//then start the getmultiplyer function
			StartCoroutine(getMultiplyer());
		}
	}
	
	IEnumerator getMultiplyer () {
		//we send a message to the scoreGUI so it will add 1 to the multiplyer
		//scoreGUI.SendMessage("addMultiplyer", SendMessageOptions.DontRequireReceiver);
		//play the pickup sound
		//GetComponent<AudioSource>().PlayOneShot(pickupSound);
		//turn off the renderer
		GetComponent<Renderer>().enabled = false;

		//wait for a bit so the sound can play
		yield return new WaitForSeconds(0.25f);

		//then destroy the object.
		Destroy(this.gameObject);
	}
}
