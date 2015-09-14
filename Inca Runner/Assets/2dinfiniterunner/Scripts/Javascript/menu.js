#pragma strict

//This script receives messages from the GUITexts for Play and Quit that are children of this object

function startRunner () {
	Application.LoadLevel("runner-game");
}

function startFlyer () {
	Application.LoadLevel("flyer-game");
}

function quitGame () {
	Application.Quit();
}