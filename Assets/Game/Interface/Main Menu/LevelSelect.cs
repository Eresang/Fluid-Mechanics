using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
	private LevelSelectButton[] buttons = new LevelSelectButton[0];
	private int pages, lastPage;

	public int pageCount;

	public GameObject nextPage, previousPage;

	void SetPage (int page)
	{
		if (page < 0)
			lastPage = 0;
		else if (page > pages)
			lastPage = Mathf.Max (0, pages - 1);
		else
			lastPage = page;

		if (nextPage != null)
			nextPage.SetActive (pages != 0 && lastPage != pages - 1);

		if (previousPage != null)
			previousPage.SetActive (pages != 0 && lastPage != 0);

		string[] names = GridSelect.ExportRange (lastPage, pageCount);

		for (int i = 0; i < Mathf.Min (names.Length, buttons.Length); i++) {
			buttons [i].SetName (names [i]);
			buttons [i].SetVisible (true);
		}

		for (int i = Mathf.Min (names.Length, buttons.Length); i < buttons.Length; i++)
			buttons [i].SetVisible (false);
	}

	// Use this for initialization
	void Start ()
	{
		pages = GridSelect.Pages (pageCount);

		buttons = GetComponentsInChildren<LevelSelectButton> ();
		for (int i = 0; i < buttons.Length; i++)
			buttons [i].SetVisible (false);

		lastPage = 0;
		SetPage (lastPage);
	}

	public void NextPage ()
	{
		SetPage (lastPage + 1);
	}

	public void PreviousPage ()
	{
		SetPage (lastPage - 1);
	}
}
