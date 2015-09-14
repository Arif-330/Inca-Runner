using UnityEngine;
using System.Collections;

public class cloudparallax : MonoBehaviour {

	//this script is for the clouds in the background so they move at their own rate, giving them a parallax feel.
	
	//this is the base speed we give the clouds.
	public float speed = 8.0f;
	
	//we find the camera to reference its position.
	private GameObject cam;
	
	void Start () {
		//here we find the camera and apply it to cam
		cam = GameObject.Find("Main Camera");
		//now we add or subtract a random amount of speed to give each cloud their own unique speed. it looks nice this way.
		speed += Random.Range(-1,1);
	}
	
	void Update () {
		//now we apply the speed to the cloud
		transform.position -= new Vector3(Time.deltaTime*speed,0,0);
		
		//if the cloud goes too far to the left, we move it to the right side of the screen so they can pan to the left again infinitely
		if(transform.position.x < cam.transform.position.x - 22){
			transform.position += new Vector3(44,0,0);
		}
	}
}
