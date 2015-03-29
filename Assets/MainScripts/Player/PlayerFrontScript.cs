﻿using UnityEngine;
using System.Collections;

public class PlayerFrontScript : MonoBehaviour {
	public GameObject Parent;
	// Use this for initialization
	void Start () {
		Parent = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(0,4.25f);
		transform.rotation = transform.parent.rotation;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player" && col.gameObject != Parent)
		{
				
			col.gameObject.GetComponent<PlayerScript>().CollisionForce = (Parent.rigidbody2D.velocity)/(2f/col.gameObject.GetComponent<PlayerScript>().DamageAmount) /** Mathf.Pow(1.2f,DamageAmount)* rigidbody2D.mass*/;
				
				foreach (ContactPoint2D contact in col.contacts) {
					//				Vector3 pos = contact.point;
					//				Instantiate(bHoleDamage,pos, Quaternion.identity);
				col.gameObject.rigidbody2D.AddForceAtPosition((Vector2)col.gameObject.GetComponent<PlayerScript>().CollisionForce*25,contact.point);
			}
				
				
			print ("HIT");
			col.gameObject.GetComponent<PlayerScript>().DamageAmount=col.gameObject.GetComponent<PlayerScript>().DamageAmount*1.05f;                  
		}
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
