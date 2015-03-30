using UnityEngine;
using System.Collections;
using GamepadInput;

public class Control : MonoBehaviour {

	GameObject GlobalControl;
	public GameObject PlayerPrefab;
	public GameObject[] targets;
	public GameObject[] suns;
	public GameObject fader;
	public bool gameEnd = false;
	public GUISkin mainSkin;
	public GUISkin countSkin;
	public int rounds;
	public float endTimeSet;
	public bool startGame = false;
	public float startTimer = 3;
	
	float timer;
	
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

		Vector3 spawnPos = Vector3.zero;
		Vector3 spawnRot = transform.rotation.eulerAngles;
		foreach (KeyCode key in GlobalControlScript.GlobalControl.KeysForPlayers)
		{
			GameObject tempPlayer = (GameObject)Instantiate(PlayerPrefab,spawnPos,Quaternion.Euler( spawnRot));
			tempPlayer.GetComponent<PlayerScript>().MyKey = key;
		}

		targets = GameObject.FindGameObjectsWithTag ("Player");
		fader.GetComponent<Fade>().sceneStarting = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		GamepadState state = GamePad.GetState(GamePad.Index.Any);
		if (startGame == true) {
			if (targets.Length == 1) {
				
				gameEnd = true;
				
			}
			if (gameEnd == true) {
				
				timer -= Time.deltaTime;
				
			}
			if (timer <= 0) {
				
				timer = endTimeSet;
				
				rounds--;
			}
			//if (rounds <= 0) {
			
			//Application.LoadLevel(3);
			
			//}
			if (gameEnd == true) {
				
				if (state.B || Input.GetKey(KeyCode.B)) {
					Application.LoadLevel (0);
					Time.timeScale = 1;
				}
				if (state.Y || Input.GetKey(KeyCode.Y)) {
					Time.timeScale = 1;
					Application.LoadLevel (1);
				}
				
			}
		} 
		
		else if (fader.GetComponent<Fade>().finished == true) {startTimer -= Time.deltaTime;}
		
		if (startTimer <= 0) {startGame = true;}
	}
	
	
	
	
	void LateUpdate()
	{
		targets = GameObject.FindGameObjectsWithTag ("Player"); 
		
		if (!GameObject.FindWithTag ("Player")){
			
			return;
		}	
	}
	
	void OnGUI()
	{
		
		GUI.skin = countSkin;
		if (startGame == false) {
			
			
			GUI.Label(new Rect(Screen.width/2,Screen.height/2,200,200),((int)startTimer).ToString());
			GUI.skin = mainSkin;
			GUI.Label(new Rect(Screen.width/2 - 100,Screen.height/2 - 50,500,200),"Knock the other players out!");
		}
		
		GUI.skin = mainSkin;
		if (gameEnd == true) {
			
			GUI.Label(new Rect(Screen.width/2,0,200,200),targets[0].name + " wins!");
			GUI.Label(new Rect(Screen.width/2,50,500,200),"Press B to go back");
			GUI.Label(new Rect(Screen.width/2,100,500,200),"Press Y to restart");
		}
		else
		{
			//GUI.Label (new Rect (Screen.width / 2 - 300, 50, 500, 200),"Player 1 Life: " + targets [1].GetComponent<PlayerScript> ().DamageAmount);
			//GUI.Label (new Rect (Screen.width / 2 + 300, 50, 500, 200),"Player 2 Life: " + targets [0].GetComponent<PlayerScript> ().DamageAmount);
		}
	}
}
