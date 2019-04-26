using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadStage : MonoBehaviour {

	public GameObject stage1;
	public GameObject stage2;
	public GameObject tutorial;
	// Use this for initialization
	void Start () {
		if (GetComponent<SaveManager> ().currentStage == 1) {
			stage1.SetActive (true);
			tutorial.SetActive (true);
		} else if (GetComponent<SaveManager> ().currentStage == 2) {
			stage2.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
