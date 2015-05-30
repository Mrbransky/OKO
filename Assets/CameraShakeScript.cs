using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour {
	
	Camera myCamera;
	float shake = 0;
	float decreaseShakeAmount = 1;
	public float shakeAmount;
	float timespeed = .1f;
	// Use this for initialization
	void Start () {
		myCamera = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	public void Shake () {
	 	if (shake > 0)
		{
			Vector3 pos = myCamera.transform.position;
			pos.x += Random.Range(-shakeAmount,shakeAmount);
			pos.y += Random.Range(-shakeAmount,shakeAmount);
			pos.z = -10;
			myCamera.transform.position = pos;
			shake -=Time.deltaTime * decreaseShakeAmount;
			Time.timeScale = .75f;
		}
		else
		{
			shake = 0;
			if (Time.timeScale < 1 && Time.timeScale > 0)
			{
				Time.timeScale = Time.timeScale + timespeed;
			}
			if (Time.timeScale > 1)
			{
				Time.timeScale = 1;
			}
		}
	}
	public void Shake(float duration)
	{
		shake = duration;
	}
}
