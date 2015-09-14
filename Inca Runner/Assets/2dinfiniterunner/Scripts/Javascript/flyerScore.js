#pragma strict

var pointSound:AudioClip;

private var score:int = 0;

function Start () {
	updateScore();
}

function addPoint () {
	score += 1;
	GetComponent.<AudioSource>().PlayOneShot(pointSound);
	updateScore();
}

function updateScore () {
	GetComponent.<GUIText>().text = "SCORE: "+score.ToString();
	var getScore = PlayerPrefs.GetInt("flyHighScore");
	if(score > getScore){
		PlayerPrefs.SetInt("flyHighScore", score);
	}
}