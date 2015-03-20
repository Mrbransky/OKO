using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class PlayerScript : MonoBehaviour {

	float pullForce = -35;
	float distance;

	public bool isDisabled = false;
	public bool shieldOn = false;

    public int LifeTotal;
	public AudioClip impact;


	//public float speed = 2;
	//public Vector2 speed = new Vector2(15,15);
	float curSpeed = 25;

	float timer = 0;
	float powerTimer = 4;
	bool gravControl = false;

	int powerUpRan;
	string curPlayer;
//	[Header("GamePad")]
	int playerNum;
//	[Header("GameObjects")]
	public List< GameObject> bHoles = new List<GameObject>();
	public GameObject enemyPlayer;
	public GameObject shield;
	public GameObject control;
	public GameObject explosionPrefab;
	public GameObject bHoleDamage;
	//public Vector3 axis = Vector3.up;
	//public Vector3 desiredPosition;
	//public float radius = 2.0f;
	//public float radiusSpeed = 0.5f;
	//public float rotationSpeed = 80.0f;
	GameObject myCamera;
	bool startboosted = false;
	bool facingForward = true;
	float buttonPressedLastTime = 0;
	float buttonPressedDuration = 0;
	float lastButtonPressedDuration = 100;
	/*TO DO
		-Better physic-based controls
		-Implement any sounds
		-Maybe change how orbit physics are done
		-Animations (AFFORDANCES PLS)
		-Decide on end screen shenanigans

*/


	// Use this for initialization
	void Start () {
		foreach (GameObject bhole in GameObject.FindGameObjectsWithTag("BlackHole"))
		{
			bHoles.Add(bhole);
		}
		Time.timeScale = 1.0f;
		curPlayer = this.gameObject.name;
        LifeTotal = 10;
		control = GameObject.FindGameObjectWithTag ("Control");
		if (GameObject.Find("Main Camera") != null)
			myCamera = GameObject.Find("Main Camera");
		else
			Debug.LogError("NO 'Main Camera' FOUND!");
	}
	

	void Update () {

		if (LifeTotal <= 0) {
				
			isDisabled = true;
		}
		//THIS IS THE THING THAT CONTROLS THE THINGS
		//			|
		//			|
		//			|
		//			V
		switch (gameObject.name) {
		case "Player 1":
			Controls (GamePad.Index.One);
			break;
		case "Player 2":
			Controls (GamePad.Index.Two);	
			break;
		default:
			break;
		}

		foreach (Collider2D otherObj in Physics2D.OverlapCircleAll(transform.position,50))
		{
			if ((otherObj.tag == "Player" && otherObj.gameObject != gameObject) || otherObj.tag == "Meteor")
			{
				rigidbody2D.AddForce((transform.position - otherObj.transform.position).normalized * -2 * rigidbody2D.mass);
		
			}
		}


		if (control.GetComponent<Control> ().startGame == false) {
			
			pullForce = 0;


		}
		else{
			if (!startboosted)
			{
				rigidbody2D.AddForce(transform.up * 1000);
				startboosted = true;
			}
			pullForce = -35;
		
		}

		//distance = Vector2.Distance(this.transform.position,bHole.transform.position);


		//force = distance * -20;
		
		//how far players can move
		if (distance <= 50) {
			
			gravControl = true;
			
				} else {

			gravControl = false;

				}


		if (shieldOn == true) { powerTimer -= Time.deltaTime;}
		if (powerTimer <= 0) {powerTimer = 4;}


	}
	void FixedUpdate()
	{
		if(isDisabled == false && control.GetComponent<Control>().startGame)
		{
			
			//rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
			//                                 Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));

			//move in direction of current joystick
			if(gameObject.name == "Player 1"){
//			if(Input.GetKey(KeyCode.W))
//			{
//				rigidbody2D.AddForce(Vector2.up*curSpeed);
//			}
//			if(Input.GetKey(KeyCode.A))
//			{
//				rigidbody2D.AddForce(-Vector2.right*curSpeed);
//			}
//			if(Input.GetKey(KeyCode.D))
//			{
//					rigidbody2D.AddForce(Vector2.right*curSpeed);
//			}
//			if(Input.GetKey(KeyCode.S))
//			{
//					rigidbody2D.AddForce(-Vector2.up*curSpeed);
//			}

				if(Input.GetKeyDown(KeyCode.Z))
				{
					buttonPressedLastTime = Time.time;
					buttonPressedDuration = 0;
				}
				if(Input.GetKey(KeyCode.Z))
				{
					buttonPressedDuration = Time.time - buttonPressedLastTime;
					rigidbody2D.AddForce(transform.up*curSpeed);
				}
				if (Input.GetKeyUp(KeyCode.Z))
				{
					if (lastButtonPressedDuration <= .25f && buttonPressedDuration <= .25f)
					{
						facingForward = !facingForward;
						rigidbody2D.velocity = -rigidbody2D.velocity;
						lastButtonPressedDuration = 100;
						buttonPressedDuration = 0;
					}
					else{
						lastButtonPressedDuration = buttonPressedDuration;
						buttonPressedDuration = 0;
					}
				}
			}
			if(gameObject.name == "Player 2"){
//				if(Input.GetKey(KeyCode.UpArrow))
//				{
//					rigidbody2D.AddForce(Vector2.up*curSpeed);
//				}
//				if(Input.GetKey(KeyCode.LeftArrow))
//				{
//					rigidbody2D.AddForce(-Vector2.right*curSpeed);
//				}
//				if(Input.GetKey(KeyCode.RightArrow))
//				{
//					rigidbody2D.AddForce(Vector2.right*curSpeed);
//				}
//				if(Input.GetKey(KeyCode.DownArrow))
//				{
//					rigidbody2D.AddForce(-Vector2.up*curSpeed);
//				}

				if(Input.GetKeyDown(KeyCode.M))
				{
					buttonPressedLastTime = Time.time;
					buttonPressedDuration = 0;
				}
				if(Input.GetKey(KeyCode.M))
				{
					buttonPressedDuration = Time.time - buttonPressedLastTime;
					rigidbody2D.AddForce(transform.up*curSpeed);
				}
				if (Input.GetKeyUp(KeyCode.M))
				{
					if (lastButtonPressedDuration <= .25f && buttonPressedDuration <= .25f)
					{
						facingForward = !facingForward;
						rigidbody2D.velocity = -rigidbody2D.velocity;
						lastButtonPressedDuration = 100;
						buttonPressedDuration = 0;
					}
					else{
						lastButtonPressedDuration = buttonPressedDuration;
						buttonPressedDuration = 0;
					}
				}
			}
		}
		else
		{
			//how long disable is active
			timer += Time.deltaTime;
			
			if(timer >= 2)
			{
				timer = 0;
				isDisabled = false;
			}
			
			
		}

		if (rigidbody2D.velocity != Vector2.zero) {
			Vector3 dir = (Vector3)rigidbody2D.velocity - (Vector3)transform.position;
			//float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			float angle = Vector3.Angle(rigidbody2D.velocity.normalized,Vector3.up);
			if (rigidbody2D.velocity.normalized.x > 0)
			{
				angle = -angle;
			}
			if (facingForward)
			{
			Quaternion rot = transform.rotation;
			transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
			
			//rot = Quaternion.LookRotation(rigidbody2D.velocity, Vector3.up);
			//print (rot.eulerAngles);
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
			}
			else{
				Quaternion rot = transform.rotation;
				transform.rotation = Quaternion.AngleAxis(-angle , Vector3.forward);
				transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
			}
		}

		//rigidbody2D.velocity = movement;
		if (gravControl == true) {
			foreach (GameObject bHole in bHoles)
			{
//				float sideA = Mathf.Sqrt(Mathf.Pow(transform.position.x - bHole.transform.position.x,2) + Mathf.Pow(transform.position.y - bHole.transform.position.y,2));
//				float sideB = 3;
//				float sideC = Mathf.Sqrt(Mathf.Pow(sideA,2) + Mathf.Pow(sideB,2));
//				float semiPerimeter = .5f*(sideA + sideB + sideC);
				Vector3 dir = (bHole.transform.position-transform.position).normalized;

				Vector3 movement = Vector3.zero;
				float currentAngle = Vector3.Angle(dir, bHole.transform.position + Vector3.up);
				if (transform.position.x > bHole.transform.position.x)
				{
					currentAngle = -currentAngle;
				}

				movement.x = dir.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad)*2;
				movement.y = dir.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad)*2;	

					//test on y
				if (rigidbody2D.velocity.y > 1f || rigidbody2D.velocity.y < -1f )
				{
					movement.x = dir.x + Mathf.Cos(currentAngle+90 * Mathf.Deg2Rad)*2;
					movement.y = dir.y + Mathf.Sin(currentAngle+90 * Mathf.Deg2Rad)*2;					
				}
				else
				{
					movement.x = dir.x + Mathf.Cos(currentAngle-90 * Mathf.Deg2Rad)*2;
					movement.y = dir.y + Mathf.Sin(currentAngle-90 * Mathf.Deg2Rad)*2;
				}

					//test on x
				if (rigidbody2D.velocity.x > 1f || rigidbody2D.velocity.x < -1f)
				{
					movement.x += dir.x + Mathf.Cos(currentAngle+90 * Mathf.Deg2Rad)*2;
					movement.y += dir.y + Mathf.Sin(currentAngle+90 * Mathf.Deg2Rad)*2;					
				}
				else
				{
					movement.x += dir.x + Mathf.Cos(currentAngle-90 * Mathf.Deg2Rad)*2;
					movement.y += dir.y + Mathf.Sin(currentAngle-90 * Mathf.Deg2Rad)*2;
				}

				movement.x = movement.x/2;
				movement.y = movement.y/2;

				movement = (movement) + (transform.up/1.5f) + (dir * 4);

//				//dir = (Quaternion.AngleAxis(90, Vector3.up) * dir)*2;
//				//bHole.transform.position + 
//				//orbit controls
				Debug.Log(gameObject.name + " " + dir);
				Debug.DrawLine(transform.position,(transform.position + movement));
				Debug.DrawLine(transform.position,(transform.position + (Vector3)rigidbody2D.velocity));
				rigidbody2D.AddForce(-(movement.normalized) * pullForce * rigidbody2D.mass);
				//rigidbody2D.AddForce(-(dir.normalized) * pullForce * rigidbody2D.mass);
				//rigidbody2D.AddForce((transform.up) * pullForce/4 * rigidbody2D.mass);
			}

			//transform.RotateAround (bHole.transform.position, axis, rotationSpeed * Time.deltaTime);
			//desiredPosition = (transform.position - bHole.transform.position).normalized * radius + bHole.transform.position;
			//transform.position = Vector2.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
		}

	}


	void OnGUI()
	{
		// Find the 2D position of the object using the main camera
		Vector2 boxPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		
		// "Flip" it into screen coordinates
		boxPosition.y = Screen.height - boxPosition.y;
		
		// Center the label over the coordinates
		boxPosition.x -= 50 * 0.5f;
		boxPosition.y -= 100 * 0.5f;

		GUI.Label (new Rect (boxPosition.x, boxPosition.y, 200, 200), name);
	}

