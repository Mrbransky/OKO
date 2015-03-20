using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public GameObject parent;
	public float timer = 4;
	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (parent.GetComponent<PlayerScript> ().shieldOn == true) {
				
			timer -= Time.deltaTime;


		}
		if (timer <= 0) {

			Destroy(gameObject);
			timer = 4;

		
		}
	
	}
	void OnColliderEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Mine") {
				
			Destroy (gameObject);
			Destroy(col.gameObject);
		
		}



	}
}
