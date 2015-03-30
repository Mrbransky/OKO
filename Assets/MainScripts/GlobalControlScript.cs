using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobalControlScript : MonoBehaviour {
	public static GlobalControlScript GlobalControl;
	public int NumberOfPlayers;
	public List<KeyCode> KeysForPlayers = new List<KeyCode>();



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
