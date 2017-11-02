using UnityEngine;
using System.Collections;

public class ActDelay : Act
{
	// Duration the move will take
	private float duration = 1.0f;
	// Internal timer
	public float timer = 0.0f;

	public override void ReadArguments ()
	{
		duration = float.Parse (arguments [0]);
	}

	public override bool OnAct ()
	{
		float old = timer;
		timer += Time.deltaTime;
		return old > duration;
	}
}
