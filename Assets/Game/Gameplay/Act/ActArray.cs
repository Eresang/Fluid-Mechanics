using UnityEngine;
using System.Collections;

public class ActArray : Act
{
	private string[][] acts;
	private Act currentAct = null;
	private int actCounter = -1;

	public override void ReadArguments ()
	{
		int argumentCounter = 0;
		int argumentIndex = 0;

		while (argumentIndex < arguments.Length) {
			argumentIndex += ActBasics.CountArguments (arguments, argumentIndex) + 1;
			argumentCounter++;
		}

		acts = new string[argumentCounter][];

		argumentIndex = 0;
		for (int i = 0; i < acts.Length; i++) {
			acts [i] = ActBasics.GetFullArguments (arguments, argumentIndex);
			// No null check; force bad command strings to create problems
			argumentIndex += acts [i].Length;
		}
	}

	public override void OnReset ()
	{
		if (parent != null)
			parent.Reset ();
		else
			OnDestroy ();
	}

	private void NewAct ()
	{
		if (currentAct != null)
			DestroyChild (currentAct);

		while (actCounter < acts.Length - 1 && (currentAct == null || (currentAct != null && currentAct.DoAct ()))) {

			if (currentAct != null)
				DestroyChild (currentAct);

			actCounter++;

			currentAct = CreateChild (acts [actCounter], 0);

			if (currentAct != null)
				currentAct.TurnStart ();
		}
	}

	public override void OnTurnStart ()
	{
		NewAct ();
	}

	public override bool OnAct ()
	{
		if (currentAct == null) {

			return true;
		} else {
			if (currentAct.DoAct ())
				NewAct ();

			return false;
		}
	}

	void OnDestroy ()
	{
		if (currentAct != null)
			DestroyChild (currentAct);
	}
}
