using UnityEngine;
using System.Collections;

public class ActSpawnLocal : Act
{
	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null) {

			GridObject newObject = (Objects.GetObject (arguments [0]) as GameObject).GetComponent<GridObject> ();

			if (newObject != null)
				gridCell.grid.GridAddExistingGridObject (newObject, gridCell.position, Direction2D.None);
		}
	}
}