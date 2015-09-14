#pragma strict

//this script adds velocity to the camera based on the speed of the player.

//if we don't receive speed from the player, we still add velocity. this won't happen by default.
private var speed:float = 20;

function Update () {
	//now we keep the camera velocity constant.
	GetComponent.<Rigidbody2D>().velocity.x = speed;
}

//when the player sends the camera a message, it tells it what the velocity should be.
function receiveSpeed (theSpeed : float) {
	speed = theSpeed;
}