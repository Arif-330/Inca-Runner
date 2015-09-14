#pragma strict

//this is a menu that just holds functions that are called by the mouseButton script attached to retry and menu guiText (child objects)

function doRetry () {
	var getLvlName = Application.loadedLevelName;
	Application.LoadLevel(getLvlName);
}

function doMenu () {
	Application.LoadLevel("menu");
}


