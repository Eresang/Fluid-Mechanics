using UnityEngine;
using System.Collections;

public class ActDestroyChat : Act
{
	public GridPosition position;
	public string symbol;
	public int variables;

	public override void ReadArguments ()
	{
		position.x = int.Parse (arguments [0]);
		position.z = int.Parse (arguments [1]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			GridCell c = gridCell.grid.GetCell (position);
			BalloonSymbols.RemoveBalloon (c);
		}
	}
}
