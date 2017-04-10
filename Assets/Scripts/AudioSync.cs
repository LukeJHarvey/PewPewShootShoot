using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSync : MonoBehaviour {
	public enum AudioType {Music, Fx};
	public AudioType whichType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (whichType == AudioType.Music) {
			transform.GetComponent<AudioSource> ().volume = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().MusicLevel;
		} else if (whichType == AudioType.Fx) {
			transform.GetComponent<AudioSource> ().volume = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().FxLevel;
		} else {
			print ("need type");
		}
	}
}
