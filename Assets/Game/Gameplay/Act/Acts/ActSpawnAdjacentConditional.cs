using UnityEngine;
using System.Collections;

public class ActSpawnAdjacentConditional : Act
{
	public int selfCondition;
	public int adjacentCondition;

	public override void ReadArguments ()
	{
		selfCondition = int.Parse (arguments [0]);
		adjacentCondition = int.Parse (arguments [1]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridCell.ContainsGridObjectType ((GridObjectProperties)selfCondition, "-1")) {
			
			for (int i = 0; i < 4; i++) {
				GridCell neighbour = gridCell.GetNeighbour ((Direction2D)i);
				if (neighbour != null && neighbour.ContainsGridObjectType ((GridObjectProperties)adjacentCondition, "-1")) {
					GridObject newObject = null;

					newObject = (Objects.GetObject (arguments [2])).GetComponent<GridObject> ();

					if (newObject != null)
						gridCell.grid.GridAddExistingGridObject (newObject, neighbour.position, (Direction2D)i);
				}
			}
		}
	}
}