#pragma strict

//Sprites for running in order
var run:Sprite[];
//This defines how fast the textures during running animations switch.
var runFrameRate:float = 12.0;
//Sprites for jumping in order
var jump:Sprite[];
//This defines how fast the textures during walking animations switch.
var jumpFrameRate:float = 1.0;

private var counter:float = 0.0;
private var i:int = 0;
private var rend:SpriteRenderer;
private var isGrounded = false;

function Start () {
	rend = GetComponent(SpriteRenderer);
}

function Update () {
	//Do the running animation if the the player is grounded.
	if(isGrounded){
		//Base the frame speed based on time. We only use this for the walking animation. Jumping and idle don't use it.
		counter += Time.deltaTime*runFrameRate;
		if(counter > i && i < run.Length){
			rend.sprite = run[i];
			i += 1;
		}
		if(counter > run.Length){
			counter = 0.0;
			i = 0;
		}
	}else{
		counter += Time.deltaTime*jumpFrameRate;
		if(counter > i && i < jump.Length){
			rend.sprite = jump[i];
			i += 1;
		}
	}
}

function setGrounded (result : boolean) {
	if(!result && isGrounded){
		counter = 0.0;
		i = 0;
	}
	isGrounded = result;
}