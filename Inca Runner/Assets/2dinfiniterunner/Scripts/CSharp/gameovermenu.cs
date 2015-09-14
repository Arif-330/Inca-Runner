using UnityEngine;
using System.Collections;

public class gameovermenu : MonoBehaviour {

	//this is a menu that just holds functions that are called by the mouseButton script attached to retry and menu guiText (child objects)
	
	public void doRetry () {
		string getLvlName = Application.loadedLevelName;
		Application.LoadLevel(getLvlName);
	}
	
	public void doMenu () {
		Application.LoadLevel("PeruMenu");
	}
}
