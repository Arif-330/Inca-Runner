#pragma strict

var sendMessageUp:String = "";
var standalone = true;
var mobile = true;

private var over = false;

function Start () {
	#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if(!standalone){
			gameObject.SetActive(false);
		}
	#endif
	#if UNITY_IOS || UNITY_ANDROID
		if(!mobile){
			gameObject.SetActive(false);
		}
	#endif
}

function OnMouseEnter () {
	over = true;
}

function OnMouseExit () {
	over = false;
}

function Update () {
	if(Input.GetMouseButtonUp(0)){
		if(over){
			SendMessageUpwards(sendMessageUp, SendMessageOptions.DontRequireReceiver);
		}
	}
}