using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ActSwipeInput : Act
{
	public string[] swipeAct;
	private Act activeInput;
	private bool isSwiping = false;
	private Vector2 lastPosition;

	public override void ReadArguments ()
	{
		swipeAct = ActBasics.GetFullArguments (arguments, 0);
	}

	private Direction2D GetSwipeA ()
	{
		//if (EventSystem.current.IsPointerOverGameObject ())
		//return Direction2D.None;

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D))
			return Direction2D.Right;
		else if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A))
			return Direction2D.Left;
		else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W))
			return Direction2D.Up;
		else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S))
			return Direction2D.Down;

		return Direction2D.None;
	}

	private Direction2D GetSwipeB ()
	{
		if (Input.touchCount <= 0)
			return Direction2D.None;

		Touch touch = Input.GetTouch (0);
		if (EventSystem.current.IsPointerOverGameObject (touch.fingerId) || EventSystem.current.currentSelectedGameObject != null)
			return Direction2D.None;

		// Replace 0.0f with a definable public variable?
		if (touch.deltaPosition.sqrMagnitude != 0.0f) {
			// First touch
			if (!isSwiping) {
				lastPosition = touch.position;
				isSwiping = true;
			} else {
				// Maybe have a built-in delay and/or minimum distance condition before a swipe is registered?
				Vector2 direction = touch.position - lastPosition;

				if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
					if (direction.x > 0.0f)
						return Direction2D.Right;
					else
						return Direction2D.Left;
				} else {
					if (direction.y > 0.0f)
						return Direction2D.Up;
					else
						return Direction2D.Down;
				}
			}
		} else
			isSwiping = false;

		return Direction2D.None;
	}

	public override void OnReset ()
	{
		isSwiping = false;
		activeInput = DestroyChild (activeInput);
	}

	public override bool OnAct ()
	{
		if (activeInput != null) {
			return forceStop || activeInput.DoAct ();
		} else {
			Direction2D swipeA = GetSwipeA ();
			Direction2D swipeB = GetSwipeB ();

			if (swipeA != Direction2D.None && swipeAct != null) {
				activeInput = CreateChild (swipeAct, 0);

				if (activeInput != null) {
					activeInput.direction = swipeA;
					activeInput.TurnStart ();
				}
			} else if (swipeB != Direction2D.None && swipeAct != null) {
				activeInput = CreateChild (swipeAct, 0);

				if (activeInput != null) {
					activeInput.direction = swipeB;
					activeInput.TurnStart ();
				}
			}
			return false;
		}
	}

	void OnDestroy ()
	{
		if (activeInput != null)
			DestroyChild (activeInput);
	}
}
