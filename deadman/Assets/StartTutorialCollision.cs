using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorialCollision : MonoBehaviour {
	public bool startTutorial = false ;

	void OnTriggerEnter2D (Collider2D col){
		Debug.Log (col.gameObject.name);
		if (col.gameObject.tag == "Player") {
			startTutorial = true;
			
		}
	}

}
