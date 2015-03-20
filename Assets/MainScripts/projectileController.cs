using UnityEngine;
using System.Collections;

public class projectileController : MonoBehaviour {
	
	public float speed;
	public float destroyTime;

	void Update () {
		transform.Translate (Vector2.up*speed*Time.deltaTime);
		destroyTime -= Time.deltaTime;
		if (destroyTime <= 0) {
			Destroy(this.gameObject);
		}
	}
}
