using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEnterScript : MonoBehaviour {
	public GameObject GlobalControl;
	public GameObject PlayerEnterKeyObject;
	public int MaxPlayers;
	List<KeyCode> keysUsed = new List<KeyCode>();
	// Use this for initialization
	void Start () {
		//MaxPlayers--;
		Time.timeScale = 1;
		if (GameObject.Find("GlobalControl") != null)
		{
			GlobalControl = GameObject.Find("GlobalControl");
		}
		else{
			GameObject globalcontrol = new GameObject();
			globalcontrol.AddComponent("GlobalControlScript");
			globalcontrol.AddComponent("soundManager");
			globalcontrol.name = "GlobalControl";
			GlobalControl = globalcontrol;
		}
		GlobalControlScript.GlobalControl.KeysForPlayers.Clear();
		GlobalControlScript.GlobalControl.NumberOfPlayers = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.inputString >= 'a' && Input.inputString <= 'z' && Input.inputString >= '0' && Input.inputString <= '9')
//		{
//
//			print(Input.inputString);
//		}
		if(keysUsed.Count < MaxPlayers)
		{
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

					GameObject tempPlayerEnterKeyObject = (GameObject)Instantiate(PlayerEnterKeyObject);
					tempPlayerEnterKeyObject.GetComponent<PlayerEnterKeyScript>().myKey = keyCode;

					GlobalControlScript.GlobalControl.KeysForPlayers.Add(keyCode);
					GlobalControl.GetComponent<GlobalControlScript>().NumberOfPlayers = keysUsed.Count;
				}
			}
			print(keyCode);
		}
	}
	public void RemoveKey(KeyCode key)
	{
		keysUsed.Remove(key);
		GlobalControlScript.GlobalControl.KeysForPlayers.Remove(key);
		GlobalControl.GetComponent<GlobalControlScript>().NumberOfPlayers = keysUsed.Count;
	}
	KeyCode FetchKey()
	{
		int e = System.Enum.GetNames(typeof(KeyCode)).Length;
		for(int i = 0; i < e; i++)
		{
			if(Input.GetKeyDown((KeyCode)i))
			{
				return (KeyCode)i;
			}
		}
		return KeyCode.None;
	}
	void OnGUI()
	{
		if (keysUsed.Count < MaxPlayers)
		{
			GUI.Label(new Rect(10,10,400,200),"Press Key To Add Player With That Key!");
		}
		if (keysUsed.Count >= 1)
		{
			GUI.Label(new Rect(10,40,400,200),"Hold Key Down to Remove Player!");
		}
		GUI.Label(new Rect(10,70,400,200),"Tap Key to Boost! Double Tap to Flip Direction!");
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
		if(keysUsed.Count >= 2)
		{
			if(GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100,50),"Play Game"))
			{
				Application.LoadLevel("BattleScene");
			}
		}
	}
}
