using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zuesCollider : MonoBehaviour {
	public bool player = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			player = true;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			player = false;
		}
	}
}
