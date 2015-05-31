using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {
	public GameObject fader;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return))
		{
			fader.GetComponent<Fade>().sceneStarting = true;
		}
		if (fader.GetComponent<Fade>().sceneStarting == true && fader.GetComponent<Fade>().finished == true)
		{
			Application.LoadLevel(1);
		}
	}
}
