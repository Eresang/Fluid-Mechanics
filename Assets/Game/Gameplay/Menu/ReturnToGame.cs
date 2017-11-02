using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToGame : MonoBehaviour
{
	public bool lastLevel = false;

	public void DoContinue ()
	{
		if (lastLevel)
			SceneManager.LoadScene ("main_menu_01");
		else
			SceneManager.LoadScene ("main_game_01");
	}
}
