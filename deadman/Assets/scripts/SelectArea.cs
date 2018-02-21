using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArea : MonoBehaviour {

    public bool isActive = false;
    public bool Spawned = false;
    public GameObject player;
    GameObject Go;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }


    public void ActivateCircle(Vector2 mousePoint)
    {
        isActive = true;
        if (isActive)
        {
            Instantiate(this.gameObject, mousePoint, Quaternion.identity);
            
           
            Spawned = true;
            isActive = false;         
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Spawned && Input.GetMouseButton(0))
        {
            //Go.SetActive(true);
            Vector2 mouseposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mouseposition);
            this.transform.position = worldMousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.GetComponent<Infection>().Activate = true;
            player.GetComponent<Infection>().activeImage.fillAmount = 0.0f;
            Destroy(this.gameObject);
        }

    }
}
