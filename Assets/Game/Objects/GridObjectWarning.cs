using UnityEngine;
using System.Collections;

public class GridObjectWarning : GridObject
{
	public override bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties != GridObjectProperties.PhysicalBlock && (other.gridObjectProperties & GridObjectProperties.Danger) == 0) && (other.gridObjectIdentity != gridObjectIdentity);
	}
}
