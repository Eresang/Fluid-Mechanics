using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Dialog : MonoBehaviour
{
	public GridManager grid;
	public GridStageID stageID;

	public GameObject touchAni, keyAni;

	private bool GetInput ()
	{
		return (Input.GetAxis ("Horizontal") * 2 + Input.GetAxis ("Vertical") != 0f) || (Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Began);
	}

	void Start ()
	{
		Animator ani = GetComponentInChildren<Animator> ();
		if (ani != null) {
			if (Input.touchSupported) {
				Destroy (keyAni);
			} else {
				Destroy (touchAni);
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (GetInput () && grid != null && grid.GetStageID () == stageID)
			Destroy (this.gameObject);
	}
}