//-----------------------------------

	void Controls(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		if(isDisabled == false)
		{
			//move in direction of current joystick
			rigidbody2D.AddForce(state.LeftStickAxis*curSpeed);
			
			//rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));
		}
		else
		{
			//how long disable is active
			timer += Time.deltaTime;
			
			if(timer >= 2)
			{
				timer = 0;
				isDisabled = false;
			}
			
			
		}
	}

//--------------------------------

	void ActivateShield()
	{
		if (shieldOn == true) {
			//spawn shield and child it
			GameObject shield1 = (GameObject)Instantiate (shield, transform.position, transform.rotation);
			shield1.transform.parent = gameObject.transform;
		}
	}
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject.tag == "BlackHole")
		{
			if (LifeTotal > 0)
			{
				LifeTotal--;
			}
			else if (LifeTotal <= 0)
			{
				Destroy (gameObject);
			}
			
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "BlackHole")
		{
//			foreach (ContactPoint2D contact in col.contacts) {
//				Vector3 pos = contact.point;
//				Instantiate(bHoleDamage,pos, Quaternion.identity);
//			}
            if (LifeTotal <= 0)
            {
                Destroy(gameObject);
                Debug.Log("End Game");
            }

			
		}
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log(col.transform.position.normalized);
			//rigidbody2D.AddForce(((col.transform.position).normalized) * Mathf.Pow(2,10/(LifeTotal+1))* rigidbody2D.mass);
			//col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * Mathf.Pow(2,10/(col.gameObject.GetComponent<PlayerScript>().LifeTotal + 1))* rigidbody2D.mass);

			audio.PlayOneShot(impact);
			myCamera.GetComponent<CameraShakeScript>().Shake(.5f);
