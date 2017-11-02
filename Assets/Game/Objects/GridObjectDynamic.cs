using UnityEngine;
using System.Collections;

public class GridObjectDynamic : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Enemy | GridObjectProperties.Danger | GridObjectProperties.Interactable)) != 0;
	}
}
