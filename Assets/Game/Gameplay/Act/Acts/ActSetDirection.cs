using UnityEngine;
using System.Collections;

public class ActSetDirection : Act
{
	private int newDirection;

	public override void ReadArguments ()
	{
		newDirection = int.Parse (arguments [0]);
	}

	public override void OnTurnStart ()
	{
		if (gridObject != null) {
			gridObject.direction = (Direction2D)newDirection;
			if (parent != null)
				parent.direction = gridObject.direction;
		}
	}
}
