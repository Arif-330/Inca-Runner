using UnityEngine;
using System.Collections;

public class highscore : MonoBehaviour {

	//this script just checks what the high score was and applies it to a guitext that shows what the highscore is
	//it will look like HIGHSCORE: 1824 (1824 is an example of whatever the highest score was saved)
	
	void Start () {
		//on start we check the highest score and apply it to the guitext for highscore
		GetComponent<GUIText>().text = "HIGHSCORE: " + PlayerPrefs.GetFloat("highscore").ToString();
	}
	
	void updateScore () {
		//if the script is sent a message to update the score, it does the same thing as start.
		GetComponent<GUIText>().text = "HIGHSCORE: " + PlayerPrefs.GetFloat("highscore").ToString();
	}
}
