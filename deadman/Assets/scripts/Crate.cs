using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public GameObject itemDrop;
    public float CrateHP = 4.0f;
	public GameObject player;
    public GameObject Item;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player").gameObject;
	}

    public void DropItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Item")
            {
                child.gameObject.SetActive(true);
                child.parent = null;
                child.transform.position = transform.position;
                i -= 1;
            }
        }
    }

    public void Crate_damage(int DamageDeal)
    {
       // Debug.Log("dealing damage");
        CrateHP -= DamageDeal;
        if (CrateHP <= 0)
        {
            DropItem();
            Destroy (gameObject);
        }
    }

	void OnMouseDown(){
		player.GetComponent<player> ().opponent = this.gameObject;
	}
	void OnMouseExit(){
		player.GetComponent<player> ().opponent = null;
	}
}