//			foreach (ContactPoint2D contact in col.contacts) {
//				Vector3 pos = contact.point;
//				Instantiate(explosionPrefab,pos, Quaternion.identity);
//			}

			if (LifeTotal <= 0){
				isDisabled = true;
			}
			else{LifeTotal--;}

//			if(this.rigidbody2D.velocity.x > enemyPlayer.rigidbody2D.velocity.x)
//			{
//                if (LifeTotal <= 0){
//					isDisabled = true;
//					}
//                else{
//					LifeTotal--;}
//			}
//			if(this.rigidbody2D.velocity.y > enemyPlayer.rigidbody2D.velocity.y)
//			{
//                if (LifeTotal <= 0){
//					isDisabled = true;
//					}
//                else{
//					LifeTotal--;}
//			}
		}
		if(curPlayer == "Player 1" && col.gameObject.name == "Player2Mine(Clone)")
		{
			Destroy (col.gameObject);

            if (LifeTotal <= 0){
				isDisabled = true;}
            else{
				LifeTotal--;}
			//Explosion animation
		}
		else if (curPlayer == "Player 2" && col.gameObject.name == "Player1Mine(Clone)")
		{
			Destroy (col.gameObject);
//			foreach (ContactPoint2D contact in col.contacts) {
//				Vector3 pos = contact.point;
//				Instantiate(explosionPrefab,pos, Quaternion.identity);
//			}
            if (LifeTotal <= 0)
                isDisabled = true;
            else
                LifeTotal--;
		}
	}
//	void OnTriggerEnter2D(Collider2D collider)
//	{
//
//		if(collider.gameObject.tag == "PowerUp")
//		{
//			powerUpRan = Random.Range(1,3);
//			Destroy(collider.gameObject);
//
//			//Give power up
//			switch (powerUpRan)
//			{
//			case 1:
//				GetComponent<playerShoot>().mineActive = true;
//				break;
//			case 2:
//				shieldOn = true;
//				ActivateShield();
//				break;
//			case 3:
//				//Something I need to do 
//				//something cool
//				// rly cool.
//                //omg wat
//				break;
//			default:
//				break;
//			}
//		}
//
//	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Bounds")
		{
			Destroy (gameObject);
		}
	}
}
