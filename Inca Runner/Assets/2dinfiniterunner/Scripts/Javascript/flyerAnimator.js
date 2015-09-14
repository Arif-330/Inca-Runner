#pragma strict

var fly:Sprite[];
var frameRate:float = 8.0;

private var counter:float = 0.0;
private var i = 0;
private var rend:SpriteRenderer;

function Start () {
	rend = GetComponent(SpriteRenderer);
}

function Update () {
	//animate the character
	counter += Time.deltaTime*frameRate;
	if(counter > i && i < fly.Length){
		rend.sprite = fly[i];
		i += 1;
	}
	if(counter > fly.Length){
		counter = 0.0;
		i = 0;
	}
	
	//turn character towards velocity
	var dir = GetComponent.<Rigidbody2D>().velocity;
	var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg/2;
	var q = Quaternion.AngleAxis(angle, Vector3.forward);
	transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 720 * Time.deltaTime); 
}