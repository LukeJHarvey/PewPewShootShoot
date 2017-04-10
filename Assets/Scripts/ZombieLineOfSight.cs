using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLineOfSight : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
		//	print ("hit");
			if (transform.parent.tag == "Enemies") {
				transform.parent.GetComponent<EnemyBehavior>().player = other.transform;
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
		//	print ("hitting");

			//makes sure that the line of sight is accessing the script appropriate for its parent. 

		}



	}
	void OnTriggerExit2D(Collider2D other){

		if(other.tag == "Player"){
		//	print ("exit");

			//makes sure that the line of sight is accessing the script appropriate for its parent. 
			if (transform.parent.tag == "Enemies") {
				transform.parent.GetComponent<EnemyBehavior>().player = transform.parent;
			}

		}

	}
}