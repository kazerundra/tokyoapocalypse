using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMusic : MonoBehaviour {

    //これのscriptは、mainSceneの音楽の強弱、シーンごとに音を切り替えるため使用します
    private AudioSource mainMusic01;

    // Use this for initialization
    void Start()
    {
        mainMusic01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        mainMusic01.Play();
        mainMusic01.volume = 0.5f; //音の調整(音を小さくしたい時などに使う。)
	}
}
