using UnityEngine;
using System.Collections;

public class flyerScore : MonoBehaviour {

	public AudioClip pointSound;
	
	private int score = 0;
	
	void Start () {
		updateScore();
	}
	
	void addPoint () {
		score += 1;
		GetComponent<AudioSource>().PlayOneShot(pointSound);
		updateScore();
	}
	
	void updateScore () {
		GetComponent<GUIText>().text = "SCORE: "+score.ToString();
		var getScore = PlayerPrefs.GetInt("flyHighScore");
		if(score > getScore){
			PlayerPrefs.SetInt("flyHighScore", score);
		}
	}
}
