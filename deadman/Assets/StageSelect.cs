﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour {
	
	public GameObject stage2;
	public GameObject stage3;

	public void loadStage(){
		if (GetComponent<SaveManager> ().clearedStage ==1)
			stage2.SetActive (true);
		else if (GetComponent<SaveManager> ().clearedStage == 2) {
			stage2.SetActive (true);
			stage3.SetActive (true);
		} 
	}
}
