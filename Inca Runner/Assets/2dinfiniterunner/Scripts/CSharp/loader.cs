using UnityEngine;
using System.Collections;

public class loader : MonoBehaviour {

	//This is in the Loader scene and will bring us to the menu right away. 
	//This is so we can carry the music manager through the entire game without duplicating it
	
	void Start () {
		//we set the targetframerate to 60. this will make mobile devices shoot for 60 fps to make it super smooth. promise its sooo worth it.
		Application.targetFrameRate = 60;
		//now we load the menu after we got the music manager going
		Application.LoadLevel("PeruMenu");
	}
}
