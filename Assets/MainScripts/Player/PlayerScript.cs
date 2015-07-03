using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamepadInput;

public class PlayerScript : MonoBehaviour {

	float pullForce = -35;
	float distance;

	public bool isDisabled = false;
	public bool shieldOn = false;

    public float DamageAmount;
	public float MaxDamageAmount;
	public AudioClip impact;

	public KeyCode MyKey;
	//public float speed = 2;
	//public Vector2 speed = new Vector2(15,15);
	public GameObject EngineBoost;
	public GameObject AtmosphereEffect;
	float curSpeed = 35;

	float timer = 0;
	float powerTimer = 4;
	bool gravControl = false;

	int powerUpRan;
	string curPlayer;

	int playerNum;

	public List< GameObject> bHoles = new List<GameObject>();
//	public GameObject enemyPlayer;
	public GameObject shield;
	public GameObject control;
	public GameObject explosionPrefab;
	public float AtmosphereDamage;
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
	float durationBetweenPresses = 0;
	bool flipping = false;
	bool doubleTapped = false;
	float doubleTappedTime =0;
	Vector3 velocityToFlip = Vector3.zero;
	public Vector3 CollisionForce =Vector3.zero;
	public GameObject MineShooter;
	List<GameObject> ignoreCollisionList = new List<GameObject>();

    private soundManager SM;

