using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	

		
	}
	void OnMouseDown()
	{
		if(this.gameObject.name == "Play" && Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(1);
		}
		if(this.gameObject.name == "Instructions" && Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(2);
		}
		if(this.gameObject.name == "Quit" && Input.GetMouseButtonDown(0))
		{
			Application.Quit();
		}
		if(this.gameObject.name == "Back" && Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(0);
		}
	}
	void OnMouseExit()
	{
		transform.localScale = new Vector2(1F,1F);
	}
}
