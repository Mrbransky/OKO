using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartTextScript : MonoBehaviour {
	public float minAlpha = .1f;
	float alpha = 1;
	public float maxAlpha = 1f;
	bool decreaseAlpha = true;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		transform.position = new Vector3(Screen.width/2,Screen.height/6);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (alpha <= minAlpha)
		{
			decreaseAlpha = false;
		}
		if (alpha >= maxAlpha)
		{
			decreaseAlpha = true;
		}
		//myColor = GetComponent<Text>().color;
		if (decreaseAlpha)
		{
			alpha -=.01f;
			GetComponent<Text>().CrossFadeAlpha(alpha,0f,false);
		}
		else
		{
			alpha +=.01f;
			GetComponent<Text>().CrossFadeAlpha(alpha,0f,false);
		}

		//GetComponent<Text>().color = myColor;
	}
}
