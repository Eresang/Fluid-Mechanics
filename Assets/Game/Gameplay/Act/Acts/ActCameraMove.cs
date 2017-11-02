using UnityEngine;
using System.Collections;

public class ActCameraMove : Act
{
	public GridPosition position;
	public float speed;

	public override void ReadArguments ()
	{
		position.x = int.Parse (arguments [0]);
		position.z = int.Parse (arguments [1]);
		speed = float.Parse (arguments [2]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			gridCell.grid.gridCamera.speed = speed;
			gridCell.grid.gridCamera.SetFocusCell (gridCell.grid.GetCell (position), false);
		}
	}
}
