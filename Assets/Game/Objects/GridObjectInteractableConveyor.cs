using UnityEngine;
using System.Collections;

public class GridObjectInteractableConveyor : GridObject
{
	public override bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return (other.gridObjectProperties & (GridObjectProperties.Interactable | GridObjectProperties.Dynamic)) != 0;
	}
}
