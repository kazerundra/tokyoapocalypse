using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject StatusPanel;
    bool isPressed = false;
    bool isQuit = false;
    bool isStatus = false;
    bool isMain = false;
    public bool PauseMenuEnable = false;

    PlayerStats mPlayerStats;

    void Start()
    {
        mPlayerStats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats> ();
    }


    public void menuPressed()
    {
        isPressed = !isPressed;
        PauseMenuEnable = !PauseMenuEnable;
    }

    public void QuitPressed()
    {
        isQuit = true;
    }

    public void StatusPressed()
    {
        isStatus = !isStatus;
    } 


    public void GoBackMain()
    {
        isMain = true;
    }


    // Update is called once per frame
    void Update () {



        if (isMain)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("mainMenu");
        }

        if (isPressed)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }

        if (isStatus)
        {
            Time.timeScale = 0;
        } 
        else
        {
            Time.timeScale = 1;
            StatusPanel.SetActive(false);
        }

    }
}
