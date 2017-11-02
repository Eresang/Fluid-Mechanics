using UnityEngine;
using System.Collections;

public class GridCamera : MonoBehaviour
{
	public float defaultSpeed = 5f;
	public float speed = 1f;
	public float zoomSpeed = 1f;
	public float targetSize = 4.5f;

	public Vector3 offset, objectOffset;

	private GridObject focusObject;
	private GridCell focusCell;

	// Which camera directions to clamp to?
	public bool bindSouth, bindNorth, bindEast, bindWest;
	public float edgeSouth, edgeNorth, edgeEast, edgeWest;

	private float GetBoundSize (float hr, float vr)
	{
		float r = Camera.main.orthographicSize;

		if (bindSouth && bindNorth)
			r = Mathf.Min (r, (edgeNorth - edgeSouth) / (2f * vr));

		if (bindEast && bindWest)
			r = Mathf.Min (r, (edgeEast - edgeWest) / (2f * hr));

		return r;
	}

	private Vector3 GetClampedPosition (float hr, float vr)
	{
		Vector3 r = Camera.main.transform.position;

		float hSize = hr * Camera.main.orthographicSize;
		float vSize = vr * Camera.main.orthographicSize;

		if (bindSouth)
			r.y = Mathf.Max (r.y, edgeSouth + vSize);
		if (bindNorth)
			r.y = Mathf.Min (r.y, edgeNorth - vSize);

		if (bindEast)
			r.x = Mathf.Min (r.x, edgeEast - hSize);
		if (bindWest)
			r.x = Mathf.Max (r.x, edgeWest + hSize);

		return r;
	}

	private float RoundFactor (float input, float factor)
	{
		return Mathf.Round (input * factor) / factor;
	}

	public void SetFocusObject (GridObject newFocus, bool jump)
	{
		focusObject = newFocus;
		focusCell = null;

		if (jump && focusObject != null) {
			GridCell c = focusObject.gridCell;
			if (c != null) {
				Vector3 p = c.grid.gameObject.transform.TransformPoint (c.localGridPosition);
				p.z = -12f;
				Camera.main.transform.position = p + offset;
			}
		}

		speed = defaultSpeed;
	}

	public void SetFocusCell (GridCell newFocus, bool jump)
	{
		focusObject = null;
		focusCell = newFocus;

		if (jump && focusCell != null) {
			Vector3 p = focusCell.grid.gameObject.transform.TransformPoint (focusCell.localGridPosition);
			p.z = -12f;
			Camera.main.transform.position = p + offset;
		}
	}

	void LateUpdate ()
	{
		float hr = Mathf.Max (1f, (float)Screen.width / (float)Screen.height);
		float vr = Mathf.Max (1f, (float)Screen.height / (float)Screen.width);

		if (focusObject != null) {
			GridCell c = focusObject.gridCell;
			if (c != null) {
				Vector3 p = c.grid.gameObject.transform.TransformPoint (c.localGridPosition);
				p.z = -12f;
				Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position - objectOffset, p, Time.deltaTime * speed) + objectOffset;
			}

		} else if (focusCell != null) {
			Vector3 p = focusCell.grid.gameObject.transform.TransformPoint (focusCell.localGridPosition);
			p.z = -12f;
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position - offset, p, Time.deltaTime * speed) + offset;
		}

		Camera.main.orthographicSize = RoundFactor (Mathf.Lerp (Camera.main.orthographicSize, targetSize, Time.deltaTime * zoomSpeed), 10000f);
		Camera.main.orthographicSize = GetBoundSize (hr, vr);

		Camera.main.transform.position = GetClampedPosition (hr, vr);
	}
}
