using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
	private TurnControl turnControl = new TurnControl ();
	private List<GridCell> cells = new List<GridCell> ();

	private int pWidth, pDepth;

	public int cellWidth = 32;
	public int cellHeight = 23;

	private float timer = 0f;

	public int width { get { return pWidth; } }

	public int depth { get { return pDepth; } }

	private string pName;

	public string gridName {
		get { 
			return pName;
		}
		set { 
			pName = value;
		}
	}

	private GridObject pPlayer;

	public GridObject player {
		get { 
			return pPlayer;
		}
		set { 
			pPlayer = value;
		}
	}

	private GridCamera pCamera;

	public GridCamera gridCamera {
		get { 
			return pCamera;
		}
		set { 
			pCamera = value;
		}
	}

	private GameObject pBackground;

	public GameObject background {
		get { 
			return pBackground;
		}
		set { 
			pBackground = value;
		}
	}

	public static string filename = "level1.grid";

	private void CreateGridMesh ()
	{
		int cellsSize = cells.Count;
		GridPosition position;

		MeshFilter meshFilter = GetComponent<MeshFilter> ();
		if (meshFilter == null)
			return;

		Mesh mesh = new Mesh ();
		Vector3[] vertices = new Vector3[0];
		Vector2[] uvs = new Vector2[0];
		int[] triangles = new int[0];

		if (pWidth * pDepth > 0) {
			float dWidth = 1.0f / pWidth;
			float dDepth = 1.0f / pDepth;

			for (int i = 0; i < cellsSize; i++)
				if (!cells [i].ContainsGridObjectType (GridObjectProperties.PhysicalBlock, "-1")) {
					position.x = i % pWidth;
					position.z = i / pWidth;

					int lastSize = vertices.Length;
					int lastTriangleSize = triangles.Length;

					Array.Resize (ref vertices, lastSize + 4);
					Array.Resize (ref uvs, lastSize + 4);
					Array.Resize (ref triangles, lastTriangleSize + 6);

					Vector3 newPosition;
					newPosition.y = -0.0f;

					newPosition.x = position.x * dWidth - 0.5f;
					newPosition.z = position.z * dDepth - 0.5f;
					vertices [lastSize] = newPosition;

					newPosition.x = (position.x + 1) * dWidth - 0.5f;
					vertices [lastSize + 1] = newPosition;

					newPosition.z = (position.z + 1) * dDepth - 0.5f;
					vertices [lastSize + 3] = newPosition;

					newPosition.x = position.x * dWidth - 0.5f;
					vertices [lastSize + 2] = newPosition;

					uvs [lastSize] = Vector2.zero;
					uvs [lastSize + 1] = Vector2.right;
					uvs [lastSize + 2] = Vector2.up;
					uvs [lastSize + 3] = Vector2.one;

					triangles [lastTriangleSize] = lastSize + 2;
					triangles [lastTriangleSize + 1] = lastSize + 1;
					triangles [lastTriangleSize + 2] = lastSize;
					triangles [lastTriangleSize + 3] = lastSize + 2;
					triangles [lastTriangleSize + 4] = lastSize + 3;
					triangles [lastTriangleSize + 5] = lastSize + 1;
				}
		}

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();

		meshFilter.sharedMesh = mesh;
	}

	private void GridDestroy ()
	{
		for (int i = 0; i < cells.Count; i++)
			cells [i].CellDestroy ();

		cells.Clear ();
	}

	public void SetGridDimensions (int Width, int Depth)
	{
		pWidth = Width;
		pDepth = Depth;

		int newSize = pWidth * pDepth;

		if (newSize < 0)
			newSize = 0;

		GridDestroy ();
		cells.Capacity = newSize;

		// Avoid division by zero and additional unnecessary work
		if (newSize == 0)
			return;

		GridCell newCell;
		GridPosition newPosition;
			
		// Per width/depth step values
		float widthStep = 1.0f / pWidth;
		float depthStep = 1.0f / pDepth;

		// Width/depth offsets
		float widthOffset = -(1.0f - widthStep) * 0.5f;
		float depthOffset = -1.0f * 0.5f;

		Vector3 relativeAdjust = Vector3.zero;

		// All cells must pre-exist before setting their references
		for (int i = 0; i < newSize; i++) {
			newCell = new GridCell ();

			newPosition.x = i % pWidth;
			relativeAdjust.x = widthOffset + newPosition.x * widthStep;
			newPosition.z = i / pWidth;
			relativeAdjust.z = depthOffset + newPosition.z * depthStep;

			newCell.position = newPosition;
			newCell.localGridPosition = relativeAdjust;

			cells.Add (newCell);
		}

		// Set cell references
		for (int i = 0; i < newSize; i++)
			cells [i].grid = this;

		CreateGridMesh ();

		transform.localScale = new Vector3 (Width, 1f, Depth);
	}

	public GridStageID GetStageID ()
	{
		return turnControl.stageID;
	}

	private bool GridValidatePosition (GridPosition position)
	{
		return (position.x >= 0) && (position.x < pWidth) && (position.z >= 0) && (position.z < pDepth);
	}

	public GridCell GridGetGridCellNeighbour (GridPosition position, Direction2D direction)
	{
		GridPosition offsetAdjusted = GridPosition.Direction (direction);

		offsetAdjusted.x += position.x;
		offsetAdjusted.z += position.z;

		return GridValidatePosition (offsetAdjusted) ? cells [offsetAdjusted.z * pWidth + offsetAdjusted.x] : null;
	}

	public void DoGridTurnStart ()
	{
		for (int i = 0; i < cells.Count; i++)
			cells [i].handled = false;

		for (int i = 0; i < cells.Count; i++)
			cells [i].GridCellInternalInteract (turnControl.stageID);

		for (int i = 0; i < cells.Count; i++)
			cells [i].GridCellTurnStart ();
	}

	public void GridAddGridObject (GameObject newObject, GridPosition position)
	{
		if (newObject != null && GridValidatePosition (position)) {
			GridObject gridObject = newObject.GetComponent<GridObject> ();
			cells [position.z * pWidth + position.x].GridCellInsertGridObject (gridObject, true);
		}
	}

	public GridCell GetCell (GridPosition position)
	{
		if (!GridValidatePosition (position))
			return null;

		return cells [position.z * pWidth + position.x];
	}

	public void GridAddExistingGridObject (GridObject gridObject, GridPosition position, Direction2D fromDirection)
	{
		if (gridObject != null) {
			int cellIndex = position.z * pWidth + position.x;
			if (GridValidatePosition (position) && cells [cellIndex].CanGridObjectShareCell (gridObject, fromDirection)) {
				cells [cellIndex].GridCellInsertGridObject (gridObject, true);
			} else
				Destroy (gridObject.gameObject);
		}
	}

	public void DelayGrid (float delay)
	{
		timer = delay;
	}

	public bool DoGridAct ()
	{
		if (timer <= 0f) {
			int finished = 0;

			for (int i = 0; i < cells.Count; i++) {
				if (cells [i].GridCellAct ())
					finished++;
			}

			return finished == cells.Count;
		} else {
			timer -= Time.deltaTime;
			return false;
		}
	}

	void OnDestroy ()
	{
		GridDestroy ();
	}

	void Update ()
	{
		turnControl.Update ();
	}

	void Awake ()
	{
	}

	void Start ()
	{
		GridReader a = new GridReader (this);
		if (!a.ReadGrid (filename))
			SceneManager.LoadSceneAsync ("main_menu_01");
		else {

			turnControl.grid = this;
			turnControl.Reset ();

			gridCamera = Camera.main.GetComponent<GridCamera> ();
		}
	}
}
