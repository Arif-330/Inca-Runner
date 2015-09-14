  using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	//This script receives messages from the GUITexts for Play and Quit that are children of this object
	
	void startRunner () {
		Application.LoadLevel("runner-game-cs");
	}

	void startFlyer () {
		Application.LoadLevel("flyer-game-cs");
	}
	
	void quitGame () {
		Application.Quit ();
	}
}
