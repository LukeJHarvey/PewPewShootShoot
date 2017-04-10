using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZuesCanvasScript : MonoBehaviour {
	public float tts;
	public int zombCount;
	public int bossCount;
	public GameObject zues;

	// Use this for initialization
	void Start () {
		tts = 7;
	}
	
	// Update is called once per frame
	void Update () {
		if (tts > 0) {
			tts -= Time.deltaTime;
		}
		if (tts <= 0) {
			tts = 7f;
			zues.GetComponent<ZuesBehavior> ().spawnMore ();
		}
		bossCount = zues.GetComponent<ZuesBehavior> ().bossCount;
		zombCount = zues.GetComponent<ZuesBehavior> ().zombCount;

		transform.GetChild (1).GetChild (0).GetChild (1).GetComponent<Text> ().text = "NEXT: " + (Mathf.Floor (tts)+1);
		transform.GetChild (1).GetChild (3).GetChild (1).GetComponent<Text> ().text = "[" + zombCount + "]";
		transform.GetChild (1).GetChild (4).GetChild (1).GetComponent<Text> ().text = "[" + bossCount + "]";
		transform.GetChild (2).GetChild (1).GetChild (0).GetComponent<Image> ().sprite = zues.GetComponent<ZuesBehavior> ().currentZombie.GetComponent<SpriteRenderer>().sprite;
	}
}
