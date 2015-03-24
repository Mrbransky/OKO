using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour {

	public List<Sprite> PlanetSprites;
	public GameObject PlanetPrefab;

	[Range(1,4)]
	public int NumberOfPlanets = 1;

	// Use this for initialization
	void Start () {
		//Planets = new List<GameObject> ();
		for (int i = 0; i < NumberOfPlanets; i++) {
						Vector2 position = new Vector2 (Random.Range (-100, 100), Random.Range (-100, 100));
						GameObject spawnedPlanet = (GameObject)Instantiate (PlanetPrefab, position, Quaternion.identity);
						spawnedPlanet.GetComponent<SpriteRenderer> ().sprite = PlanetSprites [Random.Range (0, PlanetSprites.Count)];
				}
	}
	
	// Update is called once per frame
	void Update () {
	


	}
}
