using UnityEngine;
using System.Collections;

public class ShooterChargeMeter : MonoBehaviour {
	Color color;
	float rechargeTime = 0;
	float shotRechargeDuration;
	float maxForce;
	float force = 0;
	public Sprite[] chargeSprites = new Sprite[32];
	public Sprite[] overchargeSprites = new Sprite[18];
	// Use this for initialization
	void Start () {
		shotRechargeDuration = transform.parent.GetComponent<MineShooterScript>().ShotRechargeDuration;
		maxForce = transform.parent.GetComponent<MineShooterScript>().MaxShotForce;
		color = transform.parent.GetComponent<MineShooterScript>().myColor;
		GetComponent<SpriteRenderer>().color = color;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rechargeTime = transform.parent.GetComponent<MineShooterScript>().RechargeTime();
		force = transform.parent.GetComponent<MineShooterScript>().ShotForce();
		if(rechargeTime > shotRechargeDuration){
			if (force == 0){
				GetComponent<SpriteRenderer>().sprite = chargeSprites[31];
			}
			else if(force >= maxForce){
				GetComponent<SpriteRenderer>().sprite = overchargeSprites[17];
			}
			else{
				for(int i = 1; i < 19; i++)
				{
					if(force <= i * maxForce /18){
						GetComponent<SpriteRenderer>().sprite = overchargeSprites[i-1];
						break;
					}
					else if (i==18)
					{
						if(force >= maxForce){
							GetComponent<SpriteRenderer>().sprite = overchargeSprites[i-1];
						}
					}
				}
			}
				//GetComponent<SpriteRenderer>().sprite = sprites[i-1];
		}
		else
		{
			for(int i = 1; i < 33; i++)
			{
				if(rechargeTime <= i * shotRechargeDuration /32){
					GetComponent<SpriteRenderer>().sprite = chargeSprites[i-1];
					break;
				}
				else if (i==32)
				{
					if(rechargeTime > shotRechargeDuration){
						GetComponent<SpriteRenderer>().sprite = chargeSprites[i-1];
					}
				}
			}
		}
	}
//	public float RechargeTime()
//	{
//		rechargeTime = transform.parent.GetComponent<MineShooterScript>().RechargeTime();
//		return rechargeTime;
//	}
}
