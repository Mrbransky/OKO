using UnityEngine;
using System.Collections;

public class MeteorSpawn : MonoBehaviour {



	int randNum;
	public float timer = 7;
	private float timerCounter;
	public GameObject meteor;
	public GameObject MeteorSpawner;
	public GameObject control;
	// Use this for initialization
	void Start () {

		randNum = Random.Range (1, 5);
		timerCounter = timer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timerCounter -= Time.deltaTime;

		if(timerCounter <= 0 && control.GetComponent<Control>().startGame)
		{
			SpawnMeteor();
			timerCounter = timer;
		}
	}
	void SpawnMeteor()
	{

		if(randNum == 3)
		{
			Vector2 spawnerPosition = (MeteorSpawner.transform.position);
			Instantiate(meteor, spawnerPosition, Quaternion.identity);
			randNum = Random.Range (1, 5);
		}
		else
		{
			randNum = Random.Range (1, 5);
		}

	}
}
