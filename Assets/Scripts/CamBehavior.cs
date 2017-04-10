using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CamBehavior : MonoBehaviour {
	public Transform player;
	public float smoothy;
	public float smoothx;
	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("General").GetComponent<General> ().Win) {
			transform.position = new Vector3 (200, 200, -10);
			return;
		}
		if(player != null)
		{
			float x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref smoothx, .01f);
			float y = Mathf.SmoothDamp (transform.position.y, player.position.y, ref smoothy, .01f);
			transform.position = new Vector3 (x, y, -10);
			//transform.position = player.position + new Vector3(0,0,-10);
		}
	}

	public void setTarget(Transform target)
	{
		player = target;
	}
}
