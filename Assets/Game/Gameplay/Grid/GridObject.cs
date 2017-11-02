using UnityEngine;
using System.Collections;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GridObjectProperties
{
	PhysicalBlock = 1,
	Player = 2,
	Enemy = 4,
	Danger = 8,
	Trigger = 16,
	Combustible = 32,
	Interactable = 64,
	Dynamic = 128
}

public class GridObject : MonoBehaviour
{
	private bool pHandled;

	public bool handled {
		get { return pHandled; }
		set {
			pHandled = value;
		}
	}

	public GridStageID stageID;
	public bool active = true;

	public int sortingOffset;
	private SpriteRenderer render;

	public GridObjectProperties gridObjectProperties;
	public string gridObjectIdentity;

	public string activityString;
	public string interactionString;

	private string[][] activities;
	private string[][] interactions;
	private Act pAct = null;

	public Act act { get { return pAct; } set { pAct = value; } }

	private Act pInteract = null;

	public Act interact { get { return pInteract; } set { pInteract = value; } }

	private int turnCounter = 0;
	private int interactCounter = 0;
	public bool singleActivationChain;
	public int firstTurnDelay;
	public bool alwaysCollide = false;

	private Direction2D pDirection = Direction2D.Down;

	public Direction2D direction {
		get { return pDirection; }
		set {
			pDirection = value;

			if (pGridObjectAnimation != null)
				pGridObjectAnimation.SetDirection (pDirection);
		}
	}


	private GridCell pGridCell;

	public GridCell gridCell {
		get { return pGridCell; }
		set {
			pGridCell = value;
			if (pAct != null)
				pAct.gridCell = value;
		}
	}

	private GridObjectAnimation pGridObjectAnimation;

	public GridObjectAnimation gridObjectAnimation {
		get { return pGridObjectAnimation; }
		set {
			pGridObjectAnimation = value;

			if (pGridObjectAnimation != null)
				pGridObjectAnimation.SetDirection (pDirection);
		}
	}

	public void GridObjectSetState (string stateName)
	{
		if (pGridObjectAnimation != null)
			pGridObjectAnimation.SetState (stateName, this);
	}

	private Vector3 pLocalGridPosition;

	public Vector3 localGridPosition
	{ get { return pLocalGridPosition; } set { pLocalGridPosition = value; } }

	public int GridObjectTurnCount ()
	{
		return turnCounter;
	}

	public void DetachChild ()
	{
		pAct = null;
	}

	public void StopAct ()
	{
		if (pAct != null)
			Destroy (pAct.gameObject);
	}

	public void ReadArguments ()
	{
		string[] allArguments = ActBasics.SplitIntoArguments (activityString);

		int argumentCounter = 0;
		int argumentIndex = 0;

		while (argumentIndex < allArguments.Length) {
			argumentIndex += ActBasics.CountArguments (allArguments, argumentIndex) + 1;
			argumentCounter++;
		}

		activities = new string[argumentCounter][];

		argumentIndex = 0;
		for (int i = 0; i < activities.Length; i++) {
			activities [i] = ActBasics.GetFullArguments (allArguments, argumentIndex);
			// No null check; force bad command strings to create errors
			argumentIndex += activities [i].Length;
		}
	}

	public void ReadInteractions ()
	{
		string[] allArguments = ActBasics.SplitIntoArguments (interactionString);

		int argumentCounter = 0;
		int argumentIndex = 0;

		while (argumentIndex < allArguments.Length) {
			argumentIndex += ActBasics.CountArguments (allArguments, argumentIndex) + 1;
			argumentCounter++;
		}

		interactions = new string[argumentCounter][];

		argumentIndex = 0;
		for (int i = 0; i < interactions.Length; i++) {
			interactions [i] = ActBasics.GetFullArguments (allArguments, argumentIndex);
			// No null check; force bad command strings to create errors
			argumentIndex += interactions [i].Length;
		}
	}

