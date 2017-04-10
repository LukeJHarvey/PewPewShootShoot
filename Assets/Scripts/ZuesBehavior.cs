using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ZuesBehavior : NetworkBehaviour {
	public float speed = 3;
	GameObject obj;
	public LayerMask layermask;
	Sprite unlit;
	public Sprite lit;
	public GameObject cvas;
	public GameObject[] zombs;
	public Transform content;
	public GameObject currentZombie = null;
	public int zombCount;
	public int bossCount;
	public Transform isAround;
	public ArrayList zombiesOutThere = new ArrayList();
	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3 (0, 0, 0);
		cvas = (GameObject)Instantiate (cvas, new Vector3 (0, 0, 0), Quaternion.identity);
		if (zombs.Length != 0) {
			currentZombie = zombs [0];
		}
		content = cvas.transform.GetChild(1);
		content.GetComponent<ZuesCanvasScript> ().zues = this.gameObject;
		zombCount = 2;
		bossCount = 1;
		isAround = transform.GetChild (0);
		GameObject.Find ("Main Camera").SetActive (false);
		gameObject.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameObject.Find("NetworkManager").GetComponent<NetworkScript>().StopClient();
			Application.Quit();
		}
		if (GameObject.Find ("General").GetComponent<General> ().Win) {
			transform.position = new Vector3 (300, 300, -10);
			return;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			currentZombie = zombs [0];
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			currentZombie = zombs [1];
		}
			
		
		float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

		float y = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
		transform.Translate (x, y, 0);

		if (obj != null) {
			obj.GetComponent<SpriteRenderer>().sprite = unlit;
			obj = null;
		}

		Vector3 de = transform.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
		isAround.position = new Vector3(de.x,de.y,0);
		RaycastHit2D hit;
		hit = Physics2D.Raycast(de, Vector2.zero,20,layermask);
		if (hit.collider != null && hit.collider.tag!="Dontdo" && !isAround.GetComponent<zuesCollider>().player) {
			//print (hit.transform.name);
			if (currentZombie != null) {
				//print (hit.transform.gameObject.layer);
				obj = hit.transform.gameObject;
				unlit = obj.GetComponent<SpriteRenderer> ().sprite;
				if (Input.GetMouseButtonDown (0)) {
					if (zombiesOutThere.Count < 4) {
						if (currentZombie == zombs [0]) {
							if (zombCount > 0) {
								GameObject ye = Instantiate (currentZombie, new Vector3 (hit.transform.position.x, hit.transform.position.y, 0), hit.transform.rotation) as GameObject;
								NetworkServer.Spawn (ye);
								zombiesOutThere.Add (ye);
								zombCount--;
								//zombiesOutThere++;
							}
						} else if (currentZombie == zombs [1]) {
							if (bossCount > 0) {
								GameObject ye = Instantiate (currentZombie, new Vector3 (hit.transform.position.x, hit.transform.position.y, 0), hit.transform.rotation) as GameObject;
								NetworkServer.Spawn (ye);
								zombiesOutThere.Add (ye);
								bossCount--;
								//zombiesOutThere++;
							}
						}
					}

				}
				obj.GetComponent<SpriteRenderer> ().sprite = lit;
			}
		}
	}

	public void spawnMore() {
		zombCount += 4;
		bossCount += 2;
		if (zombCount > 10) {
			zombCount = 10;
		}
		if (bossCount > 2) {
			bossCount = 2;
		}
	}
	public void takeOffZomb(GameObject d) {
		zombiesOutThere.Remove(d);
	}
}
