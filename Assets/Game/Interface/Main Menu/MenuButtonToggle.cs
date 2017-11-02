using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonToggle : MonoBehaviour
{
	private Toggle toggle;

	public string register;

	bool IntToBool (int input)
	{
		if (input == 0)
			return false;
		return true;
	}

	int BoolToInt (bool input)
	{
		if (input)
			return 1;
		return 0;
	}

	void SetVolume ()
	{
		switch (register) {
		case "music":
			AudioCollection.musicOnOrOff = toggle.isOn;
			AudioCollection.SetMusicVolume (toggle.isOn);
			break;

		case "sound":
			AudioCollection.effectOnOrOff = toggle.isOn;
			AudioCollection.SetEffectVolume (toggle.isOn);
			break;
		}
	}

	// Use this for initialization
	void Awake ()
	{
		toggle = GetComponent<Toggle> ();

		// Set toggle.isOn to value register by name register
		if (toggle != null && PlayerPrefs.HasKey (register)) {
			toggle.isOn = IntToBool (PlayerPrefs.GetInt (register));

			SetVolume ();
		}
	}

	public void Toggle ()
	{
		if (toggle != null)
			toggle.isOn = !toggle.isOn;
	}

	public void Changed ()
	{
		// Set value register by name register to toggle.isOn
		if (toggle != null) {
			PlayerPrefs.SetInt (register, BoolToInt (toggle.isOn));

			SetVolume ();
		}
	}
}
