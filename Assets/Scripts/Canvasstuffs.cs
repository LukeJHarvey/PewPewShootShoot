using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Canvasstuffs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene ();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

		if (sceneName == "Main Menu") 
		{
			transform.GetChild (1).GetChild (1).GetChild (1).gameObject.GetComponent<Button> ().onClick.AddListener (GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().startClientButton);
			transform.GetChild (1).GetChild (1).GetChild (0).gameObject.GetComponent<Button> ().onClick.AddListener (GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().startHostButton);
			transform.GetChild (1).GetChild (5).gameObject.GetComponent<Button> ().onClick.AddListener (GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().kill);
			transform.GetChild (1).GetChild (3).gameObject.GetComponent<Slider> ().onValueChanged.AddListener(GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().changeMusicLevel);
			transform.GetChild (1).GetChild (4).gameObject.GetComponent<Slider> ().onValueChanged.AddListener(GameObject.Find ("NetworkManager").GetComponent<NetworkScript> ().changeFxLevel);
			GameObject x = transform.GetChild (1).GetChild (2).GetChild (1).gameObject;
			InputField mainInputField = x.GetComponent<InputField>();
			mainInputField.onEndEdit.AddListener (delegate{GameObject.Find ("NetworkManager").GetComponent<NetworkScript>().changeString(mainInputField);});

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
