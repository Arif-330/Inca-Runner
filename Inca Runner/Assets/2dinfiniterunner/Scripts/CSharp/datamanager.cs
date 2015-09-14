using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class datamanager : MonoBehaviour {
	
	public static datamanager instance = null;
	private int highestStage;
	private int highestLevel1;
	private int highestLevel2;
	private int highestLevel3;
	private int highestLevel4;
	public int currentStage;
	public int currentLevel;
	private bool stageOneUnlocked;
	private bool stageTwoUnlocked;
	private bool stageThreeUnlocked;
	private bool stageFourUnlocked;
		
		// Use this for initialization
		void Awake () {
			if (instance == null) {
				currentStage = 1;
				currentLevel = 1;
				DontDestroyOnLoad (gameObject);
				instance = this;
			}else if(instance != this){
				Destroy(instance);
			}
			highestStage = 1;
			highestLevel1 = 1;
			highestLevel2 = 1;
			highestLevel3 = 1;
			highestLevel4 = 1;
			Load ();
		}

	public int getCurrentLevel(){
		return currentLevel;
	}

	public int getCurrentStage(){
		return currentStage;
	}

	public void setCurrentLevelDecrement(){
		currentLevel = --currentLevel;
	}

	
	public void setCurrentLevelIncrement(){
		currentLevel = ++currentLevel;
	}
	public void setCurrentLevelReset(){
		currentLevel = 1;
	}
	
	public void setCurrentStageDecrement(){
		currentLevel = 1;
		currentStage = --currentStage;
	}

	public void setCurrentStageIncrement(){
		currentLevel = 1;
		currentStage = ++currentStage;
	}

	public void unlockStage(int stage){
		highestStage = stage; // Send in the highest stage to be unlocked
		Save ();
	}

	public int checkHighestStage(){
		return highestStage;
	}

	public int checkHighestLevel(int stage){
		switch(stage){
		case 1:
			return highestLevel1;
			break;
		case 2:
			return highestLevel2;
			break;
		case 3:
			return highestLevel3;
			break;
		case 4:
			return highestLevel4;
			break;
		}
		return 1;
	}

	public int checkHighestLevel1(){
		return highestLevel1;
	}
	public int checkHighestLevel2(){
		return highestLevel2;
	}
	public int checkHighestLevel3(){
		return highestLevel3;
	}
	public int checkHighestLevel4(){
		return highestLevel4;
	}

	public void unlockLevel(int stage, int level){
		switch (stage) {
		case 1:
			if(highestLevel1 < level)
				highestLevel1 = level;
			break;
		case 2:
			if(highestLevel2 < level)
				highestLevel2 = level;
			break;
		case 3:
			if(highestLevel3 < level)
				highestLevel3 = level;
			break;
		case 4:
			if(highestLevel4 < level)
				highestLevel4 = level;
			break;
		default:
			break;
		}
		Save (); 
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/levelInfo.dat");
		LevelData data = new LevelData ();
		data.highestStage = this.highestStage;
		data.highestLevel1 = this.highestLevel1;
		data.highestLevel2 = this.highestLevel2;
		data.highestLevel3 = this.highestLevel3;
		data.highestLevel4 = this.highestLevel4;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/levelInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/levelInfo.dat", FileMode.Open);
			LevelData data = (LevelData) bf.Deserialize(file);
			file.Close ();
			this.highestStage = data.highestStage;
			this.highestLevel1 = data.highestLevel1;
			this.highestLevel2 = data.highestLevel2;
			this.highestLevel3 = data.highestLevel3;
			this.highestLevel4 = data.highestLevel4;
		}
	}

}

[Serializable]
class LevelData{
	public int highestStage;
	public int highestLevel1;
	public int highestLevel2;
	public int highestLevel3;
	public int highestLevel4;


}
