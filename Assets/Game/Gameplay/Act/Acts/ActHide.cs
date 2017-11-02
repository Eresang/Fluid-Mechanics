using UnityEngine;
using System.Collections;

public class ActHide : Act
{
	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridObject != null) {
			SpriteRenderer s = gridObject.GetComponentInChildren<SpriteRenderer> ();
			if (s != null)
				Destroy (s.gameObject);
		}
	}
}
