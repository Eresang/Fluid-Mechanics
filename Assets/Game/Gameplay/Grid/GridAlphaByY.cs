using UnityEngine;
using System.Collections;

public class GridAlphaByY : MonoBehaviour
{
	private GridObject gridObject;
	private SpriteRenderer gridObjectSprite;
	public float alphaMin = 0.75f;
	public float alphaMax = 1.0f;

	void Start ()
	{
		gridObject = transform.parent.GetComponent<GridObject> ();
		gridObjectSprite = GetComponent<SpriteRenderer> ();

		if (gridObject == null || gridObjectSprite == null) {
			Destroy (this);
		} else {
			gridObjectSprite.material = new Material (gridObjectSprite.material);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		GridManager grid = gridObject.gridCell.grid;

		if (grid.player != null && grid.player.gridCell != null) {
			int d = gridObject.gridCell.position.z - grid.player.gridCell.position.z;

			Color c = gridObjectSprite.material.color;
			if (d < 0 && Mathf.Abs (d * (gridObject.gridCell.position.x - grid.player.gridCell.position.x)) < 2)
				c.a = Mathf.Lerp (c.a, alphaMin, Time.deltaTime * 2f);
			else
				c.a = Mathf.Lerp (c.a, alphaMax, Time.deltaTime * 2f);

			gridObjectSprite.material.color = c;
		}
	}
}
