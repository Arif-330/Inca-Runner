using UnityEngine;
using System.Collections;

public class flyerAnimator : MonoBehaviour {

	public Sprite[] fly;
	public float frameRate = 8.0f;
	
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		//animate the character
		counter += Time.deltaTime*frameRate;
		if(counter > i && i < fly.Length){
			rend.sprite = fly[i];
			i += 1;
		}
		if(counter > fly.Length){
			counter = 0.0f;
			i = 0;
		}
		
		//turn character towards velocity
		var dir = GetComponent<Rigidbody2D>().velocity;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg/2;
		var q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 720 * Time.deltaTime); 
	}
}
