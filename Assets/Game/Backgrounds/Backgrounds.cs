using UnityEngine;
using System.Collections;

public class Backgrounds : MonoBehaviour
{
	public static GameObject GetBackground (string name)
	{
		return Resources.Load<GameObject> ("Backgrounds/" + name);
	}
}
