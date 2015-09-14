using UnityEngine;
using System.Collections;

public class platformspawner : MonoBehaviour {

	//array of blocks you can add to. make sure every block has a startPoint and endPoint object
	public GameObject[] blocks;
	//this is the variable that keeps track of the last position we spawned an object
	public Vector3 lastPosition;

	private int randomChoice;
	//we find the camera to reference its position so this script knows when to spawn the next object
	private GameObject cam;
	//we check to see if we can spawn or not based on camera position and lastposition
	private bool canSpawn = true;
	
	void Start () {
		//we find the camera in the scene and pair it to the variable cam
		cam = GameObject.Find("Main Camera");
	}
	
	void Update () {
		
		//if the camera is farther than the number last position minus 16, we allow a chunk to spawn.
		if(cam.transform.position.x >= lastPosition.x - 16 && canSpawn == true){
			//now we turn off can spawn so that this doesn't happen more than once and we'll turn it back on when we're ready to do another spawn.
			canSpawn = false;
			//we choose the random number that will determine what chunk will be spawned.
			randomChoice = Random.Range(0,blocks.Length);
			//now we call the spawnobject function and send the random choice variable with it
			spawnObject(randomChoice);
		}
		
		//end of function update
	}
	
	//now we spawn an object based on the number randomChoice thats received. rand is used in place of randomChoice
	void spawnObject (int rand){
		//get a temp position to make sure the block spawns out of view
		Vector3 tempPos = new Vector3(lastPosition.x+10,-8,0);
		//spawn the lock
		GameObject spawnedBlock = Instantiate(blocks[rand], tempPos, Quaternion.Euler(0,0,0)) as GameObject;
		//get the startPoint and endPoint objects from the block to determine the final position
		Transform[] getPoints = spawnedBlock.GetComponentsInChildren<Transform>();
		GameObject startPoint = new GameObject();
		GameObject endPoint = new GameObject();
		for(int i = 0;i < getPoints.Length;i++){
			if(getPoints[i].name == "startPoint"){
				startPoint = getPoints[i].gameObject;
			}
			if(getPoints[i].name == "endPoint"){
				endPoint = getPoints[i].gameObject;
			}
		}
		//get the last end position and create the new position based on the blocks start local position
		Vector3 pointPos = lastPosition-startPoint.transform.localPosition;
		//move the block in its final place
		spawnedBlock.transform.position = new Vector3(pointPos.x,pointPos.y,0);
		//save the block's end point position so we can use it for the next block
		lastPosition = endPoint.transform.position;
		//now we allow the script to get ready to spawn another platform, right after we spawn one.
		canSpawn = true;
	}
}
