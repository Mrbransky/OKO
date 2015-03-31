using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour {

	public List<Sprite> PlanetSprites;
	public GameObject PlanetPrefab;
	
	[Range(1,4)]
	public int NumberOfPlanets = 1;

	public float MinRadius;
	public float MaxRadius;

	// Use this for initialization
	void Start () {
		//Planets = new List<GameObject> ();
		if (NumberOfPlanets <= 1){
			Vector2 position = new Vector2 (0,0);
			GameObject spawnedPlanet = (GameObject)Instantiate (PlanetPrefab, position, Quaternion.identity);
			spawnedPlanet.GetComponent<SpriteRenderer> ().sprite = PlanetSprites [Random.Range (0, PlanetSprites.Count)];
		}
		else{
			float angle = Random.Range(0,Mathf.PI * 2);
			for (int i = 0; i < NumberOfPlanets; i++) {
				float radius = Random.Range(/*Min Radius*/MinRadius, /*MaxRadius*/MaxRadius);
				angle += Random.Range(((Mathf.PI * 2/NumberOfPlanets)-(Mathf.PI / 3/NumberOfPlanets)), ((Mathf.PI * 2/NumberOfPlanets)+(Mathf.PI / 3/NumberOfPlanets)));

				print (radius + " " + Mathf.Rad2Deg * angle);

				Vector2 position = (new Vector2 (Mathf.Cos(angle), 
				                                 Mathf.Sin(angle))) * radius;
						GameObject spawnedPlanet = (GameObject)Instantiate (PlanetPrefab, position, Quaternion.identity);
						spawnedPlanet.GetComponent<SpriteRenderer> ().sprite = PlanetSprites [Random.Range (0, PlanetSprites.Count)];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	


	}
}
