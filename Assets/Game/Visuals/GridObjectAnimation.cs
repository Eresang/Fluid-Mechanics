using UnityEngine;
using System.Collections;

public enum AnimationState
{
	idle = 0,
	running = 1,
	fighting = 2,
	speaking = 3,
	interacting = 4,
	death = 5,
	special01 = 6,
	special02 = 7
}

public class GridObjectAnimation : MonoBehaviour
{
	// Default state names
	public string[] stateNames = new string[8] {
		"idle",
		"moving",
		"fighting",
		"speaking",
		"interacting",
		"death",
		"special01",
		"special02"
	};

	private Animator animator;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		if (animator == null)
			Destroy (this);
		else {
			animator.logWarnings = false;
			animator.SetTrigger ("Change");
		}
	}

	void Start ()
	{
		if (transform.parent != null) {
			GridObject g = transform.parent.GetComponent<GridObject> ();
			if (g != null)
				g.gridObjectAnimation = this;
		}
	}

	private int FindState (string name)
	{
		int r = -1;
		int i = 0;

		while (i < stateNames.Length && r == -1) {
			if (name == stateNames [i])
				r = i;
			i++;
		}

		return r;
	}

	public void SetDirection (Direction2D direction)
	{
		animator.SetInteger ("Direction", (int)direction);
	}

	public void SetState (string name, GridObject gObject)
	{
		int i = FindState (name);

		int o = animator.GetInteger ("AnimationID");

		if (gObject != null && name == "death")
			gObject.active = false;

		if (i != -1) {
			animator.SetInteger ("AnimationID", i);
			if (o != i || name != "death")
				animator.SetTrigger ("Change");
		}
	}
}
