using UnityEngine;
using System.Collections;

public class GridObjectDanger : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return ((other.gridObjectProperties & (GridObjectProperties.Player | GridObjectProperties.Enemy | GridObjectProperties.Interactable)) != 0) || (other.gridObjectProperties == 0);
	}
}
