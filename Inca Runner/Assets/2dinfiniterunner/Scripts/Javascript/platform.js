#pragma strict

//this script destroys platforms if they go too far off screen to the left so we keep the hierarchy clean during play
//this script also will activate the pickup item for multiplyers at random

//this is the pickup object that 
var pickup:GameObject;
//this can be turned on in the inspector if you don't want to use the multiplyer pickups.
var canSpawnPickup = true;

//this finds the camera to reference its position
private var cam:GameObject;

function Start () {
	//here we find the camera and apply it to cam
	cam = GameObject.Find("Main Camera");

	//if the pickup exists and canspawnpickup is true, we choose a random number in a range so the pickups only happen once in awhile.
	if(pickup != null && canSpawnPickup == true){
		var randPick:int = Random.Range(1,11);
		//if the randpick = 4, we activate the pickup so the player can grab it.
		if(randPick == 4){
			pickup.SetActive(true);
		//if it didn't equal 4, then we make sure its not active.
		}else{
			pickup.SetActive(false);
		}
	}
	//if canspawnpickup is not true, we make sure its deactivated.
	if(canSpawnPickup == false){
		pickup.SetActive(false);
	}
//end of function start
}

function Update () {
	//if the position of object is less than the camera minus 32, we destroy it to keep the hierarchy clean
	if(transform.position.x < cam.transform.position.x - 256){
		Destroy(gameObject);
	}
}