using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusic : MonoBehaviour {

    //これのscriptは、mainMenuSceneの音楽の強弱、シーンごとに音を切り替えるため使用します

    private AudioSource mainMenuMusic01;

    // Use this for initialization
    void Start()
    {
        mainMenuMusic01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        mainMenuMusic01.Play();
        mainMenuMusic01.volume = 0.8f; //音の調整(音を小さくしたい時などに使う。)
    }
}
