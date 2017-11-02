using UnityEngine;
using System.Collections;

public class ActActivateObject : Act
{
	private string activateName;

	public override void ReadArguments ()
	{
		activateName = arguments [0];
	}

	public override void OnTurnStart ()
	{
		GridManager grid = gridCell.grid;

		for (int i = 0; i < (arguments.Length - 1) / 2; i++) {
			GridCell cell = grid.GetCell (new GridPosition (int.Parse (arguments [i * 2 + 1]), int.Parse (arguments [i * 2 + 2])));
			cell.GridCellActivateGridObject (activateName);
		}
	}
}
