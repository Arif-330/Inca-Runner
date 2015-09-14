#pragma strict

//this script just checks to see if its mobile or not. If its mobile we allow the arrows to show up. if its not mobile we delete them.

private var lifeCounter:float = 0.0;

function Start () {
	#if UNITY_WEBPLAYER || UNITY_STANDALONE
		Destroy(gameObject);
	#endif

	#if UNITY_IOS || UNITY_ANDROID
		GetComponent.<GUITexture>().color.a = 0.25;
	#endif
}

function Update () {
	//if the compiled script is for mobile, this will be utilized. we keep track of time so we can do a simple animation to the mobile arrows.
	lifeCounter += Time.deltaTime;
	//if the lifecounter counts to 1.25 seconds, we start subtracting alpha from the color so that it becomes more and more transparent for a fadeout.
	if(lifeCounter > 1.25){
		GetComponent.<GUITexture>().color.a -= Time.deltaTime/2;
	}

	//when alpha hits 0 we destroy the object.
	if(GetComponent.<GUITexture>().color.a <= 0){
		Destroy(gameObject);
	}
}