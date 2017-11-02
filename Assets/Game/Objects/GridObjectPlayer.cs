using UnityEngine;
using System.Collections;

public class GridObjectPlayer : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & gridObjectProperties & GridObjectProperties.PhysicalBlock) == 0;
	}

	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Enemy | GridObjectProperties.Interactable)) != 0 && (other.gridObjectProperties != 0);
	}

	void Start ()
	{
		if (gridCell != null && gridCell.grid != null)
			gridCell.grid.player = this;
	}
}
