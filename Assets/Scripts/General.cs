using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class General : NetworkBehaviour {
	[SyncVar] public bool lightson = false;
	[SyncVar] public bool podDoorOpen  = false;
	public GameObject[] objectives;
	public GameObject currentObjective;
	[SyncVar] public bool Win = false;
	// Use this for initialization
	void Start () {
		foreach(GameObject n in objectives) {
			if (n != currentObjective) {
				n.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				n.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!lightson && !podDoorOpen && currentObjective!=objectives [0]) {
			currentObjective = objectives [0];
			objectiveChange ();
		} else if (lightson && !podDoorOpen && currentObjective!=objectives [1]) {
			currentObjective = objectives [1];
			objectiveChange ();
		} else if (podDoorOpen && currentObjective!=objectives [2]) {
			currentObjective = objectives [2];
			objectiveChange ();
		}
		else if(Win){
			//SceneManager.LoadScene ("Lost");
		}
	}
	void objectiveChange() {
		foreach(GameObject n in objectives) {
			if (n != currentObjective) {
				n.GetComponent<SpriteRenderer> ().enabled = false;
			} else {
				n.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
	}
	void OnGui() {
		if (Win) {
			///display text telling the player to open the door with e
			GUI.Label (new Rect (300, 200, 150, 100), "YOU WIN!");
		}
	}
}
