using UnityEngine;
using System.Collections;

public class mobilecontrolgui : MonoBehaviour {

	//this script just checks to see if its mobile or not. If its mobile we allow the arrows to show up. if its not mobile we delete them.
	
	private float lifeCounter = 0.0f;
	private float alpha = 0.25f;
	
	void Start () {
		#if UNITY_WEBPLAYER || UNITY_STANDALONE
			Destroy(gameObject);
		#endif
		
		#if UNITY_IOS || UNITY_ANDROID
		GetComponent<GUITexture>().color = new Vector4(GetComponent<GUITexture>().color.r,GetComponent<GUITexture>().color.g,GetComponent<GUITexture>().color.b,0.25f);
		#endif
	}
	
	void Update () {
		//if the compiled script is for mobile, this will be utilized. we keep track of time so we can do a simple animation to the mobile arrows.
		lifeCounter += Time.deltaTime;
		//if the lifecounter counts to 1.25 seconds, we start subtracting alpha from the color so that it becomes more and more transparent for a fadeout.
		if(lifeCounter > 1.25f){
			alpha -= Time.deltaTime/2;
		}

		GetComponent<GUITexture>().color = new Vector4(GetComponent<GUITexture>().color.r,GetComponent<GUITexture>().color.g,GetComponent<GUITexture>().color.b,alpha);
		
		//when alpha hits 0 we destroy the object.
		if(alpha <= 0){
			Destroy(gameObject);
		}
	}
}
