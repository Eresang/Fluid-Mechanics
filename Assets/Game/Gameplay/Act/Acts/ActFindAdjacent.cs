using UnityEngine;
using System.Collections;

public class ActFindAdjacent : Act
{
	public int selfCondition;

	public override void ReadArguments ()
	{
		selfCondition = int.Parse (arguments [0]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			Direction2D result = Direction2D.None;

			for (int i = 0; i < 4; i++) {
				GridCell neighbour = gridCell.GetNeighbour ((Direction2D)i);
				if (neighbour != null && neighbour.ContainsGridObjectType ((GridObjectProperties)selfCondition, "-1")) {
					result = (Direction2D)i;
					break;
				}
			}

			if (parent != null)
				parent.direction = result;
			else
				gridObject.direction = result;
		}
	}
}
