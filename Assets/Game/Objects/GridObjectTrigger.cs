using UnityEngine;
using System.Collections;

public class GridObjectTrigger : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Player | GridObjectProperties.Enemy | GridObjectProperties.Interactable | GridObjectProperties.Dynamic)) != 0;
	}
}
