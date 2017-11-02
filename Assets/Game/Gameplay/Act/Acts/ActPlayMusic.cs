using UnityEngine;
using System.Collections;

public class ActPlayMusic : Act
{
	public string text;

	public override void ReadArguments ()
	{
		text = ActBasics.GetFullArguments (arguments, 0) [0];
	}

	public override void OnTurnStart ()
	{
		AudioCollection.PlayMusic (text);
	}
}
