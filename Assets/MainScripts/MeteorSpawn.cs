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
	public int SuddenDeathTriggerTimePerPlayer;
	float SuddenDeathTriggerTime = 0;
	int lastAmountOfPlayers = 0;
	int amountOfPlayers = 0;
	void Start () {

		randNum = Random.Range (1, maxRandom);
		timerCounter = timeSetter;
		ObjectToSpawn = Meteor;
		SuddenDeathTriggerTime = SuddenDeathTriggerTimePerPlayer * GlobalControlScript.GlobalControl.NumberOfPlayers;
	}
	

	void Update () 
	{
		lastAmountOfPlayers = amountOfPlayers;
		amountOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;

		if (amountOfPlayers < lastAmountOfPlayers)
		{
			SuddenDeathTriggerTime = ElapsedGameTime +(SuddenDeathTriggerTimePerPlayer * amountOfPlayers);
		}

		timerCounter -= Time.deltaTime;
		if(ElapsedGameTime < SuddenDeathTriggerTime)
		{
		ElapsedGameTime += Time.deltaTime;
		}
		else if(ElapsedGameTime >= SuddenDeathTriggerTime)
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
			GameObject temp = (GameObject) Instantiate(ObjectToSpawn, spawnerPosition, Quaternion.identity);
			float rand = Random.Range(50,150);
			temp.GetComponent<Rigidbody2D>().AddTorque(rand);
			//resets number to a new random number
			randNum = Random.Range (1, maxRandom);
		}
		else
		{
			randNum = Random.Range (1, maxRandom);
		}

	}
}
