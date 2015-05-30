using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public bool DestroyAfterAmountOfTime = false;
	public float DestroyTime = 0;
	float spawnTime = 0;
	public bool Fade = false;
	public float FadeAmountPerFrame = 0;
	public bool DestroyAfterFade = false;
	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (DestroyAfterAmountOfTime && DestroyTime != 0)
		{
			if (Time.time >= spawnTime + DestroyTime)
			{
				DestroySelf();
			}
		}
		if (Fade)
		{
			Color c = GetComponent<SpriteRenderer>().color;
			c.a -= (FadeAmountPerFrame);

			if (DestroyAfterFade)
			{
				c.a -= (DestroyTime/75);
				if (c.a <=0)
				{
					DestroySelf();
				}
			}
			GetComponent<SpriteRenderer>().color = c;
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "BlackHole")
		{
			DestroySelf();
			
		}
	}
	void DestroySelf()
	{
		Destroy (gameObject);
	}
}

