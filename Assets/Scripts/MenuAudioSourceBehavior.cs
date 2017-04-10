using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioSourceBehavior : MonoBehaviour {
	public AudioClip menuMusic;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		PlaySound (menuMusic);
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
