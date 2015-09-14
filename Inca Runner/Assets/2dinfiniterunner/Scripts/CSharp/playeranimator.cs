using UnityEngine;
using System.Collections;

public class playeranimator : MonoBehaviour {

	//Sprites for running in order
	public Sprite[] run;
	//This defines how fast the textures during running animations switch.
	public float runFrameRate = 12.0f;
	//Sprites for jumping in order
	public Sprite[] jump;
	//This defines how fast the textures during walking animations switch.
	public float jumpFrameRate = 1.0f;
	
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	private bool isGrounded = false;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		//Do the running animation if the the player is grounded.
		if(isGrounded){
			//Base the frame speed based on time. We only use this for the walking animation. Jumping and idle don't use it.
			counter += Time.deltaTime*runFrameRate;
			if(counter > i && i < run.Length){
				rend.sprite = run[i];
				i += 1;
//				Debug.Log("Counter : "+counter);
			}
			if(counter > run.Length){
				counter = 0.0f;
				i = 0;
			}
		}else{
			counter += Time.deltaTime*jumpFrameRate;
			if(counter > i && i < jump.Length){
				rend.sprite = jump[i];
				i += 1;
			}
		}
	}
	
	public void setGrounded (bool result) {
		if(!result && isGrounded){
			counter = 0.0f;
			i = 0;
		}
		isGrounded = result;
	}
}
