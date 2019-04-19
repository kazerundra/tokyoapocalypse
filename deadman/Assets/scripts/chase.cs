using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chase : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (GetComponentInParent<Enemy> ().idleState == true) {
			GetComponentInParent<Enemy> ().move (col.gameObject);
		}
			
	}

}
