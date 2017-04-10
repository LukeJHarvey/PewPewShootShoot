using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class NetworkScript : NetworkManager
{
	public float FxLevel = .5f;
	public float MusicLevel = .5f;
	public GameObject zuesPrefab;
	public bool zuesOn;
	public GameObject pewSound;

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		if (zuesOn) {
			zuesOn = false;
			GameObject zues = (GameObject)Instantiate(zuesPrefab, new Vector3(0,0,-10), Quaternion.identity);
			//NetworkServer.AddPlayerForConnection (conn, zues, playerControllerId);

		} else
		{
			//GameObject playerc = (GameObject)Instantiate (playerCanvas, new Vector3 (0, 0, 0), Quaternion.identity);

			GameObject player = (GameObject)Instantiate(playerPrefab, GetStartPosition().position, Quaternion.identity);
			//player.GetComponent<PlayerBehavior> ().setup (playerc);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
			//GameObject.Find ("Main Camera").SetActive (true);
		}

	}

	public void startClientButton(){
		StartClient ();
	}
	public void startHostButton(){
		StartHost ();
	}

	public void changeMusicLevel(float x) {
		FxLevel = x;
		GameObject d= Instantiate (pewSound, new Vector3 (100, 100, 100), Quaternion.identity) as GameObject;
		d.GetComponent<BulletBehavior> ().setDeath (1);
	}
	public void changeFxLevel(float x) {
		MusicLevel = x;
	}
	public void kill() {
		Application.Quit ();
	}
	public void changeString(InputField _input) {
		networkAddress = _input.text;
	}


}