	public void GridObjectIncreaseTurn ()
	{
		if (activities.Length > 0) {
			turnCounter++;
			if (!singleActivationChain)
				turnCounter = turnCounter % activities.Length;
			else
				turnCounter = Mathf.Min (turnCounter, activities.Length);
		}
	}

	public void GridObjectSetAct (string[] toCreate)
	{
		if (pAct != null)
			Destroy (pAct.gameObject);

		if (toCreate != null && toCreate.Length > 0) {
			pAct = ActBasics.ReadActArgument (toCreate, 0);

			if (pAct != null) {
				pAct.parent = null;
				pAct.gridObject = this;
				pAct.otherObject = null;
			}
		}
	}

	public void GridObjectSetInteract (string[] toCreate, GridObject other)
	{
		if (pInteract != null)
			Destroy (pInteract.gameObject);

		if (toCreate != null && toCreate.Length > 0) {
			pInteract = ActBasics.ReadActArgument (toCreate, 0);

			if (pInteract != null) {
				pInteract.parent = null;
				pInteract.gridObject = this;
				pInteract.otherObject = other;
			}
		}
	}

	// Used to determine movement limitations and potentially more
	public virtual bool CanGridObjectShareCellWith (GridObject other, Direction2D fromDirection)
	{
		return (other.name != name) && (((int)other.gridObjectProperties & (int)gridObjectProperties & (int)GridObjectProperties.PhysicalBlock) == 0);
	}

	public bool GridObjectTurn (GridStageID gridStageID)
	{
		return (stageID == gridStageID || stageID == GridStageID.Any) && active;
	}

	// Called when a turn starts
	public void GridObjectTurnStart ()
	{
		if (!active || pHandled)
			return;

		pHandled = true;

		if (activities == null)
			ReadArguments ();
	
		if (activities != null) {
			OnGridObjectTurnStart (turnCounter);

			if (turnCounter >= 0 && turnCounter < activities.Length) {
				GridObjectSetAct (activities [turnCounter]);
				if (pAct != null) {
					pAct.identity = gridObjectIdentity;
					pAct.interactProperties = gridObjectProperties;
					pAct.TurnStart ();
				}
			} else if (pAct != null)
				Destroy (pAct.gameObject);
		}
			
		GridObjectIncreaseTurn ();
	}

	// Called when a turn starts that this object is active in
	public virtual void OnGridObjectTurnStart (int turn)
	{
	}

	// Check if this object can interact with another
	public virtual bool CanGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		return false;
	}

	// Called when there is a possibility for interaction
	public bool GridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
		if (CanGridObjectInteractWith (other, fromDirection) && active) {
			if (interactions == null)
				ReadInteractions ();

			if (interactions != null && interactCounter >= 0 && interactCounter < interactions.Length) {
				GridObjectSetInteract (interactions [interactCounter], other);

				if (pInteract != null) {
					pInteract.direction = fromDirection;
					pInteract.identity = other.gridObjectIdentity;
					pInteract.interactProperties = other.gridObjectProperties;
					pInteract.TurnStart ();
				}

				interactCounter = Mathf.Clamp (interactCounter + 1, 0, interactions.Length - 1);
			}
	
			OnGridObjectInteractWith (other, fromDirection);
			return true;
		} else
			return false;
	}

	// Called when there must be an interaction
	public virtual void OnGridObjectInteractWith (GridObject other, Direction2D fromDirection)
	{
	}

	public void GridObjectInteractWithCell ()
	{
		if (gridCell != null)
			gridCell.GridObjectInteractWithCell (this, Direction2D.None);
	}

	// Called every frame by owners
	// Returns true if act is invalid or has completed
	public bool GridObjectAct ()
	{
		bool interactComplete = (pInteract != null && pInteract.DoAct ()) || pInteract == null;
		bool actComplete = (interactComplete && pAct != null && pAct.DoAct ()) || pAct == null;

		if (interactComplete && pInteract != null)
			Destroy (pInteract.gameObject);
		
		if (actComplete && pAct != null)
			Destroy (pAct.gameObject);

		return interactComplete && actComplete;
	}

	public void GridObjectSetPosition ()
	{
		if (pGridCell != null && pGridCell.grid != null) {
			Transform gridTransform = pGridCell.grid.transform;
			transform.position = gridTransform.TransformPoint (pLocalGridPosition);
		}
	}

	public void SetRenderer (SpriteRenderer newRender)
	{
		if (newRender != null) {
			render = newRender;
			GridObjectMove ();
		}
	}

	public void GridObjectMove ()
	{
		if ((render) && pGridCell != null)
			render.sortingOrder = 1000 - (pGridCell.position.z * 10 - sortingOffset);
	}

	void Awake ()
	{
		turnCounter = -firstTurnDelay;
		SetRenderer (GetComponentInChildren<SpriteRenderer> ());
	}

	void Update ()
	{
		GridObjectSetPosition ();
	}

	void OnDestroy ()
	{
		if (pAct != null)
			Destroy (pAct.gameObject);
		if (pInteract != null)
			Destroy (pInteract.gameObject);
		if (gridCell != null)
			gridCell.GridCellRemoveGridObject (this);
	}
}

