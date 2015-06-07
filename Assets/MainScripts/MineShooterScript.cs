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
	public float DefaultShotForce;
	public float MaxShotForce;
	float shotForce;
	public float ChargeAmount;
	GameObject camera;
	private Vector3 curLoc;
	private Vector3 prevLoc;
	bool shot = false;
	bool inPosition = false;
	bool stop = false;
	bool doubletapped = false;

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
				print("Shoot");
				GameObject tempMine = (GameObject)Instantiate(Projectile,transform.position,transform.rotation);
				tempMine.GetComponent<Rigidbody2D>().velocity = -transform.position.normalized * shotForce;
				shot = false;
				shotForce=DefaultShotForce;
			}

			if(Input.GetKeyDown(MyKey))
			{

				durationBetweenPresses = Time.time - buttonPressedLastTime;
				buttonPressedLastTime = Time.time;
				buttonPressedDuration = Time.time - buttonPressedLastTime;

				if (lastButtonPressedDuration <= .25f && durationBetweenPresses <= .25f)
				{
					shot = false;
					//change direction	
					//counter = angle;
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

				}

			}
			if(Input.GetKey(MyKey))
			{
				buttonPressedDuration = Time.time - buttonPressedLastTime;
				if (shotForce < MaxShotForce)
					shotForce += ChargeAmount;
				else
					shotForce = MaxShotForce;
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
