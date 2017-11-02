using UnityEngine;
using System.Collections;

public class ActReport : Act
{
	public string text;

	public override void ReadArguments ()
	{
		text = ActBasics.GetFullArguments (arguments, 0) [0];
	}

	public override void OnTurnStart ()
	{
		Debug.Log (text + "\n" + gridObject.name + " interaction; " + gridCell.grid.GetStageID () + " phase");
	}
}
