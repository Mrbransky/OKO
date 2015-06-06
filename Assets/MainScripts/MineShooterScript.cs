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
	float shotWaitTime = 0;
	public float ShotRechargeTime = 0;
	public GameObject Projectile;
	public float ShotForce;
	GameObject camera;
	private Vector3 curLoc;
	private Vector3 prevLoc;
	bool shot = false;
	bool inPosition = false;
	bool stop = false;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera");
		Time.timeScale = 1;
		circleSize = camera.GetComponent<Camera>().orthographicSize * 2;
		startingPos = Random.Range(0,360);
		transform.position = new Vector3(Mathf.Sin(Time.time * CircleSpeed + startingPos) * circleSize,Mathf.Cos(Time.time * CircleSpeed + startingPos) * circleSize,0);
		Vector3 vectorToTarget =  -transform.position;
		float angle = Mathf.Atan2(vectorToTarget.x, vectorToTarget.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {

		if (inPosition)
		{
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
			if (Time.time >= shotWaitTime + .36f && shot)
			{
				//shoot mine
				print("Shoot");
				GameObject tempMine = (GameObject)Instantiate(Projectile,transform.position,transform.rotation);
				tempMine.GetComponent<Rigidbody2D>().velocity = -transform.position.normalized * ShotForce;
				shot = false;
			}
			if(Input.GetKeyDown(MyKey))
			{
				shotWaitTime = Time.time;
				shot = true;
				durationBetweenPresses = Time.time - buttonPressedLastTime;
				buttonPressedLastTime = Time.time;
				buttonPressedDuration = Time.time - buttonPressedLastTime;

				if (lastButtonPressedDuration <= .5f && durationBetweenPresses <= .35f)
				{
					shot = false;
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


				}

			}
			if(Input.GetKey(MyKey))
			{
				buttonPressedDuration = Time.time - buttonPressedLastTime;
			}
			if (Input.GetKeyUp(MyKey))
			{
					lastButtonPressedDuration = buttonPressedDuration;
					buttonPressedDuration = 0;
			}
			counter += .01f;
			if (counter > 360)
			{
				counter -= 360;
			}
		}
		else
		{
			if (stop)
			{
				if (GetComponent<Rigidbody2D>().velocity.magnitude > 0)
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.up.normalized.x * 75,-transform.up.normalized.y * 75));
				if (GetComponent<Rigidbody2D>().velocity.magnitude <= 1.5f)
				{
					GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					inPosition = true;
				}
			}
			else
			{
				//print(GetComponent<Rigidbody2D>().velocity.magnitude);

				if (GetComponent<Rigidbody2D>().velocity.magnitude < 100)
					GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.normalized.x * 100,transform.up.normalized.y * 100);

				print(GetComponent<Rigidbody2D>().velocity);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Bounds")
			stop = true;
	}
}
