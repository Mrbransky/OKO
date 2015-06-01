using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {
	public GameObject fader;
	public GameObject SM;
	// Use this for initialization
	void Start () {
		if (GameObject.Find("GlobalControl") != null)
		{

		}
		else{
			GameObject globalcontrol = new GameObject();
			globalcontrol.AddComponent<GlobalControlScript>();
			GameObject sm = (GameObject)Instantiate(SM);
			sm.transform.SetParent(globalcontrol.transform);
			globalcontrol.name = "GlobalControl";
		}
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
