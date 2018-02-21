using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class holdMenu : MonoBehaviour {
    private float pressPeriod = 0;
    bool activated = false;
	public bool swipe = false;
    public GameObject commandMenu;
    Vector3 position;
    public Vector3 screenToWorldPointPosition;
    public GameObject partner;
    public GameObject player;
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
	public GameObject itempanel;
    public GameObject PauseMenu;

	void Start ()
	{
		itempanel = GameObject.FindGameObjectWithTag ("ItemButton").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        PauseMenu = GameObject.FindGameObjectWithTag("commandMenu").gameObject;
        
	}
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && player.GetComponent<Infection>().Activate == false)
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            pressPeriod = 0;
			//swipe = false;
        }
        if (Input.GetMouseButton(0))
        {
            pressPeriod += Time.deltaTime;
            //Debug.Log("holding");
            //Debug.Log(pressPeriod);
			if (pressPeriod >= 0.5f && !activated && !swipe && (player.GetComponent<Infection>().isPressed == false) && PauseMenu.GetComponent<UI>().PauseMenuEnable == false)
            {
                activated = true;
				swipe = false;
                //stopCount();
                //Debug.Log("commandmenu");
                
                position = Input.mousePosition;
                position.z = 10f;
                // マウス位置座標をスクリーン座標からワールド座標に変換する
                screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
                // ワールド座標に変換されたマウス座標を代入
                //commandMenu.transform.position = screenToWorldPointPosition;
                commandMenu.transform.position = position;
                commandMenu.SetActive(true);
                player.GetComponent<player>().menuStatus= true;
                pressPeriod = 0;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
			
			if (!activated && pressPeriod > 0.2f && pressPeriod < 0.5f) {
                swipe = true;
				pressPeriod = 0;
				secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				Debug.Log (secondPressPos);
				Debug.Log (firstPressPos);
				//create vector from the two points
				currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y); 

				//normalize the 2d vector
				currentSwipe.Normalize();
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 5f && !activated && swipe)
                {   
					//Debug.Log (currentSwipe.x);
					//Debug.Log("left swipe");
                    activated = false;
                    itempanel.GetComponent<ItemSwitch>().PressLeft();
                    
                }

                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 5f && !activated && swipe)
                {

					//Debug.Log (currentSwipe.x);
                    //Debug.Log("right swipe");
                    activated = false;
                    itempanel.GetComponent<ItemSwitch>().PressRight();
                    
                }
            } else {
				swipe = false;
				firstPressPos.x = 0;
				firstPressPos.y = 0;
                pressPeriod = 0;
            }
			
        }
    }
    public void moveTo()
	{ activated = false;
		if (partner != null) {
			partner.GetComponent<partnerAI> ().mode = 2;
			screenToWorldPointPosition = Camera.main.ScreenToWorldPoint (position);
			partner.GetComponent<partnerAI> ().mousePosition = screenToWorldPointPosition;
			closeMenu ();
		} else
			closeMenu ();
    }
    public void followPlayer()
	{ activated = false;
		if (partner != null) {
			partner.GetComponent<partnerAI> ().mode = 3;
			closeMenu ();
		} else
		closeMenu ();
    }
    public void closeMenu()
    {
		activated = false;
        commandMenu.SetActive(false);
        player.GetComponent<player>().menuStatus = false;
    }
    public void protect()
	{ activated = false;
		if (partner != null) {
			screenToWorldPointPosition = Camera.main.ScreenToWorldPoint (position);
			partner.GetComponent<partnerAI> ().mousePosition = screenToWorldPointPosition;
			Vector2 pPosition = player.GetComponent<player> ().transform.position;
			if (pPosition.x < screenToWorldPointPosition.x) { 
				partner.GetComponent<partnerAI> ().moveto ();
                partner.GetComponent<partnerAI>().direction = 1;


            } else {
				partner.GetComponent<partnerAI> ().moveto ();
                partner.GetComponent<partnerAI>().direction = 2;
            }
			commandMenu.SetActive (false);
			player.GetComponent<player> ().menuStatus = false;
	}else
		closeMenu ();
    }

}
