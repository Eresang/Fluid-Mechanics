using UnityEngine;
using System.Collections;

public class ActNewChat : Act
{
	public GridPosition position;
	public string symbol;
	public int variables;

	public override void ReadArguments ()
	{
		position.x = int.Parse (arguments [0]);
		position.z = int.Parse (arguments [1]);
		symbol = arguments [2];
		variables = int.Parse (arguments [3]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			GridCell c = gridCell.grid.GetCell (position);
			ObjectBalloon b = BalloonSymbols.GiveBalloon (c);

			if (b) {
				BalloonSymbols.SetSymbol (symbol, b);
				b.connectUp = (variables & 1) != 0;
				b.connectRight = (variables & 2) != 0;
				b.centered = (variables & 4) != 0;
				b.Orientate ();
			}
		}
	}
}
