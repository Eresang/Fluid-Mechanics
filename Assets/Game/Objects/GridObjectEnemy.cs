using UnityEngine;
using System.Collections;

public class GridObjectEnemy : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectIdentity != gridObjectIdentity) && ((other.gridObjectProperties & (GridObjectProperties.PhysicalBlock)) == 0);
	}

	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Player)) != 0;
	}
}
