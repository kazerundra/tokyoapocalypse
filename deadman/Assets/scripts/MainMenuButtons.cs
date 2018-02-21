using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    public Image Background;
    public Image Background1;
    bool on = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackgroundActivate()
    {
        if (on == true)
        {
            on = false;
        }
        else
        {
            on = true;
        }

        if (on == true)
        {
            Background.enabled = false;
            Background1.enabled = true;
        }
        else
        {
            Background.enabled = true;
            Background1.enabled = false;
        }
    }
}
