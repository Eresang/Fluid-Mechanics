using UnityEngine;
using System.Collections;

public class ActDestroyAll : Act
{
	private string destroyName;

	public override void ReadArguments ()
	{
		destroyName = arguments [0];
	}

	public override void OnTurnStart ()
	{
		GridManager grid = gridCell.grid;

		for (int i = 0; i < grid.depth; i++)
			for (int j = 0; j < grid.width; j++) {
				GridCell cell = grid.GetCell (new GridPosition (j, i));
				cell.GridCellDestroyGridObject (destroyName);
			}
	}
}
