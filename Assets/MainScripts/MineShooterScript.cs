using UnityEngine;
using System.Collections;

public class MineShooterScript : MonoBehaviour {
	public float CircleSpeed;				//The speed at which the shooter circles the arena
	public float circleSize = 8;			//the size of the arena of which to circle around
	public KeyCode MyKey;					//the key that controls the shooter
	float startingPos;						//the starting position of the shooter randomly placed in a circle around the arena
	float angle = 0;						//the angle of the shooter in relation to the center of the arena

	float buttonPressedLastTime = 0;		//Time the button was pressed last
	float buttonPressedDuration = 0;		//the duration the button was held down for
	float lastButtonPressedDuration = 100;	//the duration the button was held down for Last
	float durationBetweenPresses = 0;		//the duration the between button presses
	float shotWaitTime = 0;					//Sets to the currect time when the button is released to add a short tiem period to see if it will be a double tap or not
	public float ShotRechargeDuration = 0;	//The amount of time it takes before you can fire a new shot
	float shotRechargeTime = 0;				//The time at which you fire a shot to restart the recharge
	public GameObject Projectile;			//the object you shoot
	public float DefaultShotForce;			//the base force you shoot at. should be a small number
	public float MaxShotForce;				//the most force you can shoot at.
	float shotForce;						//the current shot force, determined by how long you hold down the button
	public float ChargeAmount; 				//the amound the shotforce is increased by every frame the button is held down	
	GameObject camera;						//the camera	
	bool shot = false;						//if true, the button press is a shot, not a double tap.
	bool inPosition = false;				//if true, the shooter is locked in orbit around the arena
	bool stop = false;						//if true, stops the shooter from moving towards the center of the arena and prepares to lock it into place
	bool doubletapped = false;				//if true, the button press is a double tap, not a shot
	bool recharged = false;					//if true, the shooter is recharged and can be fired again

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
		shotForce=DefaultShotForce;
	}
	
	// Update is called once per frame
	void Update () {

		if (inPosition)
		{
			Vector3 Pos = new Vector3(0,0,0);

			transform.RotateAround (Pos, Vector3.forward, CircleSpeed * Time.deltaTime);

			if (Time.time >= shotWaitTime + .26f && shot)
			{
				//shoot mine
				if (recharged)
				{
					print("Shoot");
					GameObject tempMine = (GameObject)Instantiate(Projectile,transform.position,transform.rotation);
					tempMine.GetComponent<Rigidbody2D>().velocity = -transform.position.normalized * shotForce;
					shot = false;
					shotForce=DefaultShotForce;
					shotRechargeTime = Time.time;
				}
			}
			if (Time.time <= shotRechargeTime + ShotRechargeDuration)
			{
				recharged = false;
			}
			else{
				recharged = true;
			}
			if(Input.GetKeyDown(MyKey))
			{

				durationBetweenPresses = Time.time - buttonPressedLastTime;
				buttonPressedLastTime = Time.time;
				buttonPressedDuration = Time.time - buttonPressedLastTime;
				bool reset = false; //To fix the issue of triple+ tapping functioning like a double double tap
				if (doubletapped)
				{
					doubletapped = false;
					reset = true;
				}

				if (lastButtonPressedDuration <= .25f && durationBetweenPresses <= .25f && !reset)
				{
					shot = false;
					CircleSpeed = -CircleSpeed;
					lastButtonPressedDuration = 100;
					buttonPressedDuration = 0;
					doubletapped = true;
				}
				else
				{
					lastButtonPressedDuration = 100;
					buttonPressedDuration = 0;
					doubletapped = false;
					if (!recharged)
					{
						//play not recharged sound
					}

				}

			}
			if(Input.GetKey(MyKey))
			{
				buttonPressedDuration = Time.time - buttonPressedLastTime;
				if (recharged)
				{
					if (shotForce < MaxShotForce)
						shotForce += ChargeAmount;
					else
						shotForce = MaxShotForce;
				}
			}
			if (Input.GetKeyUp(MyKey))
			{

				lastButtonPressedDuration = buttonPressedDuration;
				buttonPressedDuration = 0;
			
				if (lastButtonPressedDuration > .25f)
					shotWaitTime = 0;
				else
					shotWaitTime = Time.time;


				if (!doubletapped)
					shot = true;
			}
		}
		else
		{
			if (stop)
			{
				if (GetComponent<Rigidbody2D>().velocity.magnitude > 0)
					GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.up.normalized.x * 75,-transform.up.normalized.y * 75));
				if (GetComponent<Rigidbody2D>().velocity.magnitude <= 40)
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
	void OnGUI()
	{
		// Find the 2D position of the object using the main camera
		Vector2 boxPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		// "Flip" it into screen coordinates
		boxPosition.y = Screen.height - boxPosition.y;
		GUIStyle TextStyle = new GUIStyle();
		TextStyle.normal.textColor = Color.black;
		// Center the label over the coordinates
		boxPosition.x -= 10 * 0.5f;
		boxPosition.y -= 75 * 0.5f;
		//if (!startboosted)
		GUI.Label (new Rect (boxPosition.x, boxPosition.y, 200, 200), MyKey.ToString(), TextStyle);
	}
}
