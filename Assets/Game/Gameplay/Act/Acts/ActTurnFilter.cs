using UnityEngine;
using System.Collections;

public class ActTurnFilter : Act
{
	public string[] turnAct;
	public string[] notAct;

	private Act activity;

	public override void ReadArguments ()
	{
		turnAct = ActBasics.GetFullArguments (arguments, 0);
		notAct = ActBasics.GetFullArguments (arguments, turnAct.Length);
	}

	public override void OnTurnStart ()
	{
		if (gridObject != null) {
			if (gridObject.gridCell.grid.GetStageID () == gridObject.stageID || gridObject.stageID == GridStageID.Any) {
				activity = CreateChild (turnAct, 0);
			} else {
				activity = CreateChild (notAct, 0);
			}

			if (activity != null)
				activity.TurnStart ();
		}
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
