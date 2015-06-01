using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerEnterScript : MonoBehaviour {
	public GameObject GlobalControl;
	public GameObject PlayerEnterKeyObject;
	public int MaxPlayers;
	List<KeyCode> keysUsed = new List<KeyCode>();
	public Color[] colors = new Color[16];
	public Sprite[] boxes = new Sprite[16];
	public List<GameObject> PlayerEnterKeyObjects = new List<GameObject>();
	public GameObject PressAnyKeyText;
	public GameObject RemoveKeyText;
	public GameObject InstructionsText;
	public GameObject StartButton;
	public GameObject SM;
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
			globalcontrol.AddComponent<GlobalControlScript>();
			GameObject sm = (GameObject)Instantiate(SM);
			sm.transform.SetParent(globalcontrol.transform);
			globalcontrol.name = "GlobalControl";
			GlobalControl = globalcontrol;
		}
		GlobalControlScript.GlobalControl.KeysForPlayers.Clear();
		GlobalControlScript.GlobalControl.NumberOfPlayers = 0;
		PressAnyKeyText.SetActive(false);
		RemoveKeyText.SetActive(false);
		StartButton.SetActive(false);
		StartButton.transform.position = new Vector3((3* Screen.width)/4,(4.5f * Screen.height)/5);

		colors[0] = new Color32((byte)204,(byte)51,(byte)63,(byte)250);
		colors[1] = new Color32((byte)0,(byte)160,(byte)176,(byte)250);
		colors[2] = new Color32((byte)237,(byte)201,(byte)81,(byte)250);
		colors[3] = new Color32((byte)81,(byte)149,(byte)72,(byte)250);
		colors[4] = new Color32((byte)219,(byte)139,(byte)178,(byte)250);
		colors[5] = new Color32((byte)140,(byte)138,(byte)152,(byte)250);
		colors[6] = new Color32((byte)0,(byte)105,(byte)255,(byte)250);
		colors[7] = new Color32((byte)255,(byte)59,(byte)47,(byte)250);
		colors[8] = new Color32((byte)199,(byte)255,(byte)0,(byte)250);
		colors[9] = new Color32((byte)30,(byte)202,(byte)89,(byte)250);
		colors[10] = new Color32((byte)126,(byte)73,(byte)231,(byte)250);
		colors[11] = new Color32((byte)140,(byte)130,(byte)129,(byte)250);
		colors[12] = new Color32((byte)255,(byte)252,(byte)44,(byte)250);
		colors[13] = new Color32((byte)218,(byte)222,(byte)224,(byte)250);
		colors[14] = new Color32((byte)98,(byte)140,(byte)94,(byte)250);
		colors[15] = new Color32((byte)245,(byte)105,(byte)145,(byte)250);
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

					tempPlayerEnterKeyObject.transform.SetParent(transform);
					PlayerEnterKeyObjects.Add(tempPlayerEnterKeyObject);

					GlobalControlScript.GlobalControl.KeysForPlayers.Add(keyCode);
					GlobalControl.GetComponent<GlobalControlScript>().NumberOfPlayers = keysUsed.Count;
				}
			}
			print(keyCode);
		}
		int x = Screen.width/5;
		int y = (4*Screen.height/5)-Screen.height/20;
		int r = 1;
		int c = 1;
		int i = 0;
		foreach (GameObject obj in PlayerEnterKeyObjects)
		{

			Vector3 rot = new Vector3(0,-15 + (6*r),0);
			obj.GetComponent<Image>().transform.rotation = Quaternion.Euler(rot);
			obj.GetComponentInChildren<Text>().transform.rotation = Quaternion.Euler(rot);
			foreach (Image imageObj in obj.GetComponentsInChildren<Image>())
			{
				if (imageObj.transform.tag != "PlayerIdentifier")
				{
					imageObj.color = colors[i];
				}
			}

			obj.GetComponent<Image>().sprite = boxes[i];
			obj.transform.position = new Vector3(x,y);
			if (r < 4)
			{

				x+=Screen.width/5;
				r++;
			}
			else{
				x=Screen.width/5;
				y-=Screen.height/5;
				r = 1;
				c++;
			}
			i++;
		}
		if (keysUsed.Count < MaxPlayers)
		{
			PressAnyKeyText.SetActive(true);
			PressAnyKeyText.transform.position = new Vector3(Screen.width/2,(4.75f * Screen.height)/5);
		}
		else
		{
			PressAnyKeyText.SetActive(false);
		}
		if (keysUsed.Count >= 1)
		{
			RemoveKeyText.SetActive(true);
			RemoveKeyText.transform.position = new Vector3(Screen.width/2,(4.5f * Screen.height)/5);
		}
		else{
			RemoveKeyText.SetActive(false);
		}
		if(keysUsed.Count >= 2)
		{
			StartButton.SetActive(true);
		}
		else{
			StartButton.SetActive(false);
		}
		InstructionsText.transform.position = new Vector3(Screen.width/2,(4.25f * Screen.height)/5);
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
	public void StartGame()
	{
		Application.LoadLevel("BattleScene");
	}
//	void OnGUI()
//	{
//		if (keysUsed.Count < MaxPlayers)
//		{
//			GUI.Label(new Rect(10,10,400,200),"Press Key To Add Player With That Key!");
//		}
//		if (keysUsed.Count >= 1)
//		{
//			GUI.Label(new Rect(10,40,400,200),"Hold Key Down to Remove Player!");
//		}
//		GUI.Label(new Rect(10,70,400,200),"Tap Key to Boost! Double Tap to Flip Direction!");
//		int x = 10;
//		int y = 100;
//		int i = 1;
//		foreach (KeyCode key in keysUsed)
//		{
//			GUI.Label(new Rect(x,y,100,50),"Player " + i + ":\n" + key.ToString());
//			if (x<Screen.width - 110)
//			{
//				x+=100;
//			}
//			else{
//				x=10;
//				y+=50;
//			}
//			i++;
//		}
//		if(keysUsed.Count >= 2)
//		{
//			if(GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100,50),"Play Game"))
//			{
//				Application.LoadLevel("BattleScene");
//			}
//		}
//	}
}
