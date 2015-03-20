using UnityEngine;
using System.Collections;

public class MineBehavior : MonoBehaviour {


	public float timer = 0;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		
		if(timer <= 1)
		{
			this.rigidbody2D.AddForce(Random.insideUnitCircle * 30);
		}
	}
}
