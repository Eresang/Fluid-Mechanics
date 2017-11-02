using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ActShortestPath : Act
{
	public string[] pathAct;
	private Act activeInput;
	private Vector2 lastPosition;

	public override void ReadArguments ()
	{
		pathAct = ActBasics.GetFullArguments (arguments, 0);
	}

	public override void OnReset ()
	{
		activeInput = DestroyChild (activeInput);
	}

	public override void OnTurnStart ()
	{
		if (gridCell != null && gridCell.grid != null && gridCell.grid.player != null) {
			activeInput = CreateChild (pathAct, 0);
			if (activeInput != null) {
				activeInput.direction = GridPosition.ShortestPath (gridCell.position, gridCell.grid.player.gridCell.position);

				GridCell neighbour = gridCell.GetNeighbour (activeInput.direction);
				if (neighbour != null)
					neighbour.GridCellTurnStart ();

				if (neighbour != null && !neighbour.CanGridObjectInteractWithCell (gridObject, activeInput.direction) && !neighbour.CanGridObjectShareCell (gridObject, activeInput.direction))
					activeInput.direction = GridPosition.LongerPath (gridCell.position, gridCell.grid.player.gridCell.position);

				activeInput.TurnStart ();
			}
		}
	}

	public override bool OnAct ()
	{
		if (activeInput != null)
			return activeInput.DoAct ();
		else
			return true;
	}

	void OnDestroy ()
	{
		if (activeInput != null)
			DestroyChild (activeInput);
	}
}
