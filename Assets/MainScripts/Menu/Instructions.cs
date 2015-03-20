using UnityEngine;
using System.Collections;
using GamepadInput;

public class Instructions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GamepadState state = GamePad.GetState(GamePad.Index.Any);

		if (state.B) {
			Application.LoadLevel(0);
		}
	
	}
}
