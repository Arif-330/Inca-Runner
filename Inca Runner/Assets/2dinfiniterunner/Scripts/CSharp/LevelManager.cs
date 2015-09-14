using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	
	public Button levelSelector;
	public Button stageSelector;

	// Use this for initialization
	void Awake ()
	{
			
	}
		
	// Update is called once per frame
	void Start ()
	{
		if (levelSelector != null)
			levelSelector.GetComponentInChildren<Text> ().text = datamanager.instance.getCurrentLevel () + "";
		if (stageSelector != null)
			stageSelector.GetComponentInChildren<Text> ().text = datamanager.instance.getCurrentStage () + "";
	}

	public void changeLevel (string rightOrLeft)
	{
		if (rightOrLeft == "left") {
			if (datamanager.instance.getCurrentLevel () != 1)
				datamanager.instance.setCurrentLevelDecrement ();
		} else {
			if (datamanager.instance.getCurrentLevel () != 20)
				datamanager.instance.setCurrentLevelIncrement ();
		}

		levelSelector.GetComponentInChildren<Text> ().text = datamanager.instance.getCurrentLevel () + "";
		Button b = levelSelector.GetComponentInChildren<Button> ();
		ColorBlock colorTint = b.colors;
		if (datamanager.instance.checkHighestLevel (datamanager.instance.getCurrentStage ()) >= datamanager.instance.getCurrentLevel ())
		{
			colorTint.normalColor = Color.white;
			colorTint.highlightedColor = Color.blue;
			colorTint.pressedColor = new Color32( 200,200,200, 255);
		}
		else {
			colorTint.normalColor = Color.gray;
			colorTint.highlightedColor = Color.gray;
			colorTint.pressedColor = Color.gray;
		}
		b.colors = colorTint;
	}

	public void changeStage (string rightOrLeft)
	{
		//levelSelector.GetComponentsInChildren<>();
		if (rightOrLeft == "left") {
			if (datamanager.instance.getCurrentStage () != 1)
				datamanager.instance.setCurrentStageDecrement();
		} else {
			//if (datamanager.instance.getCurrentStage () != 4)
				//datamanager.instance.setCurrentStageIncrement();
		}
		stageSelector.GetComponentInChildren<Text> ().text = datamanager.instance.getCurrentStage () + "";
		Button b = stageSelector.GetComponentInChildren<Button> ();
		ColorBlock colorTint = b.colors;
		if (datamanager.instance.checkHighestStage () >= datamanager.instance.getCurrentStage ()) {
			colorTint.normalColor = Color.white;
			colorTint.highlightedColor = Color.blue;
			colorTint.pressedColor  = new Color32( 200,200,200, 255);
		}
		else {
			colorTint.normalColor = Color.gray;
			colorTint.highlightedColor = Color.gray;
			colorTint.pressedColor = Color.gray;
		}
		b.colors = colorTint;
	}

	public void stageClicked ()
	{
		if (datamanager.instance.checkHighestStage () >= datamanager.instance.getCurrentStage ()) {
			Application.LoadLevel ("LevelScreen");
		}
		else {

		}
	}
		
	public void backClickedStage ()
	{

		Application.LoadLevel ("PeruMenu");
	}

	public void backClicked ()
	{
		Application.LoadLevel ("StageScreen");
	}

	public void levelSelect ()
	{

		switch (datamanager.instance.getCurrentStage()) {
		case 1:
			switch (datamanager.instance.getCurrentLevel () ) {
			case 1:
				if(datamanager.instance.checkHighestLevel1() >= 1)
					Application.LoadLevel ("LevelOne");
				break;
			case 2:
				if(datamanager.instance.checkHighestLevel1() >= 2)
					Application.LoadLevel ("LevelTwo");
				break;
			case 3:
				if(datamanager.instance.checkHighestLevel1() >= 3)
					Application.LoadLevel ("LevelThree");
				break;
			case 4:
				if(datamanager.instance.checkHighestLevel1() >= 4)
					Application.LoadLevel ("LevelFour");
				break;
			case 5:
				if(datamanager.instance.checkHighestLevel1() >= 5)
					Application.LoadLevel ("LevelFive");
				break;
			case 6:
				if(datamanager.instance.checkHighestLevel1() >= 6)
				Application.LoadLevel ("LevelSix");
				break;
			case 7:
				if(datamanager.instance.checkHighestLevel1() >= 7)
				Application.LoadLevel ("LevelSeven");
				break;
			case 8:
				if(datamanager.instance.checkHighestLevel1() >= 8)
				Application.LoadLevel ("LevelEight");
				break;
			case 9:
				if(datamanager.instance.checkHighestLevel1() >= 9)
				Application.LoadLevel ("LevelNine");
				break;
			case 10:
				if(datamanager.instance.checkHighestLevel1() >= 10)
				Application.LoadLevel ("LevelTen");
				break;
			case 11:
				if(datamanager.instance.checkHighestLevel1() >= 11)
					Application.LoadLevel ("LevelEleven");
				break;
			case 12:
				if(datamanager.instance.checkHighestLevel1() >= 12)
					Application.LoadLevel ("LevelTwelve");
				break;
			case 13:
				if(datamanager.instance.checkHighestLevel1() >= 13)
					Application.LoadLevel ("LevelThirteen");
				break;
			case 14:
				if(datamanager.instance.checkHighestLevel1() >= 14)
					Application.LoadLevel ("LevelFourteen");
				break;
			case 15:
				if(datamanager.instance.checkHighestLevel1() >= 15)
					Application.LoadLevel ("LevelFifteen");
				break;
			case 16:
				if(datamanager.instance.checkHighestLevel1() >= 16)
					Application.LoadLevel ("LevelSixeteen");
				break;
			case 17:
				if(datamanager.instance.checkHighestLevel1() >= 17)
					Application.LoadLevel ("LevelSeventeen");
				break;
			case 18:
				if(datamanager.instance.checkHighestLevel1() >= 18)
					Application.LoadLevel ("LevelEighteen");
				break;
			case 19:
				if(datamanager.instance.checkHighestLevel1() >= 19)
					Application.LoadLevel ("LevelNineteen");
				break;
			case 20:
				if(datamanager.instance.checkHighestLevel1() >= 20)
					Application.LoadLevel ("LevelTwenty");
				break;

			}
			break;
		case 2:
			switch (datamanager.instance.getCurrentLevel () ) {
			case 1:
				if(datamanager.instance.checkHighestLevel2() >= 1)
				Application.LoadLevel ("LevelOne");
				break;
			case 2:
				if(datamanager.instance.checkHighestLevel2() >= 2)
				Application.LoadLevel ("LevelOne");
				break;
			case 3:
				if(datamanager.instance.checkHighestLevel2() >= 3)
				Application.LoadLevel ("LevelOne");
				break;
			case 4:
				if(datamanager.instance.checkHighestLevel2() >= 4)
				Application.LoadLevel ("LevelOne");
				break;
			case 5:
				if(datamanager.instance.checkHighestLevel2() >= 5)
				Application.LoadLevel ("LevelOne");
				break;
			case 6:
				if(datamanager.instance.checkHighestLevel2() >= 6)
				Application.LoadLevel ("LevelOne");
				break;
			case 7:
				if(datamanager.instance.checkHighestLevel2() >= 7)
				Application.LoadLevel ("LevelOne");
				break;
			case 8:
				if(datamanager.instance.checkHighestLevel2() >= 8)
				Application.LoadLevel ("LevelOne");
				break;
			case 9:
				if(datamanager.instance.checkHighestLevel2() >= 9)
				Application.LoadLevel ("LevelOne");
				break;
			case 10:
				if(datamanager.instance.checkHighestLevel2() >= 10)
				Application.LoadLevel ("LevelOne");
				break;
			}
			break;
		case 3:
			switch (datamanager.instance.getCurrentLevel () ) {
			case 1:
				if(datamanager.instance.checkHighestLevel3() >= 1)
				Application.LoadLevel ("LevelOne");
				break;
			case 2:
				if(datamanager.instance.checkHighestLevel3() >= 2)
				Application.LoadLevel ("LevelOne");
				break;
			case 3:
				if(datamanager.instance.checkHighestLevel3() >= 3)
				Application.LoadLevel ("LevelOne");
				break;
			case 4:
				if(datamanager.instance.checkHighestLevel3() >= 4)
				Application.LoadLevel ("LevelOne");
				break;
			case 5:
				if(datamanager.instance.checkHighestLevel3() >= 5)
				Application.LoadLevel ("LevelOne");
				break;
			case 6:
				if(datamanager.instance.checkHighestLevel3() >= 6)
				Application.LoadLevel ("LevelOne");
				break;
			case 7:
				if(datamanager.instance.checkHighestLevel3() >= 7)
				Application.LoadLevel ("LevelOne");
				break;
			case 8:
				if(datamanager.instance.checkHighestLevel3() >= 8)
				Application.LoadLevel ("LevelOne");
				break;
			case 9:
				if(datamanager.instance.checkHighestLevel3() >= 9)
				Application.LoadLevel ("LevelOne");
				break;
			case 10:
				if(datamanager.instance.checkHighestLevel3() >= 10)
				Application.LoadLevel ("LevelOne");
				break;
			}
			break;
		case 4:
			switch (datamanager.instance.getCurrentLevel () ) {
			case 1:
				if(datamanager.instance.checkHighestLevel4() >= 1)
				Application.LoadLevel ("LevelOne");
				break;
			case 2:
				if(datamanager.instance.checkHighestLevel4() >= 2)
				Application.LoadLevel ("LevelOne");
				break;
			case 3:
				if(datamanager.instance.checkHighestLevel4() >= 3)
				Application.LoadLevel ("LevelOne");
				break;
			case 4:
				if(datamanager.instance.checkHighestLevel4() >= 4)
				Application.LoadLevel ("LevelOne");
				break;
			case 5:
				if(datamanager.instance.checkHighestLevel4() >= 5)
				Application.LoadLevel ("LevelOne");
				break;
			case 6:
				if(datamanager.instance.checkHighestLevel4() >= 6)
				Application.LoadLevel ("LevelOne");
				break;
			case 7:
				if(datamanager.instance.checkHighestLevel4() >= 7)
				Application.LoadLevel ("LevelOne");
				break;
			case 8:
				if(datamanager.instance.checkHighestLevel4() >= 8)
				Application.LoadLevel ("LevelOne");
				break;
			case 9:
				if(datamanager.instance.checkHighestLevel4() >= 9)
				Application.LoadLevel ("LevelOne");
				break;
			case 10:
				if(datamanager.instance.checkHighestLevel4() >= 10)
				Application.LoadLevel ("LevelOne");
				break;
			}
			break;
		default:
			Application.LoadLevel ("PeruMenu");
			break;
		}
	}
}
