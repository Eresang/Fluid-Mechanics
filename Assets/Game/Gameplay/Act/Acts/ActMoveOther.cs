using UnityEngine;
using System.Collections;

public class ActMoveOther : Act
{
	public string[] moveAct;

	private Act activity;

	public override void ReadArguments ()
	{
		moveAct = ActBasics.GetFullArguments (arguments, 0);
	}

	public override bool OnAct ()
	{
		if (activity != null)
			return activity.DoAct ();
		else {
			if (otherObject != null) {
				GridCell neighbour = otherObject.gridCell.GetNeighbour (direction);
				if (neighbour != null) {
					if (neighbour.CanGridObjectShareCell (otherObject, direction))
						activity = CreateChild (moveAct, 0);

					if (activity) {
						activity.gridObject = otherObject;
						activity.otherObject = null;
						activity.direction = direction;
						activity.TurnStart ();
						return false;
					}
				}
			}
			return true;
		}
	}

	public override void DetachChild ()
	{
		activity = null;
	}

	void OnDestroy ()
	{
		if (activity != null)
			DestroyChild (activity);
	}
}
