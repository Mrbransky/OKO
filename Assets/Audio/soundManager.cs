using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {

	public AudioSource audioManagerSource;
	public AudioSource thrusterSource;
	public AudioSource explosionSource;
	public AudioSource announcerSource;
	public AudioSource musicSource;
	public AudioSource ambianceSource;
	//VOX
	public AudioClip[] Countdown;
	public AudioClip[] Knockout;
	public AudioClip KnockoutFinal;
	public AudioClip titleScreenVOX;

	//SFX
	public AudioClip explosionSound;
	public AudioClip AmbianceClip;
	public AudioClip thrusterSound;

	public AudioClip musicClip;

	private bool musicGate = true;

	void Awake(){
		//DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		audioManagerSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		explosionSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		announcerSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		thrusterSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		musicSource = (AudioSource)gameObject.AddComponent <AudioSource>();
		ambianceSource = (AudioSource)gameObject.AddComponent <AudioSource>();

		thrusterSource.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
		//testVoid ();

		//musicFunction ();

		if (gameObject.name == "SoundManager(Clone)" /*&& Application.loadedLevel == 2*/ && musicGate == true) {
			audioManagerSource.clip = musicClip;
			audioManagerSource.Play ();
			audioManagerSource.volume = 0.5f;
			audioManagerSource.loop = true;
			musicGate = false;
			Debug.Log ("PlayingMusic");
		}
		if (Application.loadedLevel != 2) {
			//audioManagerSource.Stop();
		}

	}

	public void explosionFunction(){
		explosionSource.clip = explosionSound;
		//explosionSource.pitch = Random.Range (-0.5F, 0.5F);
		explosionSource.PlayOneShot (explosionSound, 0.5f);
	}

	public void KnockOutFunction(){
			announcerSource.clip = Knockout [Random.Range (0, 3)];
			announcerSource.PlayOneShot (announcerSource.clip);
	}

	public void ThrusterFunction(bool isThrustOn = false){
		if (isThrustOn == true) {
			if (!thrusterSource.isPlaying) {
				thrusterSource.clip = thrusterSound;
				thrusterSource.volume = 0.2F;
				thrusterSource.Play ();
				thrusterSource.loop = true;
			}
		} else {
			thrusterSource.Pause ();
		}
	}
	
	public void musicFunction(/*bool isMusic = false*/){
		if (gameObject.name == "SoundManager" && Application.loadedLevel == 2) {


		} else {
			musicGate = false;
		}
		if (musicGate == true) {
			musicSource.clip = musicClip;
			musicSource.Play ();
			musicSource.loop = true;
			//calling code for ambiance sound
			ambianceFunction(true);

		} else {
			musicSource.Stop();
			//calling code for ambiance sound
			ambianceFunction(false);
		}
	}

	public void ambianceFunction(bool isAmbiance){
		if (isAmbiance == true) {
			ambianceSource.clip = AmbianceClip;
			ambianceSource.Play ();
			ambianceSource.loop = true;
		} else {
			ambianceSource.Stop();
		}
	}

	public void countdownFunction(int countdownInt){
		bool isThree = true;
		bool isTwo = true;
		bool isOne = true;
		bool isGo = true;

		switch (countdownInt) {
			case 3:
				if(isThree == true){
					announcerSource.clip = Countdown[3];
					announcerSource.PlayOneShot(Countdown[3], 2.0F);
					isThree = false;
				}
				break;
			case 2:
				if(isTwo == true){
					announcerSource.clip = Countdown[2];
					announcerSource.PlayOneShot(Countdown[2], 2.0F);
					isTwo = false;
				}
				break;
			case 1:
				if(isOne == true){
					announcerSource.clip = Countdown[1];
					announcerSource.PlayOneShot(Countdown[1], 2.0F);
					isOne = false;
				}
				break;
			case 0:
				if(isGo == true){
					announcerSource.clip = Countdown[0];
					announcerSource.PlayOneShot(Countdown[0], 2.0F);
					isGo = false;
				}
				break;
		}

		//announcerSource.PlayOneShot(announcerSource.clip, 2.0F);
		
	}

	public void titleScreen(){
			announcerSource.clip = titleScreenVOX;
			announcerSource.PlayOneShot(announcerSource.clip, 2.0F);
	}

	/*public void testVoid(){

		//Ship Explosion/"Knockout!"
		if (Input.GetKeyDown ("h")) {
			KnockOutFunction();
			explosionFunction();
		}

		//Title Screen Announcer
		if (Input.GetKeyDown ("d")) {
			titleScreen();
		}

		//Thruster
		if (Input.GetKey ("f")) {
			ThrusterFunction(true);
		}
		if (Input.GetKeyUp ("f")) {
			ThrusterFunction(false);
		}

		//Countdown
		if (Input.GetKeyDown ("3")) {
			countdownFunction (3);
		} else if (Input.GetKeyDown ("2")) {
			countdownFunction(2);
		} else if (Input.GetKeyDown ("1")) {
			countdownFunction(1);
		} else if (Input.GetKeyDown ("0")) {
			countdownFunction(0);
		}

		//Music
		if(Input.GetKeyDown("z")){
			musicFunction(true);
		}
		if (Input.GetKeyDown ("x")) {
			musicFunction(false);
		}
	}*/
}
