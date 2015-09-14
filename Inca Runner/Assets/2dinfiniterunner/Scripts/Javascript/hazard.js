#pragma strict

//This checks if the player entered, then sends the player a message to call the function doDeath in playercontrols
function OnTriggerEnter2D (other : Collider2D) {
	if(other.tag == "Player"){
		other.SendMessage("doDeath", SendMessageOptions.DontRequireReceiver);
	}
}