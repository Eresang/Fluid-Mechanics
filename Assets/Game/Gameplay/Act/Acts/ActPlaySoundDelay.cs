using UnityEngine;
using System.Collections;

public class ActPlaySoundDelay : Act
{
	public string text;
	public float delay;

	public override void ReadArguments ()
	{
		text = arguments [0];
		delay = Random.Range (0f, float.Parse (arguments [1]));
	}

	public override void OnTurnStart ()
	{
		AudioCollection.PlayEffect (text, 0f);
	}
}
