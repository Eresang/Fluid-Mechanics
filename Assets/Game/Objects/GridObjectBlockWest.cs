using UnityEngine;
using System.Collections;

public class GridObjectBlockWest : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return fromDirection != Direction2D.Right;
	}
}
