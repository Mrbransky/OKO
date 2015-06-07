using UnityEngine;
using System.Collections;

public class PlayerMineScript : MonoBehaviour {
	public GameObject MineExplosion;
	// Use this for initialization
	void Start()
	{
		float rand = Random.Range(10,30);
		GetComponent<Rigidbody2D>().AddTorque(rand);
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
