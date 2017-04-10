using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class General : NetworkBehaviour {
	[SyncVar] public bool lightson = false;
	public GameObject currentCanvas;
	public GameObject zues;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
