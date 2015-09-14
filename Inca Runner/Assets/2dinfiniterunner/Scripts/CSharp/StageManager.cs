using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

	public Button stageSelector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void changeStage(string rightOrLeft){
		
	}

	public void stageClicked(){
		Application.LoadLevel("LevelScreen");
	}

	public void backClicked(){
		Application.LoadLevel("PeruMenu");
	}
}
