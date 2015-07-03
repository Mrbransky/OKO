using UnityEngine;
using System.Collections;

public class ShooterHardStopScript : MonoBehaviour {
	public bool Stop = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Bounds")
			Stop = true;
	}
}
