using UnityEngine;
using System.Collections;

public class ActObjectPicker : Act
{
	private string[][] acts;
	private Act currentAct = null;

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
			// No null check; force bad command strings to create errors
			argumentIndex += acts [i].Length;
		}
	}

	public override void OnTurnStart ()
	{
		ReadArguments ();

		for (int i = 0; i < acts.Length / 3; i++) {

			if (((int)interactProperties & int.Parse (acts [i * 3] [0])) != 0 && (identity == acts [i * 3 + 1] [0] || acts [i * 3 + 1] [0] == "-1")) {
				currentAct = CreateChild (acts [i * 3 + 2], 0);

				if (currentAct != null)
					currentAct.TurnStart ();

				return;
			}
		}
	}

	public override bool OnAct ()
	{
		if (currentAct != null) {
			return currentAct.DoAct ();
		} else
			return true;
	}

	void OnDestroy ()
	{
		if (currentAct != null)
			DestroyChild (currentAct);
	}
}
