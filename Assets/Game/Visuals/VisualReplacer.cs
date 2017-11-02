using UnityEngine;
using System.Collections;

public class VisualReplacer : MonoBehaviour
{
	public GameObject visualPrefab;

	void Awake ()
	{
		if (visualPrefab != null) {
			GameObject v = Instantiate (visualPrefab) as GameObject;
			v.transform.parent = transform.parent;
			v.transform.localPosition = visualPrefab.transform.localPosition;
			GridObject g = transform.parent.GetComponent<GridObject> ();
			if (g != null) {
				g.SetRenderer (v.GetComponent<SpriteRenderer> ());
				g.gridObjectAnimation = v.GetComponent<GridObjectAnimation> ();
			}
		}

		Destroy (this.gameObject);
	}
}
