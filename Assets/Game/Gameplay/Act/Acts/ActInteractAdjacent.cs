using UnityEngine;
using System.Collections;

public class ActInteractAdjacent : Act
{
	public override bool OnAct ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			GridCell neighbour = gridCell.GetNeighbour (direction);
			if (neighbour != null)
				neighbour.GridObjectInteractWithCell (gridObject, direction);
		}
		return true;
	}
}
