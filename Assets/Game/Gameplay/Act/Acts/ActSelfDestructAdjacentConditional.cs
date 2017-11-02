using UnityEngine;
using System.Collections;

public class ActSelfDestructAdjacentConditional : Act
{
	public int adjacentCondition;
	public string adjacentIdentity;

	public override void ReadArguments ()
	{
		adjacentCondition = int.Parse (arguments [0]);
		adjacentIdentity = arguments [1];
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {

			bool toDestroy = gridCell.ContainsGridObjectType ((GridObjectProperties)adjacentCondition, adjacentIdentity);

			for (int i = 0; i < 4; i++) {
				GridCell neighbour = gridCell.GetNeighbour ((Direction2D)i);
				if (neighbour != null)
					toDestroy = toDestroy || neighbour.ContainsGridObjectType ((GridObjectProperties)adjacentCondition, adjacentIdentity);
			}

			if (!toDestroy)
				Destroy (gridObject.gameObject);
		}
	}
}
