using UnityEngine;
using System.Collections;

public class ActAnimate : Act
{
	private string animationName;

	private GridObjectAnimation goa;

	public override void ReadArguments ()
	{
		animationName = arguments [0];
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridObject != null) {
			gridObject.GridObjectSetState (animationName);
		}
	}
}
