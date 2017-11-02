using UnityEngine;
using System.Collections;

public class GridObjectFire : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return /*(other.gridObjectProperties != GridObjectProperties.PhysicalBlock) &&*/ (other.gridObjectIdentity != gridObjectIdentity) && (other.gridObjectIdentity != "invisible_block");
	}

	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Player | GridObjectProperties.Enemy | GridObjectProperties.Interactable)) != 0 && other.active;
	}
}
