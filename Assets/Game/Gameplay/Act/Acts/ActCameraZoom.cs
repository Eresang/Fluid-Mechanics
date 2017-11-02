using UnityEngine;
using System.Collections;

public class ActCameraZoom : Act
{
	public float zoom;
	public float speed;

	public override void ReadArguments ()
	{
		zoom = float.Parse (arguments [0]);
		speed = float.Parse (arguments [1]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			gridCell.grid.gridCamera.targetSize = zoom;
			gridCell.grid.gridCamera.zoomSpeed = speed;
		}
	}
}
