using UnityEngine;
using System.Collections;

public class Objects : MonoBehaviour
{
	private static Hashtable customHash = new Hashtable ();
	private static GameObject iaPrefab, trPrefab;

	private static string[] ReadCustomObject (string gridObject)
	{
		string[] result = new string[4];

		int i = gridObject.IndexOf (' ', 0);
		if (i == -1)
			i = gridObject.Length;
		result [0] = gridObject.Substring (0, i);

		int j = Mathf.Min (i + 1, gridObject.Length);
		i = gridObject.IndexOf (' ', j);
		if (i == -1)
			i = gridObject.Length;
		result [1] = gridObject.Substring (j, i - j);

		j = Mathf.Min (i + 1, gridObject.Length);
		i = gridObject.IndexOf (' ', j);
		if (i == -1)
			i = gridObject.Length;
		result [2] = gridObject.Substring (j, i - j);

		if (i + 1 > gridObject.Length)
			i--;
		result [3] = gridObject.Substring (i + 1);

		return result;
	}

	private static GameObject MakeCustomObject (string gridObject)
	{
		string[] objectParameters = ReadCustomObject (gridObject);
		GameObject newObject = null;

		if (trPrefab == null)
			trPrefab = Resources.Load<GameObject> ("Objects/Trigger");
		if (iaPrefab == null)
			iaPrefab = Resources.Load<GameObject> ("Objects/Interactable");

		if (objectParameters [0] == "Trigger" && trPrefab != null) {
			newObject = Instantiate (trPrefab) as GameObject;
		} else if (objectParameters [0] == "Interactable" && iaPrefab != null) {
			newObject = Instantiate (iaPrefab) as GameObject;
		}

		if (newObject != null) {
			GridObject newGridObject = newObject.GetComponent<GridObject> ();
			if (newGridObject != null) {

				GameObject newVisual = Visuals.GetVisual (objectParameters [2]);
				if (newVisual != null) {
					newVisual = Instantiate (newVisual) as GameObject;
					newVisual.transform.parent = newObject.transform;
					newGridObject.SetRenderer (newVisual.GetComponentInChildren<SpriteRenderer> ());
				}
					
				newGridObject.interactionString = objectParameters [3];
				newGridObject.gridObjectIdentity = objectParameters [1];
			}
		}

		return newObject;
	}

	public static void AddStringToHash (string gridObject)
	{
		string name = ReadCustomObject (gridObject) [1];
		customHash.Add (name, gridObject);
	}

	public static void ClearCustomHash ()
	{
		customHash.Clear ();
	}

	public static GameObject GetObject (string name)
	{
		GameObject o = Resources.Load<GameObject> ("Objects/" + name);

		if (o != null) {
			o = Instantiate (o) as GameObject;
			GridObject g = o.GetComponent<GridObject> ();
			if (g != null)
				g.gridObjectIdentity = name.Replace ("s_", "");
			return o;
		} else if (customHash.ContainsKey (name))
			return MakeCustomObject (customHash [name] as string);
		else
			return null;
	}
}
