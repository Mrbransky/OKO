using UnityEngine;
using System.Collections;

public class PlayerFrontScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Player")
		{
			if(otherObj.transform != transform.parent)
			{
				otherObj.rigidbody2D.AddForce(transform.parent.gameObject.rigidbody2D.velocity);
			}
		}
	}
}
