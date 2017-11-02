using UnityEngine;
using System.Collections;

public class ActSetDirectionOther : Act
{
	private int newDirection;

	public override void ReadArguments ()
	{
		newDirection = int.Parse (arguments [0]);
	}

	public override void OnTurnStart ()
	{
		if (otherObject != null) {
			otherObject.direction = (Direction2D)newDirection;

			if (parent != null)
				parent.direction = otherObject.direction;
		}
	}
}
