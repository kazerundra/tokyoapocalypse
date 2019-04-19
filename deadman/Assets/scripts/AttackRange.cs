using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {
	public bool AttackMove;
	public bool StartAttack;
	public GameObject target;
	public void attackMove(GameObject enemy){
		AttackMove = true;
		target = enemy;
		//StartAttack = false;
	}


	private void OnTriggerStay2D(Collider2D other)
	{

		if (AttackMove&& other.name == target.name) 
		{	Debug.Log (other+ "target" + target);
			AttackMove = false;
			StartAttack= true;
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
