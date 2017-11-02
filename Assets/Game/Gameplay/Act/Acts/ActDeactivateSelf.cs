using UnityEngine;
using System.Collections;

public class ActDeactivateSelf : Act
{
	public override void OnTurnStart ()
	{
		if (gridObject != null)
			gridObject.active = false;
	}
}
