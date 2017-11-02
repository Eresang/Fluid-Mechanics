using UnityEngine;
using System.Collections;

public class Visuals : MonoBehaviour
{
	public static GameObject GetVisual (string name)
	{
		return Resources.Load<GameObject> ("Visuals/" + name);
	}
}
