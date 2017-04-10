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
    int currentHealth;
	public Slider AmmoBarScript;
	public int maxAmmo = 15;
	public int currentAmmo;


	// Use this for initialization
	public override void OnStartLocalPlayer() {

		//note: change getting at scripts to be less hardcoded as far as the hierarchy
		currentHealth = maxHealth;
		HealthBarSliderScript = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
		HealthBarSliderScript.maxValue = maxHealth;
		HealthBarSliderScript.value = currentHealth;
		///could equal maxHealth but i thought this would make the connection between value and 
		/// current health make more sense in my head.
	
		currentAmmo = maxAmmo;
		AmmoBarScript = GameObject.Find ("Canvas").transform.GetChild (1).GetComponent<Slider> ();
		AmmoBarScript.maxValue = maxAmmo;
		AmmoBarScript.value = currentAmmo;
		//same here


		rg = transform.GetComponent<Rigidbody2D> ();
		Camera.main.GetComponent<CamBehavior>().setTarget(gameObject.transform);
		mpos = Input.mousePosition;
		mpos = Camera.main.ScreenToWorldPoint (mpos);
		float angle = AngleBetweenTwoPoints (transform.position, mpos);
		transform.eulerAngles = (new Vector3 (0, 0, angle+90));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isLocalPlayer)
			return;

		HealthBarSliderScript.value = currentHealth;
		AmmoBarScript.value = currentAmmo;
		//print (currentAmmo);

		mpos = Input.mousePosition;
		mpos = Camera.main.ScreenToWorldPoint (mpos);

		float angle = AngleBetweenTwoPoints (transform.position, mpos);
		transform.eulerAngles = (new Vector3 (0, 0, angle+90));
		if (Input.GetKeyDown (KeyCode.Space)) {
			Cmd_SpawnBullet ();
		}
		float x = Input.GetAxis ("Horizontal") * speed ;

		float y = Input.GetAxis ("Vertical") * speed ;

		rg.velocity = transform.TransformDirection(new Vector2(x,y));

		if(Input.GetKeyDown("r")){
			currentAmmo = maxAmmo;
		}
	}

	[Command]
	void Cmd_SpawnBullet() {
		if(currentAmmo > 0){
			currentAmmo--;
			GameObject bull = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			//so the rotation works with sideways sprite
			bull.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			NetworkServer.Spawn (bull);
		}
	}

	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}


}
