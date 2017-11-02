using UnityEngine;
using System.Collections;

public class Act : MonoBehaviour
{
	public bool autoDetach = false;
	private Direction2D pDirection;

	private bool doReset = false;
	private bool detached = false;

	private GridObjectProperties pInteractProperties;

	public GridObjectProperties interactProperties {
		get {
			return pInteractProperties;
		}
		set { 
			pInteractProperties = value;
		}
	}

	private string pIdentity;

	public string identity {
		get {
			return pIdentity;
		}
		set { 
			pIdentity = value;
		}
	}

	public Direction2D direction {
		get {
			return pDirection;
		}
		set { 
			pDirection = value;
		}
	}

	private GridCell pGridCell;

	public GridCell gridCell {
		get {
			return pGridCell;
		}
		set { 
			pGridCell = value;
		}
	}

	private GridObject pGridObject;

	public GridObject gridObject {
		get {
			return pGridObject;
		}
		set { 
			pGridObject = value;
			if (pGridObject != null)
				gridCell = pGridObject.gridCell;
		}
	}

	private GridObject pOtherObject;

	public GridObject otherObject {
		get {
			return pOtherObject;
		}
		set { 
			pOtherObject = value;
		}
	}

	private Act pParent;

	public Act parent {
		get {
			return pParent;
		}
		set { 
			pParent = value;
		}
	}

	private bool pForceStop = false;

	public bool forceStop {
		get {
			return pForceStop;
		}
		set { 
			pForceStop = value;
		}
	}


	private string[] pArguments;

	public string[] arguments {
		get {
			return pArguments;
		}
		set { 
			pArguments = value;
		}
	}

	public Act CreateChild (string[] toCreate, int atIndex)
	{
		if (toCreate != null && toCreate.Length > atIndex) {
			Act newAct = ActBasics.ReadActArgument (toCreate, atIndex);
			if (newAct != null) {
				newAct.parent = this;
				newAct.direction = pDirection;
				newAct.interactProperties = pInteractProperties;
				newAct.identity = pIdentity;
				newAct.gridObject = pGridObject;
				newAct.otherObject = pOtherObject;
			}

			return newAct;
		}

		return null;
	}

	public Act DestroyChild (Act toDestroy)
	{
		if (toDestroy != null)
			Destroy (toDestroy.gameObject);

		return null;
	}

	public void TurnStart ()
	{
		OnTurnStart ();
		if (autoDetach)
			Detach ();
	}

	public virtual void OnTurnStart ()
	{
	}

	public virtual void ReadArguments ()
	{
	}

	// Returns wether act has finished (true == finished)
	public bool DoAct ()
	{
		if (doReset) {
			OnReset ();
			doReset = false;
		}

		bool finished = pForceStop || OnAct ();

		if (finished)
			OnStop ();
		
		return finished;
	}

	public virtual bool OnAct ()
	{
		return true;
	}

	public void Reset ()
	{
		doReset = true;
	}

	public virtual void OnReset ()
	{
	}

	public virtual void OnStop ()
	{
	}

	public void Detach ()
	{
		detached = true;
		if (parent != null)
			parent.DetachChild ();
		if (gridObject != null && parent == null)
			gridObject.DetachChild ();
	}

	public virtual void DetachChild ()
	{
	}

	void Update ()
	{
		if (detached) {
			bool finished = DoAct ();
			if (finished)
				Destroy (this.gameObject);
		}
	}
}
