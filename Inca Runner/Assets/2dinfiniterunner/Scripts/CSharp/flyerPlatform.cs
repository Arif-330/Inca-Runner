using UnityEngine;
using System.Collections;

public class flyerPlatform : MonoBehaviour {

	public int minHeight = -5;
	public int maxHeight = 5;
	
	private GameObject cam;
	
	void Start () {
		cam = GameObject.Find("Main Camera");
		transform.position = new Vector3(transform.position.x,Random.Range(minHeight,maxHeight+1),0);
	}
	
	void Update () {
		//if the position of object is less than the camera minus 32, we destroy it to keep the hierarchy clean
		if(transform.position.x < cam.transform.position.x - 32){
			Destroy(gameObject);
		}
	}
}
