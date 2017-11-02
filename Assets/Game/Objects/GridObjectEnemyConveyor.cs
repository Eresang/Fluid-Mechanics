using UnityEngine;
using System.Collections;

public class GridObjectEnemyConveyor : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Enemy)) != 0;
	}
}
