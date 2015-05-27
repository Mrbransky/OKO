using UnityEngine;
using System.Collections;

public class PlayerEnterKeyScript : MonoBehaviour {
	public KeyCode myKey;
	float buttonpressedTime = 0;
	float timeToDelete = 2;
	public GameObject playerEnterController;
	bool JustSpawned = false;
	bool timeSet = false;
	// Use this for initialization
	void Start () {
		playerEnterController = GameObject.FindGameObjectWithTag("MainCamera");
		JustSpawned = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(myKey))
		{
			if (JustSpawned)
			{
				JustSpawned = false;
			}
		}
		if (Input.GetKey(myKey))
		{
			if (!timeSet)
			{
				print(Time.time);
				buttonpressedTime = Time.time;
				timeSet = true;
			}
			if (JustSpawned)
			{
			}
			else if (Time.time >= buttonpressedTime + timeToDelete)
			{

				playerEnterController.GetComponent<PlayerEnterScript>().RemoveKey(myKey);
				Destroy(gameObject);
			}
		}
		if (Input.GetKeyUp(myKey))
		{
			timeSet = false;
		}

	}
}
