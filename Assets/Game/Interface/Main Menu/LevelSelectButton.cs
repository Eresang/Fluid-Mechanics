using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectButton : MonoBehaviour
{
	private Text levelName;
	private Button button;
	private string gridName;

	void Awake ()
	{
		levelName = GetComponentInChildren<Text> ();
		button = GetComponent<Button> ();
	}

	public void SetVisible (bool visible)
	{
		this.gameObject.SetActive (visible);
	}

	public void SetName (string name)
	{
		gridName = name;
		if (levelName != null)
			levelName.text = name.Replace (".grid", "").Replace ("level", "Level ");

		if (button != null)
			button.interactable = PlayerPrefs.HasKey (name) || (name == GridSelect.First ());

	}

	public void Load ()
	{
		GridManager.filename = gridName;
		SceneManager.LoadSceneAsync ("main_game_01");
	}
}
