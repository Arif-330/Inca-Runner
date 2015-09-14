using UnityEngine;
using System.Collections;

public class hazard : MonoBehaviour {

	//This checks if the player entered, then sends the player a message to call the function doDeath in playercontrols
	void OnTriggerEnter2D (Collider2D other) {
		if(other.tag == "Player"){
			other.SendMessage("doDeath", SendMessageOptions.DontRequireReceiver);
		}
	}
}
