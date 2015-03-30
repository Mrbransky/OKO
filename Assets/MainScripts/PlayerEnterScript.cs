﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEnterScript : MonoBehaviour {
	public GameObject GlobalControl;
	List<KeyCode> keysUsed = new List<KeyCode>();
	// Use this for initialization
	void Start () {
		if (GameObject.Find("GlobalControl") != null)
		{
			GlobalControl = GameObject.Find("GlobalControl");
		}
		else{
			GameObject globalcontrol = new GameObject();
			globalcontrol.AddComponent("GlobalControlScript");
			GlobalControl = globalcontrol;
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.inputString >= 'a' && Input.inputString <= 'z' && Input.inputString >= '0' && Input.inputString <= '9')
//		{
//
//			print(Input.inputString);
//		}
		KeyCode keyCode = FetchKey();
		bool add = true;
		if (keyCode != KeyCode.None)
		{
			foreach (KeyCode key in keysUsed)
			{
				if (key == keyCode)
				{
					add = false;
					break;
				}
			}
			if (add)
			{
				keysUsed.Add(keyCode);
				GlobalControlScript.GlobalControl.KeysForPlayers.Add(keyCode);
				GlobalControl.GetComponent<GlobalControlScript>().NumberOfPlayers = keysUsed.Count;
			}
		}
		print(keyCode);
		
	}
	KeyCode FetchKey()
	{
		int e = System.Enum.GetNames(typeof(KeyCode)).Length;
		for(int i = 0; i < e; i++)
		{
			if(Input.GetKey((KeyCode)i))
			{
				return (KeyCode)i;
			}
		}
		return KeyCode.None;
	}
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,200),"Press Key To Join!");
		int x = 10;
		int y = 100;
		int i = 1;
		foreach (KeyCode key in keysUsed)
		{
			GUI.Label(new Rect(x,y,100,50),"Player " + i + ":\n" + key.ToString());
			if (x<Screen.width - 110)
			{
				x+=100;
			}
			else{
				x=10;
				y+=50;
			}
			i++;
		}
	}
}
