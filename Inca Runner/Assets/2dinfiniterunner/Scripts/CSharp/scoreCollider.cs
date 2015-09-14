using UnityEngine;
using System.Collections;

public class scoreCollider : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other){
		if(other.tag == "Player"){
			GameObject getScore = GameObject.Find("GUI/score");
			getScore.SendMessage("addPoint", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}
}
