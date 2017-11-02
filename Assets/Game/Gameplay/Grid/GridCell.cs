using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridCell
{
	private bool pHandled;

	public bool handled {
		get { return pHandled; }
		set {
			pHandled = value;
			if (!pHandled)
				for (int i = 0; i < objects.Count; i++) {
					if (objects [i] != null)
						objects [i].handled = false;
				}
		}
	}

	// Local coordinates on the grid
	private GridPosition pPosition;

	public GridPosition position {
		get { 
			return pPosition;
		}
		set { 
			pPosition = value;
		}
	}

	// Used for interpolation of positions (movement)
	private Vector3 pLocalGridPosition;

	public Vector3 localGridPosition {
		get {
			return pLocalGridPosition;
		}
		set {
			pLocalGridPosition = value;
		}
	}

	// Grid the cell lies on
	private GridManager pGrid;

	public GridManager grid {
		get { 
			return pGrid;
		}
		set { 
			pGrid = value;
			GetNeighbours ();
		}
	}

	// Local coordinates on the grid
	private ObjectBalloon pBalloon;

	public ObjectBalloon balloon {
		get { 
			return pBalloon;
		}
		set { 
			pBalloon = value;
		}
	}

	private GridCell[] neighbours = new GridCell[4];

	// All objects in the cell
	private List<GridObject> objects = new List<GridObject> ();

	// Get adjacent cell references
	private void GetNeighbours ()
	{
		if (pGrid != null) {
			for (int i = 0; i < 4; i++)
				neighbours [i] = pGrid.GridGetGridCellNeighbour (pPosition, (Direction2D)i);
		} else {
			neighbours [0] = null;
			neighbours [1] = null;
			neighbours [2] = null;
			neighbours [3] = null;
		}
	}

	// Get an adjacent cell reference
	public GridCell GetNeighbour (Direction2D direction)
	{
		return direction != Direction2D.None ? neighbours [(int)direction] : null;
	}

	public bool ContainsGridObjectType (GridObjectProperties objectType, string identity)
	{
		for (int i = 0; i < objects.Count; i++) {
			GridObject gridObject = objects [i];
			if ((gridObject.gridObjectProperties & objectType) != 0 && (gridObject.gridObjectIdentity == identity || identity == "-1"))
				return true;
		}

		return false;
	}

	public bool ContainsExactGridObjectType (GridObjectProperties objectType, string identity)
	{
		for (int i = 0; i < objects.Count; i++) {
			GridObject gridObject = objects [i];
			if (gridObject.gridObjectProperties == objectType && (gridObject.gridObjectIdentity == identity || identity == "-1"))
				return true;
		}

		return false;
	}

	public bool CanGridObjectInteractWithCell (GridObject gridObject, Direction2D fromDirection)
	{
		bool result = false;

		if (gridObject == null)
			return result;

		for (int i = 0; i < objects.Count; i++) {
			GridObject cellObject = objects [i];

			if (cellObject != null && gridObject != cellObject) {
				result = result || gridObject.CanGridObjectInteractWith (cellObject, fromDirection);
			}
		}

		return result;
	}

	public void GridCellTurnStart ()
	{
		if (!pHandled) {
			pHandled = true;
			for (int i = 0; i < objects.Count; i++)
				if (objects [i].GridObjectTurn (grid.GetStageID ())) {
					GridObject cellObject = objects [i];

					cellObject.GridObjectTurnStart ();
				}
		}
	}

	public bool GridCellAct ()
	{
		bool complete = true;

		for (int i = 0; i < objects.Count; i++) {
			GridObject cellObject = objects [i];

			//complete = ((cellObject == null) || (cellObject != null && cellObject.GridObjectTurn (grid.GetStageID ()) && cellObject.GridObjectAct ()) || (cellObject != null && !cellObject.GridObjectTurn (grid.GetStageID ()))) && complete;
			complete = ((cellObject == null) || (cellObject != null && cellObject.GridObjectAct ())) && complete;
		}

		return complete;
	}

	public void GridCellInternalInteract (GridStageID stageID)
	{
		List<GridObject> interacts = new List<GridObject> ();

		for (int i = 0; i < objects.Count; i++) {
			GridObject cellObject = objects [i];

			for (int j = i + 1; j < objects.Count; j++) {
				interacts.Add (cellObject);
				interacts.Add (objects [j]);
			}
		}

		for (int i = 0; i < interacts.Count / 2; i++) {
			GridObject a = interacts [i * 2];
			GridObject b = interacts [i * 2 + 1];

			if (a != null && b != null) {
				a.GridObjectInteractWith (b, Direction2D.None);
				b.GridObjectInteractWith (a, Direction2D.None);
			}
		}
	}

	public bool GridObjectInteractWithCell (GridObject gridObject, Direction2D fromDirection)
	{
		bool result = false;

		if (gridObject == null)
			return result;

		for (int i = 0; i < objects.Count; i++) {
			GridObject cellObject = objects [i];

			if (cellObject != null && gridObject != cellObject)
				result = result | cellObject.GridObjectInteractWith (gridObject, fromDirection) | gridObject.GridObjectInteractWith (cellObject, fromDirection);
		}

		return result;
	}

	// Can a new gridobject join this cell?
	public bool CanGridObjectShareCell (GridObject gridObject, Direction2D fromDirection)
	{
		bool result = true;

		if (gridObject == null)
			return false;

		for (int i = 0; i < objects.Count && result; i++) {
			GridObject cellObject = objects [i];
			if (cellObject != null)
				result = result && gridObject.CanGridObjectShareCellWith (cellObject, fromDirection) && (!cellObject.alwaysCollide || (cellObject.alwaysCollide && cellObject.CanGridObjectShareCellWith (gridObject, fromDirection)));
		}

		return result;
	}

	public bool GridObjectMoveToCell (GridObject toMove, GridCell toCell)
	{
		if (toCell != null) {
			objects.Remove (toMove);
			toCell.GridCellInsertGridObject (toMove, true);

			return true;
		} else
			return false;
	}

	public bool GridObjectMoveReferenceToCell (GridObject toMove, GridCell toCell)
	{
		if (toCell != null) {
			objects.Remove (toMove);
			toCell.GridCellInsertGridObject (toMove, false);

			return true;
		} else
			return false;
	}

	public void GridCellInsertGridObject (GridObject gridObject, bool Initialize)
	{
		if (gridObject != null) {
			objects.Add (gridObject);
			gridObject.gridCell = this;
			if (Initialize) {
				gridObject.localGridPosition = pLocalGridPosition;
				gridObject.GridObjectSetPosition ();
			}

			if (grid)
				gridObject.transform.parent = grid.transform;

			if (gridObject.GridObjectTurn (grid.GetStageID ()))
				gridObject.GridObjectTurnStart ();
			
			gridObject.GridObjectMove ();
		}
	}

	public void GridCellDestroyGridObject (GridObject gridObject)
	{
		objects.Remove (gridObject);
		GameObject.Destroy (gridObject.gameObject);
	}

	public void GridCellDestroyGridObject (string name)
	{
		for (int i = 0; i < objects.Count; i++) {
			GridObject gridObject = objects [i];

			if (gridObject.gridObjectIdentity == name) {
				objects.Remove (gridObject);
				GameObject.Destroy (gridObject.gameObject);
			}
		}
	}

	public void GridCellActivateGridObject (string name)
	{
		for (int i = 0; i < objects.Count; i++) {
			GridObject gridObject = objects [i];

			if (gridObject.gridObjectIdentity == name)
				gridObject.active = true;
		}
	}

	public void GridCellRemoveGridObject (GridObject gridObject)
	{
		objects.Remove (gridObject);
	}

	public void CellDestroy ()
	{
		for (int i = 0; i < objects.Count; i++)
			GameObject.Destroy (objects [i]);
	}
}
