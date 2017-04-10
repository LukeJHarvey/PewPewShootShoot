using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBehavior : NetworkBehaviour {
	Vector3 mpos = Vector3.zero;
	bool move;
	bool up;
	public float speed;
	public GameObject bullet;
	Rigidbody2D rg;

	//i decided not to give currenthealth and currentammo public here bc i thought it would get confusing
	//in the inspector. they are initialized to be the same as max health at the game start anyway. AJ
	public Slider HealthBarSliderScript;
	public int maxHealth = 70;
    public int currentHealth;
	public Slider AmmoBarScript;
	public int maxAmmo = 15;
	public int currentAmmo;
	public GameObject cvas;
	bool knockback;
	float kbTime;
	float kbAngle;



	// Use this for initialization
	public override void OnStartLocalPlayer() {
		setup ((GameObject)Instantiate (cvas, new Vector3 (0, 0, 0), Quaternion.identity));
		rg = transform.GetComponent<Rigidbody2D> ();
		GameObject.Find("Main Camera").GetComponent<CamBehavior>().setTarget(gameObject.transform);
		mpos = Input.mousePosition;
		mpos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint (mpos);
		float angle = AngleBetweenTwoPoints (transform.position, mpos);
		transform.eulerAngles = (new Vector3 (0, 0, angle+90));
		//rg.isKinematic = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		if (GameObject.Find ("General").GetComponent<General> ().Win) {
			return;
		}
		HealthBarSliderScript.value = currentHealth;
		AmmoBarScript.value = currentAmmo;

		mpos = Input.mousePosition;
		mpos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint (mpos);

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (currentAmmo > 0) {
				currentAmmo--;
				Cmd_SpawnBullet ();
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			speed = 12;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = 3;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameObject.Find("NetworkManager").GetComponent<NetworkScript>().StopClient();
			Application.Quit();
		}
		if (!knockback) {
			float x = Input.GetAxis ("Horizontal") * speed;

			float y = Input.GetAxis ("Vertical") * speed;
			if (x != 0 || y != 0) {
				rg.isKinematic = false;
				rg.velocity = transform.TransformDirection (new Vector2 (x, y));
			} else {
				rg.isKinematic = true;
				rg.velocity = Vector2.zero;
			}
			//rg.velocity = transform.TransformDirection (new Vector2 (x, y));

			float angle = AngleBetweenTwoPoints (transform.position, mpos);
			transform.eulerAngles = (new Vector3 (0, 0, angle+90));
		} else {
			if (kbTime > 0) {
				kbTime -= Time.deltaTime;
			} else {
				knockback = false;
			}
			rg.velocity = transform.TransformDirection (new Vector2 (0, -5));
			transform.eulerAngles = (new Vector3 (0, 0, kbAngle + 90));
		}

		if(Input.GetKeyDown("r")){
			currentAmmo = maxAmmo;
		}

		if (currentHealth <= 0) {
			GameObject.Find("NetworkManager").GetComponent<NetworkScript>().StopClient();
			Application.Quit();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameObject.Find("NetworkManager").GetComponent<NetworkScript>().StopClient();
			Application.Quit();
		}
	}

	[Command]
	void Cmd_SpawnBullet() {
		GameObject bull = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
		NetworkServer.Spawn (bull);
	}
	[Command]
	public void Cmd_DestroyObj(GameObject obj) {
		NetworkServer.Destroy (obj);
	}
	[Command]
	public void Cmd_TurnLights() {
		GameObject.Find ("General").GetComponent<General> ().lightson = true;
	}
	[Command]
	public void Cmd_OpenPod() {
		GameObject.Find ("General").GetComponent<General> ().podDoorOpen = true;
	}
	[Command]
	public void Cmd_WinGame() {
		GameObject.Find ("General").GetComponent<General> ().Win = true;
	}


	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	public void setup(GameObject o) {
		cvas = o;
		//note: change getting at scripts to be less hardcoded as far as the hierarchy
		currentHealth = maxHealth;
		HealthBarSliderScript = cvas.transform.GetChild(0).GetComponent<Slider>();
		HealthBarSliderScript.maxValue = maxHealth;
		HealthBarSliderScript.value = currentHealth;
		///could equal maxHealth but i thought this would make the connection between value and 
		/// current health make more sense in my head.

		currentAmmo = maxAmmo;
		AmmoBarScript = cvas.transform.GetChild (1).GetComponent<Slider> ();
		AmmoBarScript.maxValue = maxAmmo;
		AmmoBarScript.value = currentAmmo;
		//same here
	}

	public void PlaySound(AudioClip soundName)
	{
		AudioSource audio = GetComponent<AudioSource>();
		if (!audio.isPlaying || audio.clip != soundName)
		{
			audio.clip = soundName;
			audio.Play();
		}
	}
	public void weHit(int hm, EnemyBehavior.zombType x, float angle) {
		currentHealth-=hm;
		knockback = true;
		if (x == EnemyBehavior.zombType.Boss) {
			kbTime = .5f;
		} else {
			kbTime = .25f;
		}
		kbAngle = angle;
	}


}
