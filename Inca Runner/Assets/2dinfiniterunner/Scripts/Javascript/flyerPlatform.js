#pragma strict

var minHeight:int = -5;
var maxHeight:int = 5;

private var cam:GameObject;

function Start () {
	cam = GameObject.Find("Main Camera");
	transform.position.y = Random.Range(minHeight,maxHeight+1);
}

function Update () {
	//if the position of object is less than the camera minus 32, we destroy it to keep the hierarchy clean
	if(transform.position.x < cam.transform.position.x - 32){
		Destroy(gameObject);
	}
}
