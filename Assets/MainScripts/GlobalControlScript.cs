using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobalControlScript : MonoBehaviour {
	public static GlobalControlScript GlobalControl;
	public int NumberOfPlayers;
	public List<KeyCode> KeysForPlayers = new List<KeyCode>();

	//Thoughts On Alternate Game Modes That would not be hard to implement
	//Implemented
	public bool OneSunOnly = false;
	//Not Implemented
	public bool FreezeTag = false;
	public bool NoRadiation = false;
	public bool NoGrowth = false;
	public bool SuddenDeath = false;


	void Awake () {
		if(!GlobalControl) {
			GlobalControl = this;
			DontDestroyOnLoad(gameObject);
		}else
			Destroy(gameObject);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
