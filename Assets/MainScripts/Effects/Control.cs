using UnityEngine;
using System.Collections;
using GamepadInput;

public class Control : MonoBehaviour {

	GameObject GlobalControl;
	public GameObject OoB;
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
			globalcontrol.name = "GlobalControl";
			GlobalControl = globalcontrol;
		}
		//PlayerSpawning
		GetComponent<PlanetSpawner>().SpawnPlanets();
	
		GameObject[] suns = GameObject.FindGameObjectsWithTag("BlackHole");
		int PlayersPerSun = (int)Mathf.Floor(GlobalControlScript.GlobalControl.NumberOfPlayers/suns.Length);

		float DistAwayFromSun = 40; 
		float angle = Mathf.PI * 2 / PlayersPerSun; 

		Vector3 spawnPos = Vector3.zero;
		Quaternion spawnRot = transform.rotation;
		int sunToSpawnTo = 0;
		int i = 1;
		int playerAroundSun = 1;
		foreach (KeyCode key in GlobalControlScript.GlobalControl.KeysForPlayers)
		{
			DistAwayFromSun = 34 + (PlayersPerSun*3); 

			angle += Mathf.PI * 2 / PlayersPerSun; 
			spawnPos = suns[sunToSpawnTo].transform.position + new Vector3 (Mathf.Cos(angle), Mathf.Sin(angle)) * DistAwayFromSun;

//			float angleToSun = Vector3.Angle((suns[sunToSpawnTo].transform.position-spawnPos).normalized,suns[sunToSpawnTo].transform.position+Vector3.up);
////
////			if(spawnPos.y > suns[sunToSpawnTo].transform.position.y)
////			{
////				if (angleToSun < 90 || angleToSun > 270)
////				{
////				
////					angleToSun += 180;
////				}
////			}
////			else
////			{
////				if (angleToSun > 90 && angleToSun < 270)
////				{
////					angleToSun += 180;
////				}
////			}
////
//			if (spawnPos.x > suns[sunToSpawnTo].transform.position.x)
//			{
//				angleToSun = -angleToSun + 90;
//
//			}
//			else{
//				angleToSun = angleToSun + 90;
//			}
//			//Quaternion rot = transform.rotation;
//			print (angleToSun);
			spawnRot = Quaternion.AngleAxis((angle*Mathf.Rad2Deg), Vector3.forward);
			//spawnRot = Quaternion.Euler(0, 0, spawnRot.z);


			GameObject tempPlayer = (GameObject)Instantiate(PlayerPrefab,spawnPos,spawnRot);
			tempPlayer.GetComponent<PlayerScript>().MyKey = key;
			tempPlayer.name = "Player " + i;

			if (playerAroundSun % PlayersPerSun == 0)
			{
				if (sunToSpawnTo+1 < suns.Length)
				{
					sunToSpawnTo++;
					PlayersPerSun = (int)Mathf.Floor((GlobalControlScript.GlobalControl.NumberOfPlayers-i)/(suns.Length-sunToSpawnTo));
					angle = Mathf.PI * 2 / PlayersPerSun; 
					playerAroundSun = 0;
					//print (PlayersPerSun);
				}
				else
				{
					break;
				}
			}

			i++;
			playerAroundSun++;
		}

		targets = GameObject.FindGameObjectsWithTag ("Player");

		OoB.transform.localScale = new Vector3(23+GlobalControlScript.GlobalControl.NumberOfPlayers,
		                                       23+GlobalControlScript.GlobalControl.NumberOfPlayers);

		fader.GetComponent<Fade>().sceneStarting = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		GamepadState state = GamePad.GetState(GamePad.Index.Any);
		if (startGame == true) {
			if (targets.Length <= 1) {
				
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
					Application.LoadLevel (2);
				}
				
			}
		} 
		
		else if (fader.GetComponent<Fade>().finished == false) {startTimer -= Time.deltaTime;}
		
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
			
			
			GUI.Label(new Rect(Screen.width/2,Screen.height/2,200,200),((int)startTimer+1).ToString());
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
			//GUI.Label (new Rect (Screen.width / 2 - 400, 150, 500, 200),"Player 1 col: " + targets [1].GetComponent<PlayerScript> ().CollisionForce);
			//GUI.Label (new Rect (Screen.width / 2 + 100, 150, 500, 200),"Player 2 col: " + targets [0].GetComponent<PlayerScript> ().CollisionForce);
		}
	}
}
