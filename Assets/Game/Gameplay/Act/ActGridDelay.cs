using UnityEngine;
using System.Collections;

public class ActGridDelay : Act
{
	// Duration the move will take
	private float duration = 1.0f;

	public override void ReadArguments ()
	{
		duration = float.Parse (arguments [0]);
	}

	public override void OnTurnStart ()
	{
		if (gridCell != null && gridCell.grid != null)
			gridCell.grid.DelayGrid (duration);
	}
}
