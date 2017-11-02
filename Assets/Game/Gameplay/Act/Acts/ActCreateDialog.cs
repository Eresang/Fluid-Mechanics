using UnityEngine;
using System.Collections;

public class ActCreateDialog : Act
{
	public override void OnTurnStart ()
	{
		GameObject prefab = Dialogs.GetDialog (arguments [0]);

		if (prefab != null) {
			prefab = Instantiate (prefab) as GameObject;
			if (Dialogs.currentGUI != null)
				prefab.transform.SetParent (Dialogs.currentGUI.transform, false);

			Dialog d = prefab.GetComponent<Dialog> ();
			d.grid = gridCell.grid;
		}
	}
}
