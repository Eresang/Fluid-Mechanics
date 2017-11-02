using UnityEngine;
using System.Collections;

public class ActAnimatePlayer : Act
{
	private string animationName;

	public override void ReadArguments ()
	{
		animationName = arguments [0];
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridCell.grid != null && gridCell.grid.player != null) {
			gridCell.grid.player.GridObjectSetState (animationName);
			Debug.Log (animationName);
		}
	}
}
