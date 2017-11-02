using UnityEngine;
using System.Collections;

public class ActNewObject : Act
{
	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null) {

			for (int i = 0; i < (arguments.Length - 1) / 2; i++) {
				GridObject newObject = (Objects.GetObject (arguments [0]) as GameObject).GetComponent<GridObject> ();
				GridPosition position = new GridPosition (int.Parse (arguments [i * 2 + 1]), int.Parse (arguments [i * 2 + 2]));

				if (newObject != null)
					gridCell.grid.GridAddExistingGridObject (newObject, position, Direction2D.None);
			}
		}
	}
}