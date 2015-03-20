using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
	public GameObject Tile;
	public int tileNumberHeight;
	public int tileNumberWidth;
	// Use this for initialization
	void Start () {
		float x = -(tileNumberWidth * Tile.GetComponent<SpriteRenderer>().sprite.texture.width*Tile.transform.localScale.x/100)/2;
		float y = -(tileNumberHeight * Tile.GetComponent<SpriteRenderer>().sprite.texture.height*Tile.transform.localScale.y/100)/2;
		int xNum = 0;
		int yNum = 0;

		while (yNum < tileNumberHeight)
		{
			while (xNum < tileNumberWidth)
			{
				GameObject temp = (GameObject) Instantiate(Tile,new Vector3(x,y,0),transform.rotation);
				temp.transform.parent = gameObject.transform;
				x += Tile.GetComponent<SpriteRenderer>().sprite.texture.width*Tile.transform.localScale.x/100;
				xNum++;
			}
			xNum = 0;
			x = -(tileNumberWidth * Tile.GetComponent<SpriteRenderer>().sprite.texture.width*Tile.transform.localScale.x/100)/2;
			y += Tile.GetComponent<SpriteRenderer>().sprite.texture.height*Tile.transform.localScale.y/100;
			yNum++;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
