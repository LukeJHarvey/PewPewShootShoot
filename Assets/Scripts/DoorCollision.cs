using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DoorCollision : NetworkBehaviour {
	public enum doorType {Normal, Bridge, Pod};
	public doorType bluh;
	public bool canOpen = false;
	bool collide = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(bluh == doorType.Normal){
			canOpen = true;
		}
		else if(GameObject.Find ("General").GetComponent<General> ().lightson && bluh == doorType.Bridge ){
			canOpen = true;
		}
		else if(GameObject.Find ("General").GetComponent<General> ().podDoorOpen && bluh == doorType.Pod){
			canOpen = true;
		}

	}

	void OnTriggerStay2D(Collider2D other) {
		if (!other.GetComponent<NetworkIdentity>().isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.E) && canOpen) {
			other.GetComponent<PlayerBehavior> ().Cmd_DestroyObj (transform.parent.gameObject);
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

		if(collide && !canOpen){
			///display text telling the player to open the door with e
			GUI.Label(new Rect(300,200, 150, 100), "Door is Locked Yo!");
		}
		else if(collide){
			///display text telling the player to open the door with e
			GUI.Label(new Rect(300,200, 150, 100), "Open Doors With e.");
		}

	}
}
