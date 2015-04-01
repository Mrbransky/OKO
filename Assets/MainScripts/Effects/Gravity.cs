using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {



	public GameObject[] sources;
	public float force;

	// Use this for initialization
	void Start () 
	{
		sources = GameObject.FindGameObjectsWithTag("BlackHole");
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (GameObject bHole in sources)
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
			
			movement.x = dir.x + Mathf.Cos(currentAngle * Mathf.Deg2Rad)*2;
			movement.y = dir.y + Mathf.Sin(currentAngle * Mathf.Deg2Rad)*2;	
			
			//test on y
			if (rigidbody2D.velocity.y > .75f || rigidbody2D.velocity.y < -.75f )
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
			if (rigidbody2D.velocity.x > .75f || rigidbody2D.velocity.x < -.75f)
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
			
			movement = (movement) + (transform.up/1.5f) + (dir * 45/dist * 10f);
			
			//				//dir = (Quaternion.AngleAxis(90, Vector3.up) * dir)*2;
			//				//bHole.transform.position + 
			//				//orbit controls
			//Debug.Log(gameObject.name + " " + dir);
			Debug.DrawLine(transform.position,(transform.position + movement));
			Debug.DrawLine(transform.position,(transform.position + (Vector3)rigidbody2D.velocity));
			rigidbody2D.AddForce(-(movement.normalized) * force * rigidbody2D.mass);
		}
	}
}
