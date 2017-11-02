using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GridSelect: MonoBehaviour
{
	private static Object[] grids = new Object[0];

	private static void CheckGrids ()
	{
		if (grids == null || grids.Length < 1) {
			grids = Resources.LoadAll ("Levels/");
		}
	}

	public static int Count ()
	{
		CheckGrids ();
		return grids.Length;
	}

	public static string First ()
	{
		CheckGrids ();
		return grids.Length > 1 ? grids [0].name : "";
	}

	public static bool Contains (string name)
	{
		CheckGrids ();
		for (int i = 0; i < grids.Length; i++)
			if (grids [i].name == name)
				return true;
		return false;
	}

	public static int Pages (int pageCount)
	{
		CheckGrids ();
		return Mathf.FloorToInt ((grids.Length - 1) / pageCount) + 1;
	}

	public static string[] ExportRange (int pageID, int pageCount)
	{
		CheckGrids ();
		int pageMax = Mathf.FloorToInt ((grids.Length - 1) / pageCount) + 1;
		if (pageID < pageMax) {
			pageMax = pageID * pageCount;
			string[] result = new string[Mathf.Min (pageCount, grids.Length - pageMax)];
			for (int i = 0; i < result.Length; i++)
				result [i] = grids [pageMax + i].name;
			return result;
		} else
			return new string[0];
	}

	public void TryLoadFirst ()
	{
		GridManager.filename = First ();
		SceneManager.LoadSceneAsync ("main_game_01");
	}
}
