using UnityEngine;
using System.Collections;

public class ActPlaySound : Act
{
	public string text;

	public override void ReadArguments ()
	{
		text = ActBasics.GetFullArguments (arguments, 0) [0];
	}

	public override void OnTurnStart ()
	{
		AudioCollection.PlayEffect (text, 0f);
	}
}
