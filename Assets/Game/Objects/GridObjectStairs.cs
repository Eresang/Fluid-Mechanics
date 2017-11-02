using UnityEngine;
using System.Collections;

public class GridObjectStairs : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return !(fromDirection == Direction2D.Up || fromDirection == Direction2D.Down);
	}

	void Start ()
	{
		if (gridCell != null) {
			Vector3 v = gridCell.localGridPosition;
			v.z -= (1f / gridCell.grid.depth) * -(Mathf.Floor (gridCell.grid.cellHeight / 2f) / (float)gridCell.grid.cellHeight);
			gridCell.localGridPosition = v;
		}
	}
}
