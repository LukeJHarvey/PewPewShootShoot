using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightbeh : MonoBehaviour {
	public bool on = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		bool d = GameObject.Find ("General").GetComponent<General> ().lightson;
		if (d != on) {
			on = d;
			GetComponent<Light> ().enabled = on;
		} else if (GetComponent<Light> ().enabled != on) {
			GetComponent<Light> ().enabled = on;
		}
	}
}
