using UnityEngine;
using System.Collections;

public class ActAllNeighbourInternalInteract : Act
{
	public override bool OnAct ()
	{
		if (gridCell != null && gridObject != null) {
			for (int i = 0; i < 4; i++) {
				GridCell neighbour = gridCell.GetNeighbour ((Direction2D)i);

				if (neighbour != null)
					neighbour.GridCellInternalInteract (gridCell.grid.GetStageID ());
			}
		}
		return true;
	}
}