	/*TODO
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
        DamageAmount = 1f;
		control = GameObject.FindGameObjectWithTag ("Control");
		if (GameObject.Find("Main Camera") != null)
			myCamera = GameObject.Find("Main Camera");
		else
			Debug.LogError("NO 'Main Camera' FOUND!");

        //SoundManager
        SM = GameObject.Find("GlobalControl").GetComponentInChildren<soundManager>();
		EngineBoost.SetActive(true);
		SM.ThrusterFunction(false);
	}
	

	void Update () {

		if (DamageAmount <= 0) {
				
			isDisabled = true;
		}
		if (DamageAmount >MaxDamageAmount)
		{
			DamageAmount = MaxDamageAmount;
		}
		//THIS IS THE THING THAT CONTROLS THE THINGS
		//			|
		//			|
		//			|
		//			V
//		switch (gameObject.name) {
//		case "Player 1":
//			Controls (GamePad.Index.One);
//			break;
//		case "Player 2":
//			Controls (GamePad.Index.Two);	
//			break;
//		default:
//			break;
//		}

		foreach (Collider2D otherObj in Physics2D.OverlapCircleAll(transform.position,20))
		{
			if ((otherObj.tag == "Player" && otherObj.gameObject != gameObject) || otherObj.tag == "Meteor")
			{
				GetComponent<Rigidbody2D>().AddForce((transform.position - otherObj.transform.position).normalized * -2 * GetComponent<Rigidbody2D>().mass);
		
			}
		}

		if (Mathf.Abs(CollisionForce.magnitude) > CollisionForce.normalized.magnitude)
		{

			CollisionForce -= CollisionForce.normalized/2;
			//print (CollisionForce);
		}
		else
		{
			CollisionForce = Vector3.zero;
		}

		if (control.GetComponent<Control> ().startGame == false) {
			
			pullForce = 0;


		}
		else{
			if (!startboosted)
			{
				GetComponent<Rigidbody2D>().AddForce(transform.up * 3000);
				startboosted = true;
			}
			pullForce = -100;
		
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


		if(control.GetComponent<Control>().startGame)
		{
		
			testCollision();

			//rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
			//                                 Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));

			if (flipping)
			{
				flipVelocity();
			}

			//move in direction of current joystick
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
			if (doubleTapped)
			{
				if (Time.time >= doubleTappedTime + .35f)
					doubleTapped = false;
			}
			//When Pressing the MyKey, boost or Flip if double tap.
			if(Input.GetKeyDown(MyKey))
			{
				durationBetweenPresses = Time.time - buttonPressedLastTime;
				buttonPressedLastTime = Time.time;
				buttonPressedDuration = Time.time - buttonPressedLastTime;
				//EngineBoost.SetActive(true);
				EngineBoost.GetComponent<Animator>().SetBool("ButtonPressed",true);

				if (lastButtonPressedDuration <= .5f && buttonPressedDuration <= .4f && durationBetweenPresses <= .35f && !doubleTapped)
				{
					facingForward = !facingForward;
					//rigidbody2D.velocity = -rigidbody2D.velocity;
					flipping = true;
					velocityToFlip = GetComponent<Rigidbody2D>().velocity;
					lastButtonPressedDuration = 100;
					buttonPressedDuration = 0;
					doubleTapped = true;
					doubleTappedTime = Time.time;
				}
				else{
					lastButtonPressedDuration = 100;
					buttonPressedDuration = 0;
				}



                //sound for thrusters
                SM.ThrusterFunction(true);
			}
			if(Input.GetKey(MyKey))
			{
				buttonPressedDuration = Time.time - buttonPressedLastTime;
				GetComponent<Rigidbody2D>().AddForce(transform.up*curSpeed);
			}
			if (Input.GetKeyUp(MyKey))
			{
				EngineBoost.GetComponent<Animator>().SetBool("ButtonPressed",false);
				//EngineBoost.SetActive(false);
				lastButtonPressedDuration = buttonPressedDuration;
				buttonPressedDuration = 0;

                //stops thruster sound
                SM.ThrusterFunction(false);
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

		if (GetComponent<Rigidbody2D>().velocity != Vector2.zero) {
			Vector3 dir = ((Vector3)GetComponent<Rigidbody2D>().velocity-CollisionForce) - (Vector3)transform.position;
			//float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			float angle;
			angle = Vector3.Angle(((Vector3)GetComponent<Rigidbody2D>().velocity-CollisionForce).normalized,Vector3.up);

//			if (CollisionForce.x > rigidbody2D.velocity.x)
//				angle = Vector3.Angle(((Vector3)rigidbody2D.velocity+CollisionForce).normalized,Vector3.up);
//			else
//				angle = Vector3.Angle(((Vector3)rigidbody2D.velocity-CollisionForce).normalized,Vector3.up);
			if (((Vector3)GetComponent<Rigidbody2D>().velocity-CollisionForce).normalized.x > 0)
			{
				angle = -(angle-2);
			}
//			if (facingForward)
//			{
			Quaternion rot = transform.rotation;
			transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
			
			//rot = Quaternion.LookRotation(rigidbody2D.velocity, Vector3.up);
			//print (rot.eulerAngles);
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
//			}
//			else{
//				Quaternion rot = transform.rotation;
//				transform.rotation = Quaternion.AngleAxis(-angle , Vector3.forward);
//				transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
//			}
		}

		//pulls the player around the suns
		if (gravControl == true) {
			foreach (GameObject bHole in bHoles)
			{
//				float sideA = Mathf.Sqrt(Mathf.Pow(transform.position.x - bHole.transform.position.x,2) + Mathf.Pow(transform.position.y - bHole.transform.position.y,2));
//				float sideB = 3;
//				float sideC = Mathf.Sqrt(Mathf.Pow(sideA,2) + Mathf.Pow(sideB,2));
//				float semiPerimeter = .5f*(sideA + sideB + sideC);
				Vector3 dir = (bHole.transform.position-transform.position).normalized;
				float dist = (bHole.transform.position-transform.position).magnitude;
				if (dist == 0)
				{
					dist = .000001f;
				}
				Vector3 movement = Vector3.zero;
				float currentAngle = Vector3.Angle(dir, bHole.transform.position + Vector3.up);
				if (transform.position.x > bHole.transform.position.x)
				{
					currentAngle = -currentAngle;
				}

				movement.x = dir.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad)*3;
				movement.y = dir.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad)*3;	

					//test on y
				if (GetComponent<Rigidbody2D>().velocity.y > .5f || GetComponent<Rigidbody2D>().velocity.y < -.5f )
				{
					movement.x = dir.x + Mathf.Cos(currentAngle+90 * Mathf.Deg2Rad)*3;
					movement.y = dir.y + Mathf.Sin(currentAngle+90 * Mathf.Deg2Rad)*3;					
				}
				else
				{
					movement.x = dir.x + Mathf.Cos(currentAngle-90 * Mathf.Deg2Rad)*3;
					movement.y = dir.y + Mathf.Sin(currentAngle-90 * Mathf.Deg2Rad)*3;
				}

					//test on x
				if (GetComponent<Rigidbody2D>().velocity.x > .5f || GetComponent<Rigidbody2D>().velocity.x < -.5f)
				{
					movement.x += dir.x + Mathf.Cos(currentAngle+90 * Mathf.Deg2Rad)*3;
					movement.y += dir.y + Mathf.Sin(currentAngle+90 * Mathf.Deg2Rad)*3;					
				}
				else
				{
					movement.x += dir.x + Mathf.Cos(currentAngle-90 * Mathf.Deg2Rad)*3;
					movement.y += dir.y + Mathf.Sin(currentAngle-90 * Mathf.Deg2Rad)*3;
				}

				movement.x = movement.x/2;
				movement.y = movement.y/2;

				movement = ((movement*1.25f) + (transform.up/1.325f) + (dir * (55f* DamageAmount)/dist * 10f/1.05f)/ Mathf.Pow(GlobalControlScript.GlobalControl.NumberOfSuns, (1 / 1.25f)));

//				//dir = (Quaternion.AngleAxis(90, Vector3.up) * dir)*2;
//				//bHole.transform.position + 
//				//orbit controls
				//Debug.Log(gameObject.name + " " + dir);
				Debug.DrawLine(transform.position,(transform.position + movement),Color.red);
				Debug.DrawLine(transform.position,(transform.position + CollisionForce),Color.yellow);
//				if (CollisionForce.x > rigidbody2D.velocity.x)
//					Debug.DrawLine(transform.position,(transform.position + (Vector3)rigidbody2D.velocity + CollisionForce),Color.blue);
//				else
//					Debug.DrawLine(transform.position,(transform.position + (Vector3)rigidbody2D.velocity - CollisionForce),Color.blue);
				Debug.DrawLine(transform.position,(transform.position + (Vector3)GetComponent<Rigidbody2D>().velocity - CollisionForce),Color.blue);

				Debug.DrawLine(transform.position,(transform.position + (Vector3)GetComponent<Rigidbody2D>().velocity),Color.white);
				GetComponent<Rigidbody2D>().AddForce(-(movement.normalized) * pullForce * GetComponent<Rigidbody2D>().mass);
				//rigidbody2D.AddForce(-(dir.normalized) * pullForce * rigidbody2D.mass);
				//rigidbody2D.AddForce((transform.up) * pullForce/4 * rigidbody2D.mass);
			}

			//transform.RotateAround (bHole.transform.position, axis, rotationSpeed * Time.deltaTime);
			//desiredPosition = (transform.position - bHole.transform.position).normalized * radius + bHole.transform.position;
			//transform.position = Vector2.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
		}

	}
	void testCollision()
	{
//		Debug.DrawLine(new Vector2 (transform.position.x - 2f, transform.position.y+2f), 
//		               new Vector2 (transform.position.x + 2f, transform.position.y+3.5f));
//		foreach (Collider2D otherobj in Physics2D.OverlapAreaAll(new Vector2 (transform.position.x - 2f, transform.position.y-.375f), 
//		                                                         new Vector2 (transform.position.x + 2f, transform.position.y+.375f)))
//		{
//			bool ignoreMe = false;
//			List<GameObject> addToIgnore = new List<GameObject>();
//			foreach (GameObject ignoreObj in ignoreCollisionList)
//			{
//				if (otherobj.gameObject == ignoreObj)
//				{
//					ignoreMe = true;
//					break;
//				}
//			}
//			if (otherobj.tag == "Player" && otherobj.gameObject != gameObject && !ignoreMe)
//			{
//				CollisionForce = (otherobj.gameObject.rigidbody2D.velocity)/(2f/DamageAmount) /** Mathf.Pow(1.2f,DamageAmount)* rigidbody2D.mass*/;
//				rigidbody2D.velocity += (Vector2)CollisionForce;
//				
//				DamageAmount=DamageAmount*1.05f;
//				addToIgnore.Add(otherobj.gameObject);
//			}
//			else
//			{
//				addToIgnore.Add(otherobj.gameObject);
//			}
//			ignoreCollisionList.Clear();
//			foreach(GameObject addIgnore in addToIgnore)
//			{
//				ignoreCollisionList.Add(addIgnore);
//			}
//		}
	}
	void flipVelocity()
	{
		GetComponent<Rigidbody2D>().velocity =  ((Vector3)GetComponent<Rigidbody2D>().velocity) - velocityToFlip.normalized * 10;
		if (((Vector3)GetComponent<Rigidbody2D>().velocity).magnitude < velocityToFlip.magnitude)
		{

		}
		else
		{
			GetComponent<Rigidbody2D>().velocity =  ((Vector3)GetComponent<Rigidbody2D>().velocity+CollisionForce) + velocityToFlip.normalized * 10;
			flipping = false;
		}
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

//-----------------------------------

	void Controls(GamePad.Index controller)
	{
		GamepadState state = GamePad.GetState(controller);
		if(isDisabled == false)
		{
			//move in direction of current joystick
			GetComponent<Rigidbody2D>().AddForce(state.LeftStickAxis*curSpeed);
			
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
	void OnTriggerStay2D(Collider2D col)
	{
//		if(col.gameObject.tag == "BlackHole")
//		{
//			if (DamageAmount > 0)
//			{
//				DamageAmount--;
//			}
//			else if (DamageAmount <= 0)
//			{
//				Destroy (gameObject);
//			}
//			
//		}
		if (col.tag == "Atmosphere")
		{
			DamageAmount += AtmosphereDamage;
			AtmosphereEffect.SetActive(true);
			AtmosphereEffect.transform.rotation = Quaternion.LookRotation(transform.position-col.transform.position);
		}
	}
//	void OnCollisionEnter2D(Collision2D col)
//	{
//		if(col.gameObject.tag == "FrontOfShip")
//		{
//			if (col.gameObject.GetComponent<PlayerFrontScript>().Parent != gameObject)
//			{
//				
//				CollisionForce = (col.gameObject.transform.parent.rigidbody2D.velocity)/(2f/DamageAmount) /** Mathf.Pow(1.2f,DamageAmount)* rigidbody2D.mass*/;
//				
//				foreach (ContactPoint2D contact in col.contacts) {
//					//				Vector3 pos = contact.point;
//					//				Instantiate(bHoleDamage,pos, Quaternion.identity);
//					rigidbody2D.AddForceAtPosition((Vector2)CollisionForce*25,contact.point);
//				}
//				
//				
//				print ("HIT");
//				DamageAmount=DamageAmount*1.05f;                  
//			}
//		}
//	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "MineExplosion")
		{
			//print("Called");
			if (CollisionForce == Vector3.zero)
				CollisionForce = ((transform.position - col.transform.position).normalized * 
				                  (100/(1+(transform.position - col.transform.position).magnitude)))/(2f/DamageAmount)/2;
			else
				CollisionForce = (CollisionForce+((transform.position - col.transform.position).normalized * 
				                                  (100/(1+(transform.position - col.transform.position).magnitude)))/(2f/DamageAmount))/2;
			
			GetComponent<Rigidbody2D>().AddForce((Vector2)CollisionForce*1500);
			
			DamageAmount=DamageAmount*1.05f;  
			
		}
		if(col.gameObject.tag == "PlayerMineExplosion")
		{
			//print("Called");
			if (CollisionForce == Vector3.zero)
				CollisionForce = ((transform.position - col.transform.position).normalized * 
				                  (100/(1+(transform.position - col.transform.position).magnitude)))/(2f/DamageAmount)/2;
			else
				CollisionForce = (CollisionForce+((transform.position - col.transform.position).normalized * 
				                                  (100/(1+(transform.position - col.transform.position).magnitude)))/(2f/DamageAmount))/2;
			
			GetComponent<Rigidbody2D>().AddForce((Vector2)CollisionForce*600);
			
			DamageAmount=DamageAmount*1.05f;  
			
		}

		if(col.gameObject.tag == "BlackHole")
		{
//			foreach (ContactPoint2D contact in col.contacts) {
//				Vector3 pos = contact.point;
//				Instantiate(bHoleDamage,pos, Quaternion.identity);
//			}
			DestroySelf();
         	Debug.Log("End Game");
    

			
		}
//		if(col.gameObject.tag == "FrontOfShip")
//		{
//			if (col.gameObject.transform.parent != gameObject)
//			{
//
//				CollisionForce = (col.gameObject.transform.parent.rigidbody2D.velocity)/(2f/DamageAmount) /** Mathf.Pow(1.2f,DamageAmount)* rigidbody2D.mass*/;
//				rigidbody2D.velocity += (Vector2)CollisionForce;
//				
//				DamageAmount=DamageAmount*1.05f;                  
//			}
//		}
		if(col.gameObject.tag == "Player")
		{
			//Debug.Log(col.transform.position.normalized);
			//rigidbody2D.AddForce(((col.transform.position).normalized) * Mathf.Pow(2,10/(LifeTotal+1))* rigidbody2D.mass);
			//col.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.position.normalized * Mathf.Pow(2,10/(col.gameObject.GetComponent<PlayerScript>().LifeTotal + 1))* rigidbody2D.mass);

			//audio.PlayOneShot(impact);
//			myCamera.GetComponent<CameraShakeScript>().Shake(.5f);
//			foreach (ContactPoint2D contact in col.contacts) {
//				Vector3 pos = contact.point;
//				Instantiate(explosionPrefab,pos, Quaternion.identity);
//			}
//
//			if (DamageAmount <= 0){
//				isDisabled = true;
//			}
//			//else{LifeTotal--;}
//
////			if(this.rigidbody2D.velocity.x > enemyPlayer.rigidbody2D.velocity.x)
////			{
////                if (LifeTotal <= 0){
////					isDisabled = true;
////					}
////                else{
////					LifeTotal--;}
////			}
////			if(this.rigidbody2D.velocity.y > enemyPlayer.rigidbody2D.velocity.y)
////			{
////                if (LifeTotal <= 0){
////					isDisabled = true;
////					}
////                else{
////					LifeTotal--;}
////			}
//		}
//		if(curPlayer == "Player 1" && col.gameObject.name == "Player2Mine(Clone)")
//		{
//			Destroy (col.gameObject);
//
//            if (DamageAmount <= 0){
//				isDisabled = true;}
//            else{
//				DamageAmount--;}
//			//Explosion animation
//		}
//		else if (curPlayer == "Player 2" && col.gameObject.name == "Player1Mine(Clone)")
//		{
//			Destroy (col.gameObject);
////			foreach (ContactPoint2D contact in col.contacts) {
////				Vector3 pos = contact.point;
////				Instantiate(explosionPrefab,pos, Quaternion.identity);
////			}
//            if (DamageAmount <= 0)
//                isDisabled = true;
//            else
//                DamageAmount--;
//		}
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
		}

	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Bounds")
		{
			DestroySelf();
		}
		if (col.tag == "Atmosphere")
		{
			AtmosphereEffect.SetActive(false);
		}
	}
	void DestroySelf()
	{
        //explosion sound
        SM.explosionFunction();
		SM.ThrusterFunction(false);
        SM.KnockOutFunction();
		myCamera.GetComponent<CameraShakeScript>().shakeAmount = 1.25f;
		myCamera.GetComponent<CameraShakeScript>().Shake(.2f);

		float angle = Vector3.Angle((-transform.position).normalized,Vector3.up);
		if (transform.position.x < 0)
		{
			angle = -(angle+180);
		}
		else
		{
			angle = (angle+180);
		}
		Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
		rot = Quaternion.Euler(0, 0, rot.eulerAngles.z);

		Instantiate(explosionPrefab,transform.position,rot);
		GameObject shooter = (GameObject)Instantiate(MineShooter);
		shooter.GetComponent<MineShooterScript>().MyKey = MyKey;
		shooter.GetComponent<MineShooterScript>().myColor = GetComponent<SpriteRenderer>().color;
		Destroy (gameObject);
	}
}
