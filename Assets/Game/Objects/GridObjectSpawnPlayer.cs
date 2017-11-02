using UnityEngine;
using System.Collections;

public class GridObjectSpawnPlayer : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return other.CanGridObjectShareCellWith (this, fromDirection);
	}

	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Enemy | GridObjectProperties.Interactable | GridObjectProperties.Trigger)) != 0 && (other.gridObjectProperties != 0);
	}

	void Start ()
	{
		if (gridCell != null && gridCell.grid != null)
			gridCell.grid.player = this;
	}
}
