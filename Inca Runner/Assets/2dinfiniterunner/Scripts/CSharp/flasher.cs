using UnityEngine;
using System.Collections;

public class flasher : MonoBehaviour {

	//we hold the gameover text because we'll receive a message from score to do a gameover flash
	public GameObject gameOverScreen;
	
	//private variables that we keep track of based on what happens in the game
	private bool gameOver = false;
	private bool victoryEnabled = false;
	private bool isMenu = false;
	private float alpha = 0.5f;
	private GameObject player;
	public float completeTime;
	public GameObject completed;
	public bool blink;
	
	void Start () {
		//we find the player and apply it to the variable player
		player = GameObject.Find("Player");
		gameOverScreen.SetActive (false);
		//if the player is null from the start, we're assuming that the flash is in the menu scene, or at least not in the actual gameplay scene
		if(player == null){
			isMenu = true;
		}
		//we make sure the alpha of the guiTexture is set right. 0.5 is actually %100 opacity.
		GetComponent<GUITexture>().color = new Vector4(1,1,1,alpha);
	}
	
	void Update () {


		//if ismenu is not true, then we do stuff for the game scene
		if(isMenu != true){

			if(alpha > 0 && gameOver == false) {
				alpha -= Time.deltaTime/2;
			}

			//if its not a gameover and the alpha color is greater than 0, we subtract alpha to make it fade out
			if(alpha > 0 && gameOver == true){
				alpha -= Time.deltaTime/2;
			}

			if(alpha <= 0 && GetComponent<GUITexture>().enabled == true && gameOver == false){
				GetComponent<GUITexture>().enabled = false;
			}

			if(alpha > 0 && GetComponent<GUITexture>().enabled == false && gameOver == false){
				GetComponent<GUITexture>().enabled = true;
			}

			if(!victoryEnabled){
				completed.SetActive(false);
				completeTime = 0;
			}else{
				completeTime += Time.deltaTime;
			}

			//if gameover is true, then we limit the alpha to 0.35 to give it a faded look when theres a game over
			if(blink && victoryEnabled == true){
				//alpha -= Time.deltaTime/2;
				completed.SetActive(false);
				blink = false;
			}
			
			if(!blink && victoryEnabled == true){
				completed.SetActive(true);
				blink = true; 
			}
			
			if(completeTime >= 3f){
				gameOver = false;
				victoryEnabled = false;
				completed.SetActive(false);
				int currentStage = datamanager.instance.getCurrentStage();
				
				if (datamanager.instance.checkHighestLevel (datamanager.instance.getCurrentStage ()) == 10) {
					if(datamanager.instance.getCurrentLevel() == 10){
						datamanager.instance.unlockStage(++currentStage);
						datamanager.instance.setCurrentStageIncrement (); // If completed all stages then unlock new level
						Application.LoadLevel("StageScreen");
					}else{
						datamanager.instance.setCurrentLevelIncrement ();
						int currentLevel = datamanager.instance.getCurrentLevel ();
						datamanager.instance.unlockLevel(currentStage, currentLevel);
						Application.LoadLevel("LevelScreen");
					}
				} else {
					datamanager.instance.setCurrentLevelIncrement ();
					int currentLevel = datamanager.instance.getCurrentLevel ();
					datamanager.instance.unlockLevel(currentStage, currentLevel);
					Application.LoadLevel("LevelScreen");
				}

			}

		}else{
			//if it is the menu, we just do this and thats it.
			if(alpha > 0.25){
				alpha -= Time.deltaTime/2;
			}

		}

		GetComponent<GUITexture>().color = new Vector4(1,1,1,alpha);
		//end of function update
	}
	
	//if we receive a message from score, we do stuff
	void gameOverFlash () {
		//we reset the alpha so it flashes again
		alpha = 0.5f;

		if (!victoryEnabled) {
			GetComponent<GUITexture> ().color = new Vector4 (1, 1, 1, alpha);
			//make sure the guitexture is turned on
			GetComponent<GUITexture> ().enabled = true;
			//set game over to true
			gameOver = true;
			//move it to a different depth in the gui layer
			transform.position = new Vector3 (0.5f, 0.5f, 0);

			gameOverScreen.SetActive (true);
			//and turn on the game over text/menu
		}
	}


	//if we receive a message from score, we do stuff
	void victory () {

		//we reset the alpha so it flashes again
		//alpha = 0.5f;
		 alpha = 0.5f;
		//GetComponent<GUITexture>().color = new Vector4(1,1,1,alpha);
		//make sure the guitexture is turned on
		//GetComponent<GUITexture>().enabled = true;
		if (!gameOver) {
			//set game over to true
			victoryEnabled = true;
			gameOver = false;
			completed.SetActive (true);
		}
		//move it to a different depth in the gui layer
		//transform.position = new Vector3(0.5f,0.5f,0);

		//and turn on the game over text/menu
		//gameOverText.SetActive(true);
	}

	public void doRetry () {
		string getLvlName = Application.loadedLevelName;
		Application.LoadLevel(getLvlName);
	}
	
	public void doMenu () {
		Application.LoadLevel("PeruMenu");
	}

	public void doLevel(){
		Application.LoadLevel ("LevelScreen");
	}
		
}
