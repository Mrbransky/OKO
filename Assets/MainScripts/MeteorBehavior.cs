using UnityEngine;
using System.Collections;

public class MeteorBehavior : MonoBehaviour {


	public float timer = 0;
	public GameObject meteor;
	public GameObject meteorShards;
	public GameObject powerUp;
	public int randNum;
	bool spawnPowerUp;
	// Use this for initialization
	void Start () 
	{
		randNum = Random.Range (1,3);
	}
	
	// Update is called once per frame
	void Update () 
	{

		timer += Time.deltaTime;

		if(timer <= 3)
		{
			this.rigidbody2D.AddForce(Random.insideUnitCircle * 100);
		}
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Vector2 meteorPosition = (meteor.transform.position);
			Instantiate(meteorShards, meteorPosition, Quaternion.identity);
			Instantiate(meteorShards, meteorPosition, Quaternion.identity);
			Instantiate(meteorShards, meteorPosition, Quaternion.identity);

			if(randNum == 2000)
			{
			Instantiate(powerUp, meteorPosition, Quaternion.identity);
			}
			else
			{

			}
			Destroy(this.gameObject);

		}

	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "BlackHole") {
			
			Destroy (gameObject);
		}
	}
}
