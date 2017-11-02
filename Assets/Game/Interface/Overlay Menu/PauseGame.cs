using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseGame : MonoBehaviour
{
	public delegate void Pause (bool paused);

	public static Pause gamePaused;

	private float oldValue;

	void Awake ()
	{
		oldValue = Time.timeScale;
	}

	public void SetPaused (bool pause)
	{
		if (pause) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = oldValue;
		}
		if (gamePaused != null)
			gamePaused (pause);
	}

	public void Restart ()
	{
		SetPaused (false);
		SceneManager.LoadSceneAsync ("main_game_01");
	}

	public void Menu ()
	{
		SetPaused (false);
		SceneManager.LoadSceneAsync ("main_menu_01");
	}
}
