using UnityEngine;
using System.Collections;

public class Dialogs : MonoBehaviour
{
	public GameObject GUI;
	public static GameObject currentGUI;

	public static GameObject GetDialog (string name)
	{
		return Resources.Load<GameObject> ("Dialogs/" + name);
	}

	void Awake ()
	{
		currentGUI = GUI;
	}
}
