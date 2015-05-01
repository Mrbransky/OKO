using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public AudioSource audioManagerSource;
	//VOX
	public AudioClip[] Countdown;
	public AudioClip[] Knockout;
	public AudioClip KnockoutFinal;
	public AudioClip titleScreenVOX;

	//SFX
	public AudioClip explosionSound;
	public AudioClip Ambiance;
	public AudioClip thrusterSound;


	// Use this for initialization
	void Start () {
		audioManagerSource = (AudioSource)gameObject.AddComponent ("AudioSource");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void explosionFunction(){
		audioManagerSource.clip = explosionSound;
		audioManagerSource.pitch = Random.Range (-1, 1);
		audioManagerSource.PlayOneShot (explosionSound);
	}

	public void KnockOutFunction(){
		audioManagerSource.clip = Knockout [Random.Range(0, 3)];
		audioManagerSource.PlayOneShot (audioManagerSource.clip);
	}
}