#if UNITY_EDITOR
[CustomEditor (typeof(GridObject), true)]
public class TileNodeEditor : Editor
{
	public SerializedProperty stageID;
	public SerializedProperty active;
	public SerializedProperty sortingOffset;
	public SerializedProperty gridObjectProperties;
	public SerializedProperty gridObjectIdentity;
	public SerializedProperty activityString;
	public SerializedProperty interactionString;
	public SerializedProperty singleActivationChain;
	public SerializedProperty firstTurnDelay;
	public SerializedProperty alwaysCollide;

	void OnEnable ()
	{
		stageID = serializedObject.FindProperty ("stageID");
		active = serializedObject.FindProperty ("active");
		sortingOffset = serializedObject.FindProperty ("sortingOffset");
		gridObjectProperties = serializedObject.FindProperty ("gridObjectProperties");
		gridObjectIdentity = serializedObject.FindProperty ("gridObjectIdentity");
		activityString = serializedObject.FindProperty ("activityString");
		interactionString = serializedObject.FindProperty ("interactionString");
		singleActivationChain = serializedObject.FindProperty ("singleActivationChain");
		firstTurnDelay = serializedObject.FindProperty ("firstTurnDelay");
		alwaysCollide = serializedObject.FindProperty ("alwaysCollide");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		stageID.intValue = (int)((GridStageID)EditorGUILayout.EnumPopup ("Stage ID", (GridStageID)stageID.intValue));
		active.boolValue = EditorGUILayout.Toggle ("Active", active.boolValue);
		sortingOffset.intValue = EditorGUILayout.IntField ("Sorting Offset", sortingOffset.intValue);

		gridObjectProperties.intValue = (int)((GridObjectProperties)EditorGUILayout.EnumMaskField ("Grid Object Properties", (GridObjectProperties)gridObjectProperties.intValue));
		EditorGUILayout.TextField (gridObjectProperties.intValue.ToString ());

		gridObjectIdentity.stringValue = EditorGUILayout.TextField ("Identity", gridObjectIdentity.stringValue);
		activityString.stringValue = EditorGUILayout.TextField ("Activity String", activityString.stringValue);
		interactionString.stringValue = EditorGUILayout.TextField ("Interaction String", interactionString.stringValue);
		singleActivationChain.boolValue = EditorGUILayout.Toggle ("Single Activation", singleActivationChain.boolValue);
		firstTurnDelay.intValue = EditorGUILayout.IntField ("First Turn Delay", firstTurnDelay.intValue);
		alwaysCollide.boolValue = EditorGUILayout.Toggle ("Always Collide", alwaysCollide.boolValue);

		serializedObject.ApplyModifiedProperties ();
	}
}
#endif