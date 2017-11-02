using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class DebugGridLoadScene : MonoBehaviour
{
	public InputField inputField;

	public void TryLoadGrid ()
	{
		if (inputField == null)
			return;
		
		GridManager.filename = inputField.text;
		SceneManager.LoadSceneAsync ("main_game_01");
	}

	void Start ()
	{
		if (inputField != null) {
			inputField.text = GridManager.filename;
			EventSystem.current.SetSelectedGameObject (inputField.gameObject);
		}
	}

	/*
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Time.timeScale = 1f;
			SceneManager.LoadScene ("debug_grid_entry");
		} else if (Input.GetKeyDown (KeyCode.Return))
			TryLoadGrid ();
	}*/
}
