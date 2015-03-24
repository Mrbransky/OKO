using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {



	public GameObject source;
	public float force;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		rigidbody2D.AddForce((transform.position - source.transform.position).normalized * force * rigidbody2D.mass);
	}
}
