using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class changeIp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Your IP Address: " + Network.player.ipAddress;
	}
}