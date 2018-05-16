using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioMixerGroup audioMixer;
	public Sound[] sounds;

	public static MusicManager instance;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.outputAudioMixerGroup = audioMixer;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound=> sound.name == name);
		s.source.Play ();
	}

	public void StopAll()
	{
		foreach (Sound s in sounds) {
			s.source.Stop ();
		}
	}
}
