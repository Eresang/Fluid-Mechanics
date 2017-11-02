using UnityEngine;
using System.Collections;

public class ActInteractAndMove : Act
{
	public string[] moveAct;

	private Act activity;

	public override void ReadArguments ()
	{
		moveAct = ActBasics.GetFullArguments (arguments, 0);
	}

	public override void OnReset ()
	{
		activity = DestroyChild (activity);
	}

	public override void OnTurnStart ()
	{
		GridCell neighbour = gridCell.GetNeighbour (direction);
		bool interacting = false;

		if (neighbour != null) {
			if (neighbour.CanGridObjectInteractWithCell (gridObject, direction)) {
				gridObject.direction = direction;
				gridObject.GridObjectSetState ("idle");

				interacting = true;

				neighbour.GridObjectInteractWithCell (gridObject, direction);
			}

			if (neighbour.CanGridObjectShareCell (gridObject, direction))
				activity = CreateChild (moveAct, 0);

			if (activity != null)
				activity.TurnStart ();
		}

		if (activity == null && !interacting)
			parent.Reset ();
	}

	public override bool OnAct ()
	{
		if (activity != null)
			return activity.DoAct ();
		else
			return true;
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
