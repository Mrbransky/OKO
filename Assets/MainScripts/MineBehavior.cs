using UnityEngine;
using System.Collections;

public class MineBehavior : MonoBehaviour {

	public GameObject MineExplosion;
	public float timer = 0;
	void Start()
	{

	}
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		
		if(timer <= 1)
		{
			this.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 300);
			float rand = Random.Range(10,30);
			GetComponent<Rigidbody2D>().AddTorque(rand);
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
