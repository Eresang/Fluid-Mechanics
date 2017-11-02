using UnityEngine;
using System.Collections;

public class ActNeighbourInternalInteract : Act
{
	public override bool OnAct ()
	{
		if (gridCell != null && gridObject != null) {
			GridCell neighbour = gridCell.GetNeighbour (direction);

			if (neighbour != null)
				neighbour.GridCellInternalInteract (gridCell.grid.GetStageID ());
		}
		return true;
	}
}
