using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class BulletBehavior : NetworkBehaviour {
	public float tilldeath = 5;
	public float offsetx;
	public float offsety;
	public AudioClip pew;
	// Use this for initialization
	void Start () {
		transform.Translate (offsetx, offsety, 0);
		PlaySound (pew);
	}

	// Update is called once per frame
	void Update () {
		if (!base.isServer)
			return;

		tilldeath -= Time.deltaTime;
		if (tilldeath < 0) {
			NetworkServer.Destroy (gameObject);
		}
		transform.Translate (0, 5 * Time.deltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemies") {
			other.GetComponent<EnemyBehavior> ().hit ();
		}
		NetworkServer.Destroy(gameObject);
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

	public void setDeath(int x) {
		Destroy (gameObject,x);
	}
}