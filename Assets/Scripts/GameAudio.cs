using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour {

	public AudioClip music;

	// Use this for initialization
	void Start () {
		PlaySound (music);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void PlaySound(AudioClip soundName)
	{
		AudioSource audio = GetComponent<AudioSource>();
		if (!audio.isPlaying || audio.clip != soundName)
		{
			audio.clip = soundName;
			audio.Play();
		}
	}

}
