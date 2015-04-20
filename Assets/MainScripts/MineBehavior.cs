using UnityEngine;
using System.Collections;

public class MineBehavior : MonoBehaviour {

	public GameObject MineExplosion;
	public float timer = 0;

	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		
		if(timer <= 1)
		{
			this.rigidbody2D.AddForce(Random.insideUnitCircle * 300);
		}
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Instantiate(MineExplosion,transform.position,transform.rotation);
			DestroySelf();	
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
