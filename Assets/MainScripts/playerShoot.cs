using UnityEngine;
using System.Collections;
using GamepadInput;

public class playerShoot : MonoBehaviour {

	//public GameObject projectile;
	public GameObject mine;
	public GameObject playerObject;
	public KeyCode shoot = KeyCode.Space;
	float timer = 4;
	public bool mineActive = false;
	//public bool turretActive = false;

	void Controls(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		if((Input.GetKeyDown(shoot)|| state.X)&& mineActive == true)
		{
			
			Vector2 playerPosition = (playerObject.transform.position);
			Instantiate(mine, playerPosition, Quaternion.identity);
			
			
		}
	}
	void Update () {

		if (mineActive == true) {
				
			timer -= Time.deltaTime;
		
		}
		if (timer <= 0) {
				
			timer = 4;
			mineActive = false;
		
		}


		/*if(Input.GetKeyDown(shoot)&& turretActive == true)
		{
			Vector2 playerPosition = (playerObject.transform.position);
			Instantiate(projectile, playerPosition, Quaternion.identity);

		}*/
		if (gameObject.name == "Player 1") {
			Controls (GamePad.Index.One);		
		}
		if (gameObject.name == "Player 2") {
			Controls (GamePad.Index.Two);		
		}

	}
}
