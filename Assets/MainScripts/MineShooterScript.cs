using UnityEngine;
using System.Collections;

public class MineShooterScript : MonoBehaviour {
	public float CircleSpeed;
	public float MaxForwardSpeed = 1; // Assuming negative Z is towards the camera
	public float MinForwardSpeed = 1;
	float forwardSpeed;
	float counter = 0;
	public float circleSize = 8;
	public KeyCode MyKey;
	float startingPos;
	float lastAngle = 0;
	float angle = 0;

	float buttonPressedLastTime = 0;
	float buttonPressedDuration = 0;
	float lastButtonPressedDuration = 100;
	float durationBetweenPresses = 0;

	public GameObject Projectile;
	public float ShotForce;
	
	private Vector3 curLoc;
	private Vector3 prevLoc;
	// Use this for initialization
	void Start () {
		startingPos = 0;
		transform.position = new Vector3(0,circleSize,0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Pos = new Vector3(0,0,0);
//		Pos.x = Mathf.Sin(counter * CircleSpeed + startingPos) * circleSize;
//		Pos.y  = Mathf.Cos(counter * CircleSpeed + startingPos) * circleSize;
//		Pos.z  += forwardSpeed * Time.deltaTime;
//		transform.position = Pos;
//		
//		Vector3 moveDirection = gameObject.transform.position - prevLoc;
//		if (moveDirection != Vector3.zero)
//		{
//			lastAngle = angle;
//			angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;
//			if (angle < 2 + lastAngle || angle > lastAngle - 2 )
//				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//			else
//				transform.rotation = Quaternion.AngleAxis(lastAngle, Vector3.forward);
//		}
		transform.RotateAround (Pos, Vector3.forward, CircleSpeed * Time.deltaTime);

		if(Input.GetKeyDown(MyKey))
		{
			durationBetweenPresses = Time.time - buttonPressedLastTime;
			buttonPressedLastTime = Time.time;
			buttonPressedDuration = Time.time - buttonPressedLastTime;

			if (lastButtonPressedDuration <= .5f && durationBetweenPresses <= .5f)
			{
				//change direction	
				//counter = angle;
				CircleSpeed = -CircleSpeed;
				lastButtonPressedDuration = 100;
				buttonPressedDuration = 0;
			}
			else
			{
				lastButtonPressedDuration = 100;
				buttonPressedDuration = 0;
				//shoot mine
				print("Shoot");
			}

		}
		if(Input.GetKey(MyKey))
		{
			buttonPressedDuration = Time.time - buttonPressedLastTime;
		}
		if (Input.GetKeyUp(MyKey))
		{
			if (lastButtonPressedDuration <= .5f && buttonPressedDuration <= .5f && durationBetweenPresses <= .75f)
			{
				lastButtonPressedDuration = 100;
				buttonPressedDuration = 0;
			}
			else{
				lastButtonPressedDuration = buttonPressedDuration;
				buttonPressedDuration = 0;
			}
		}
		counter += .01f;
		if (counter > 360)
		{
			counter -= 360;
		}
	}
}
