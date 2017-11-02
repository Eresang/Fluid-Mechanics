using UnityEngine;
using System.Collections;

public class ActMoveAdjacent : Act
{
	// Place to move from and to
	private Vector3 startPosition, endPosition;

	// Duration the move will take
	public float duration = 1.0f;
	// Internal timer
	private float timer = 0.0f;
	private bool allowMovement = false;

	public override void ReadArguments ()
	{
		duration = float.Parse (arguments [0]);
	}

	public override void OnTurnStart ()
	{
		// If the act has no cell to work with, force parent to stop
		if (gridCell != null && gridObject != null) {
			GridCell neighbour = gridCell.GetNeighbour (direction);

			if (neighbour != null && neighbour.CanGridObjectShareCell (gridObject, direction) && direction != Direction2D.None) {
				startPosition = gridCell.localGridPosition;
				endPosition = neighbour.localGridPosition;

				gridObject.direction = direction;

				gridCell.GridObjectMoveReferenceToCell (gridObject, neighbour);
				allowMovement = true;

				gridObject.GridObjectSetState ("moving");

			} else if (parent != null)
				parent.Reset ();
		}
	}

	public override bool OnAct ()
	{
		if (timer >= duration || !allowMovement) {

			gridObject.GridObjectSetState ("idle");

			return true;
		} else {
			timer = Mathf.Clamp (timer + Time.deltaTime, 0.0f, duration);
			float ratio = duration != 0.0f ? timer / duration : 1.0f;

			if (gridObject != null)
				gridObject.localGridPosition = Vector3.Lerp (startPosition, endPosition, ratio);

			return false;
		}
	}
}
