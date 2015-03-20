using UnityEngine;
using System.Collections;
using GamepadInput;

public class Menu : MonoBehaviour {

	public GameObject fader;
	// Use this for initialization
	void Start () {

		Time.timeScale = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

		GamepadState state = GamePad.GetState(GamePad.Index.Any);

		transform.Rotate (0,0,-state.LeftStickAxis.x*2);

		if (Input.GetKey (KeyCode.A))
						transform.Rotate (0, 0, 2);
		if (Input.GetKey (KeyCode.D))
			transform.Rotate (0, 0, -2);

		if(Input.GetKey(KeyCode.Space))
		{
			fader.GetComponent<Fade>().sceneStarting = true;
		}
		if (fader.GetComponent<Fade>().sceneStarting == true && fader.GetComponent<Fade>().finished == true)
		{
			Application.LoadLevel(1);
		}
	}

	void OnTiggerEnter2D(Collider2D col)
	{
	
	}
	void OnTriggerStay2D(Collider2D col)
	{
		GamepadState state = GamePad.GetState(GamePad.Index.Any);
		if(col.gameObject.name == "Play")
		{
			col.transform.parent.renderer.enabled = true;
		}
		if(col.gameObject.name == "Instructions")
		{
			col.transform.parent.renderer.enabled = true;
		}
		if(col.gameObject.name == "Quit")
		{
			col.transform.parent.renderer.enabled = true;
		}



		if(col.gameObject.name == "Play" && (state.A || Input.GetKey(KeyCode.Space)))
		{
			fader.GetComponent<Fade>().sceneStarting = true;
		}
		if(col.gameObject.name == "Instructions" && (state.A || Input.GetKey(KeyCode.Space)))
		{
			Application.LoadLevel(2);
		}
		if(col.gameObject.name == "Quit" && (state.A || Input.GetKey(KeyCode.Space)))
		{
			Application.Quit();
		}

	}
	void OnTriggerExit2D(Collider2D col)
	{
		col.transform.parent.renderer.enabled = false;
	}
}
