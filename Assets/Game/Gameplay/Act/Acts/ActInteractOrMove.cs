using UnityEngine;
using System.Collections;

public class ActInteractOrMove : Act
{
	public string[] moveAct;

	private Act activity;

	public override void ReadArguments ()
	{
		moveAct = ActBasics.GetFullArguments (arguments, 0);
	}

	public override void OnTurnStart ()
	{
		if (gridObject != null)
			gridObject.direction = direction;
	}

	public override bool OnAct ()
	{
		if (activity != null)
			return activity.DoAct ();
		else {
			GridCell neighbour = gridCell.GetNeighbour (direction);
			if (neighbour != null) {
				if (neighbour.GridObjectInteractWithCell (gridObject, direction))
					parent.forceStop = true;
				else if (neighbour.CanGridObjectShareCell (gridObject, direction))
					activity = CreateChild (moveAct, 0);
			

				if (activity) {
					activity.direction = direction;
					activity.TurnStart ();
					return false;
				}
			}

			parent.Reset ();
			return false;
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
