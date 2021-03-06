﻿using UnityEngine;
using UnityEngine.UI;
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
		GetComponentInChildren<Text>().text = myKey.ToString();
		transform.localScale = new Vector3((Screen.currentResolution.width/1600),(Screen.currentResolution.width/1200),1);
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
				playerEnterController.GetComponent<PlayerEnterScript>().PlayerEnterKeyObjects.Remove(gameObject);
				Destroy(gameObject);
			}
		}
		if (Input.GetKeyUp(myKey))
		{
			timeSet = false;
		}

	}
}
