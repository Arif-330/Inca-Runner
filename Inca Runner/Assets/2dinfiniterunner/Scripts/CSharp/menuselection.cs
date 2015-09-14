using UnityEngine;
using System.Collections;

public class menuselection : MonoBehaviour {
	

	public void playClick(){
		Application.LoadLevel("StageScreen");
	}

	public void historyClick(){
		Application.LoadLevel("History");
	}  

	public void storeClick(){
		Application.LoadLevel("Store");
	}

}
