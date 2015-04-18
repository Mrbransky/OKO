using UnityEngine;
using System.Collections;

public class MineBehavior : MonoBehaviour {


	public float timer = 0;

	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		
		if(timer <= 1)
		{
			this.rigidbody2D.AddForce(Random.insideUnitCircle * 300);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "BlackHole") {
			
			DestroySelf();
		}
	}
	void DestroySelf()
	{
		Destroy (gameObject);
	}

}
