using UnityEngine;
using System.Collections;

public class GridObjectMovable : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return ((other.gridObjectProperties & GridObjectProperties.Player) != 0);
	}
}
