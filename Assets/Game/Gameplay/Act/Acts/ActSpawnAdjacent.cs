using UnityEngine;
using System.Collections;

public class ActSpawnAdjacent : Act
{
	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null) {

			for (int i = 0; i < 4; i++) {
				GridCell neighbour = gridCell.GetNeighbour ((Direction2D)i);

				if (neighbour != null) {
					GridObject newObject = null;

					newObject = (Objects.GetObject (arguments [0]) as GameObject).GetComponent<GridObject> ();

					if (newObject != null)
						gridCell.grid.GridAddExistingGridObject (newObject, neighbour.position, (Direction2D)i);
				}
			}
		}
	}
}