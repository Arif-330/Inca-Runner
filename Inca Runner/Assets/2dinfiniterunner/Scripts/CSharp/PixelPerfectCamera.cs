using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsToUnits = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution = new Vector2 (720, 1280);
	
	void Awake () {
		var camera = GetComponent<Camera> ();
		pixelsToUnits = 30f;
		scale = 1f;
		if (camera.orthographic) {
			scale = Screen.height/nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
		}
	}

}
