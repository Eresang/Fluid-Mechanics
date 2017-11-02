using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActLoadGrid : Act
{
	private string gridName;

	public override void ReadArguments ()
	{
		gridName = arguments [0];
	}

	public override void OnTurnStart ()
	{
		if (GridSelect.Contains (gridName)) {
			if (!PlayerPrefs.HasKey (gridName))
				PlayerPrefs.SetInt (gridName, 0);
			else
				PlayerPrefs.SetInt (gridName, PlayerPrefs.GetInt (gridName) + 1);

			GridManager.filename = gridName;
			SceneManager.LoadSceneAsync ("main_win_01");
		} else {
			SceneManager.LoadSceneAsync (gridName);
		}
	}

	public override bool OnAct ()
	{
		return gridName == "";
	}
}
