using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectiveBehavior : NetworkBehaviour {
	public enum huhu {Engine, Bridge, Pod};
	public huhu jiji; 
	bool collide = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other) {
		
		if (!other.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;
		
		if (Input.GetKeyDown (KeyCode.E) && jiji == huhu.Engine) {
			other.GetComponent<PlayerBehavior> ().Cmd_TurnLights();
		}

		if (Input.GetKeyDown (KeyCode.E) && jiji == huhu.Bridge) {
			other.GetComponent<PlayerBehavior> ().Cmd_OpenPod();
		}
		if (Input.GetKeyDown (KeyCode.E) && jiji == huhu.Pod) {
			other.GetComponent<PlayerBehavior> ().Cmd_WinGame();
		}

	}
	void OnTriggerEnter2D(Collider2D other) {
		if (!other.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		collide = true;
	}
	void OnTriggerExit2D(Collider2D other) {
		if (!other.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		collide = false;
	}

	void OnGUI (){

		if (jiji == huhu.Engine) {
			bool x = GameObject.Find ("General").GetComponent<General> ().lightson;
			if (collide && !x) {
				///display text telling the player to open the door with e
				GUI.Label (new Rect (300, 200, 150, 100), "Turn on Power With e.");
			}
			if (collide && x) {
				///display text telling the player to open the door with e
				GUI.Label (new Rect (300, 200, 150, 100), "Turn off Power With e.");
			}
		}

		if(jiji == huhu.Bridge){
			if(collide && !GameObject.Find("General").GetComponent<General>().podDoorOpen){
				GUI.Label(new Rect(300,200, 150, 100), "Open the hangar doors with e.");
			}
			else if(collide){
				GUI.Label(new Rect(300,200, 150, 100), "Pod Doors open!");
			}
		}

		if(jiji == huhu.Pod){
			if(collide && !GameObject.Find("General").GetComponent<General>().Win){
				GUI.Label(new Rect(300,200, 150, 100), "Press E to Get in Pod Get Out!");
			}
		}
	}
}
