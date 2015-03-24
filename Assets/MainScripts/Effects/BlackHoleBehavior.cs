using UnityEngine;
using System.Collections;

public class BlackHoleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(transform.forward * Time.deltaTime * 25);
	}
}
