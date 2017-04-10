

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class EnemyBehavior : NetworkBehaviour {
	
	public float speed = 2f;
	[SyncVar] public Transform player = null;
	Rigidbody2D rg;
	[SyncVar] int health = 10;
	float y;
	float ttmove = 3;
	public LayerMask layers;
	int kb;

	Vector2 randomPoint;
	float addTime;
	public AudioClip hurtmrgroans;
	public AudioClip deathmrgroans;
	public enum zombType {Normal, Boss};
	public zombType zomb;
	public GameObject zues;



	// Use this for initialization
	void Start () {
		GetComponent<AudioSource> ().volume = GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().FxLevel;
		rg = transform.GetComponent<Rigidbody2D> ();
		y = speed;
		if (zomb == zombType.Boss) {
			kb = 3;
		}
		if (zomb == zombType.Normal) {
			kb = 5;
		}
	}

	void Update () {
		if (GameObject.Find ("General").GetComponent<General> ().Win) {
			return;
		}

		addTime += Time.deltaTime;

		if(health <= 0){
			if (zues != null) {
				zues.GetComponent<ZuesBehavior> ().takeOffZomb (this.gameObject);
			}
			Destroy (gameObject);
		}
		if (player == null) {
			player = transform;
		}

		if (player.tag == "Player") {
			addTime = 0;

			float angle = AngleBetweenTwoPoints (transform.position, player.transform.position);
			transform.eulerAngles = (new Vector3 (0, 0, angle+90));
		
			y = speed;

			rg.velocity = transform.TransformDirection(new Vector2(0,y));
		}

		if (player.tag != "Player") {
			if (y >= 0) {
				y-= .02f;
			}
			if (y < 0)
				y = 0;

			rg.angularVelocity = 0f;
			rg.velocity = transform.TransformDirection(new Vector2(0,y));
				
			if (addTime >= ttmove){
				
				//chooses a random direction and points the zombie towards it. 
				randomPoint = Random.insideUnitCircle * (2*Mathf.PI);
				float angle = AngleBetweenTwoPoints (transform.position, randomPoint);
				transform.eulerAngles = (new Vector3 (0, 0, angle+90));
				y = speed;
				rg.velocity = transform.TransformDirection(new Vector2(0,y));

				addTime = 0;

			}
		}
	}

	public void hit() {
		health--;
		if (health == 0) {
			PlaySound (deathmrgroans);
		} else {
			PlaySound (hurtmrgroans);
		}
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
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

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Player") {
			float angle = AngleBetweenTwoPoints (other.transform.position, transform.position);
			other.gameObject.GetComponent<PlayerBehavior> ().weHit (kb, zomb, angle);

		}
	}



}
