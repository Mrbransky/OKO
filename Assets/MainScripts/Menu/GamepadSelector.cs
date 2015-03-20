using UnityEngine;
using System.Collections;
using GamepadInput;

public class GamepadSelector : MonoBehaviour {
	
	public GameObject p1,p2,p3,p4;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
		GamepadState state = GamePad.GetState(GamePad.Index.Any);
		Player1 (GamePad.Index.One);
		Player2 (GamePad.Index.Two);
		Player3 (GamePad.Index.Three);
		Player4 (GamePad.Index.Four);

		if (state.Start) {
			Application.LoadLevel(1);		
		}


	}

	void Player1(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		if (state.A) {

			p1.renderer.enabled = true;

			}
		}
	void Player2(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		if (state.A) {
			
			p2.renderer.enabled = true;
			
		}
	}
	void Player3(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		
	}
	void Player4(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);		
	}
}
