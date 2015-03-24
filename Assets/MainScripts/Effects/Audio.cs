using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	public GameObject otherPlayer1;
	//public GameObject otherPlayer2;

	//public AudioClip[] Ambiance;
	//public AudioClip impact;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		/*float playerDistance = Vector3.Distance (otherPlayer1.transform.position, gameObject.transform.position);
		
		if (playerDistance < 3) {
			print ("Contact!");
			audio.Play();

		} */
	}



	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.name == "Player 2") {
			print ("contact!");
			audio.Play(44100);
		}
	}
}
