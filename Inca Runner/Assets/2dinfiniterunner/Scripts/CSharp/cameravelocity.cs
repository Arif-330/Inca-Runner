using UnityEngine;
using System.Collections;

public class cameravelocity : MonoBehaviour {

	//this script adds velocity to the camera based on the speed of the player.
	
	//if we don't receive speed from the player, we still add velocity.
	public float speed = 10.0f;
	
	void Update () {
		//now we keep the camera velocity constant.
		GetComponent<Rigidbody2D>().velocity = new Vector3(speed,0f,0f);
	}
	
	//when the player sends the camera a message, it tells it what the velocity should be.
	void receiveSpeed (float theSpeed) {
		speed = theSpeed;
	}
}
