#pragma strict

function OnTriggerEnter2D (other : Collider2D){
	if(other.tag == "Player"){
		var getScore = GameObject.Find("GUI/score");
		getScore.SendMessage("addPoint", SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
	}
}