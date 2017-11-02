using UnityEngine;
using System.Collections;

public class AudioCollection : MonoBehaviour
{
	public delegate void EffectVolume (bool onOrOff);

	public static EffectVolume effectVolume;

	private static GameObject effectObject;
	private static AudioSource musicSource;
	public GameObject audioObject;

	public static bool effectOnOrOff;
	public static bool musicOnOrOff;


	public static void SetEffectVolume (bool onOrOff)
	{
		effectOnOrOff = onOrOff;

		if (effectVolume != null)
			effectVolume (onOrOff);
	}

	public static void SetMusicVolume (bool onOrOff)
	{
		musicOnOrOff = onOrOff;

		if (musicSource != null) {
			if (onOrOff) {
				musicSource.volume = 100f;
			} else {
				musicSource.volume = 0f;
			}
		}
	}


	public static AudioClip GetAudio (string name)
	{
		AudioClip a = Resources.Load<AudioClip> ("Audio/Sounds/" + name);
		if (a == null) {
			a = Resources.Load<AudioClip> ("Audio/Music/" + name);
		}
		return a;
	}

	public static void PlayEffect (string name, float delay)
	{
		if (effectObject != null) {
			GameObject n = Instantiate (effectObject) as GameObject;
			AudioEffect a = n.GetComponent<AudioEffect> ();

			if (a != null) {
				a.audioName = name;
				a.delay = delay;
			}
		}
	}

	public static void PlayMusic (string name)
	{
		AudioClip a = GetAudio (name);

		if (musicSource != null && a != null) {
			SetMusicVolume (musicOnOrOff);

			musicSource.clip = a;
			musicSource.Play ();
		}
	}

	public void SetPaused (bool paused)
	{
		if (musicSource != null) {
			if (paused)
				musicSource.Pause ();
			else
				musicSource.UnPause ();
		}
	}

	void Awake ()
	{
		musicSource = GetComponent<AudioSource> ();
		effectObject = audioObject;

		if (!PlayerPrefs.HasKey ("music"))
			PlayerPrefs.SetInt ("music", 1);
		
		if (!PlayerPrefs.HasKey ("sound"))
			PlayerPrefs.SetInt ("sound", 1);
		
		musicOnOrOff = PlayerPrefs.GetInt ("music") != 0;
		effectOnOrOff = PlayerPrefs.GetInt ("sound") != 0;

		PauseGame.gamePaused += SetPaused;
	}

	void OnDestroy ()
	{
		PauseGame.gamePaused -= SetPaused;
	}
}
