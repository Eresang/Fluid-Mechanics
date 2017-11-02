using UnityEngine;
using System.Collections;

public class ActCameraFollowPlayer : Act
{
	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			gridCell.grid.gridCamera.SetFocusObject (gridCell.grid.player, false);
		}
	}
}
