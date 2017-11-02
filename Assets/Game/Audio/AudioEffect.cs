using UnityEngine;
using System.Collections;

public class AudioEffect : MonoBehaviour
{
	private AudioSource source;

	public float delay;
	public string audioName;
	private bool pause;

	private float timer = 0f;

	public void SetVolume (bool onOrOff)
	{
		if (source != null) {
			if (onOrOff) {
				source.volume = 100f;
			} else {
				source.volume = 0f;
			}
		}
	}

	public void SetPaused (bool paused)
	{
		if (source != null) {
			if (paused)
				source.Pause ();
			else
				source.UnPause ();
		}
		pause = paused;
	}

	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource> ();
		source.clip = AudioCollection.GetAudio (audioName);

		SetVolume (AudioCollection.effectOnOrOff);

		AudioCollection.effectVolume += SetVolume;
		PauseGame.gamePaused += SetPaused;
	}

	void OnDestroy ()
	{
		AudioCollection.effectVolume -= SetVolume;
		PauseGame.gamePaused -= SetPaused;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (source == null || source.clip == null) {
			Destroy (this.gameObject);

		} else if (timer > delay) {
			if (!source.isPlaying && !pause)
				Destroy (this.gameObject);
			
		} else { // timer < delay
			if (timer + Time.deltaTime > delay)
				source.Play ();
		}

		timer += Time.deltaTime;
	}
}
