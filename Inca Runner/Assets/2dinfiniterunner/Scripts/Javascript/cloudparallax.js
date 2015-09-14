#pragma strict

//this script is for the clouds in the background so they move at their own rate, giving them a parallax feel.

//this is the base speed we give the clouds.
var speed:float = 8.0;

//we find the camera to reference its position.
private var cam:GameObject;

function Start () {
	//here we find the camera and apply it to cam
	cam = GameObject.Find("Main Camera");
	//now we add or subtract a random amount of speed to give each cloud their own unique speed. it looks nice this way.
	speed += Random.Range(-1,1);
}

function Update () {
	//now we apply the speed to the cloud
	transform.position.x -= Time.deltaTime*speed;

	//if the cloud goes too far to the left, we move it to the right side of the screen so they can pan to the left again infinitely
	if(transform.position.x < cam.transform.position.x - 22){
		transform.position.x += 44;
	}
}