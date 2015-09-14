#pragma strict

//we hold the gameover text because we'll receive a message from score to do a gameover flash
var gameOverText:GameObject;

//private variables that we keep track of based on what happens in the game
private var gameOver = false;
private var isMenu = false;
private var player:GameObject;

function Start () {
	//we find the player and apply it to the variable player
	player = GameObject.Find("Player");
	//if the player is null from the start, we're assuming that the flash is in the menu scene, or at least not in the actual gameplay scene
	if(player == null){
		isMenu = true;
	}
	//we make sure the alpha of the guiTexture is set right. 0.5 is actually %100 opacity.
	GetComponent.<GUITexture>().color.a = 0.5;
}

function Update () {
	//if ismenu is not true, then we do stuff for the game scene
	if(isMenu != true){
		//if its not a gameover and the alpha color is greater than 0, we subtract alpha to make it fade out
		if(GetComponent.<GUITexture>().color.a > 0 && gameOver == false){
			GetComponent.<GUITexture>().color.a -= Time.deltaTime/2;
		}
		//if gameover is true, then we limit the alpha to 0.35 to give it a faded look when theres a game over
		if(GetComponent.<GUITexture>().color.a > 0.35 && gameOver == true){
			GetComponent.<GUITexture>().color.a -= Time.deltaTime/2;
		}
		//if the alpha is less than or equal to 0 and the guitexture is enabled, we turn it off
		if(GetComponent.<GUITexture>().color.a <= 0 && GetComponent.<GUITexture>().enabled == true && gameOver == false){
			GetComponent.<GUITexture>().enabled = false;
		}
		//if the alpha is greater than 0 and the guitexture is disabled, we turn it back on.
		if(GetComponent.<GUITexture>().color.a > 0 && GetComponent.<GUITexture>().enabled == false && gameOver == false){
			GetComponent.<GUITexture>().enabled = true;
		}
	}else{
	//if it is the menu, we just do this and thats it.
		if(GetComponent.<GUITexture>().color.a > 0.25){
			GetComponent.<GUITexture>().color.a -= Time.deltaTime/2;
		}
	}
//end of function update
}

//if we receive a message from score, we do stuff
function gameOverFlash () {
	//we reset the alpha so it flashes again
	GetComponent.<GUITexture>().color.a = 0.5;
	//make sure the guitexture is turned on
	GetComponent.<GUITexture>().enabled = true;
	//set game over to true
	gameOver = true;
	//move it to a different depth in the gui layer
	transform.position.z = 0;
	//and turn on the game over text/menu
	gameOverText.SetActive(true);
}