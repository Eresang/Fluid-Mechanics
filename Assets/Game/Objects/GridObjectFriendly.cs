using UnityEngine;
using System.Collections;

public class GridObjectFriendly : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Player | GridObjectProperties.Enemy | GridObjectProperties.Danger | GridObjectProperties.Interactable)) != 0 && active;
	}
}
