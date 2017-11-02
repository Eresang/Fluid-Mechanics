using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GridStageID
{
	Any = -1,
	PrePlayer4 = 0,
	PrePlayer3 = 1,
	PrePlayer2 = 2,
	PrePlayer1 = 3,
	Player = 4,
	PostPlayer1 = 5,
	PostPlayer2 = 6,
	PostPlayer3 = 7,
	PostPlayer4 = 8,
	None = 9
}

public class TurnControl
{
	// Which stage is it?
	private GridStageID pStageID = GridStageID.Any;

	public GridStageID stageID
	{ get { return pStageID; } }

	// Holds the grid
	private GridManager pGrid;

	public GridManager grid
	{ get { return pGrid; } set { pGrid = value; } }

	public void Reset ()
	{
		pStageID = GridStageID.Any;
	}

	public void Update ()
	{
		if (pGrid) {
			// Let the grid do its thing
			if (pGrid.DoGridAct ()) {
				// Obtain the next stage ID
				pStageID = (GridStageID)(((int)pStageID + 1) % ((int)GridStageID.PostPlayer4 + 1));
				// Start the next turn
				//Debug.Log (pStageID);
				pGrid.DoGridTurnStart ();
			}
		}
	}
}
