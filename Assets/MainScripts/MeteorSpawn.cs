using UnityEngine;
using System.Collections;

public class MeteorSpawn : MonoBehaviour {

	int randNum;
	public float timeSetter = 7;

	private float timerCounter;
	private int maxRandom = 5;

	public GameObject Meteor;
	public GameObject ExplosiveMine;
	GameObject ObjectToSpawn;
	public GameObject control;

	float ElapsedGameTime;
	public int SuddenDeathTriggerTime;


	void Start () {

		randNum = Random.Range (1, maxRandom);
		timerCounter = timeSetter;
		ObjectToSpawn = Meteor;
	}
	

	void Update () 
	{
		timerCounter -= Time.deltaTime;
		if(ElapsedGameTime < (float)SuddenDeathTriggerTime)
		{
		ElapsedGameTime += Time.deltaTime;
		}
		else if(ElapsedGameTime >= (float)SuddenDeathTriggerTime)
		{
			//Sudden Death
			//Debug.Log ("Sudden Death Started");
			ObjectToSpawn = ExplosiveMine;
			timeSetter = 2;
			maxRandom = 3;
		}

		//Attempts to spawn meteor every seven seconds
		if(timerCounter <= 0 && control.GetComponent<Control>().startGame)
		{
			SpawnMeteor();
			timerCounter = timeSetter;
		}

	}
	void SpawnMeteor()
	{

		if(randNum == (int)(maxRandom/2))
		{
			Vector2 spawnerPosition = (this.transform.position);
			Instantiate(ObjectToSpawn, spawnerPosition, Quaternion.identity);
			//resets number to a new random number
			randNum = Random.Range (1, maxRandom);
		}
		else
		{
			randNum = Random.Range (1, maxRandom);
		}

	}
}
