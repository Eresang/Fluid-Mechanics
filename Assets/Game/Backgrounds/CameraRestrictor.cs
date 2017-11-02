using UnityEngine;
using System.Collections;

public class CameraRestrictor : MonoBehaviour
{
	public bool bindSouth, bindNorth, bindEast, bindWest;
	private GridCamera gridCamera;

	public Transform northWest, southEast;

	void Start ()
	{
		gridCamera = Camera.main.GetComponent<GridCamera> ();
	}

	void Update ()
	{
		if (gridCamera != null && southEast != null && northWest != null) {
			gridCamera.bindSouth = bindSouth;
			gridCamera.bindNorth = bindNorth;
			gridCamera.bindEast = bindEast;
			gridCamera.bindWest = bindWest;

			gridCamera.edgeSouth = southEast.position.y;
			gridCamera.edgeNorth = northWest.position.y;
			gridCamera.edgeEast = southEast.position.x;
			gridCamera.edgeWest = northWest.position.x;
		}
	}
}
