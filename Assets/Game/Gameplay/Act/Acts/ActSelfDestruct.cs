using UnityEngine;
using System.Collections;

public class ActSelfDestruct : Act
{
	public override bool OnAct ()
	{
		if (gridObject != null)
			Destroy (gridObject.gameObject);
		return true;
	}
}
