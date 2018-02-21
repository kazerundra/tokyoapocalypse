using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleMusic : MonoBehaviour {

    //これのscriptは、titleSceneの音の調整をする際に使用します。
    // Use this for initialization
    private AudioSource titleMusic01;

    // Use this for initialization
    void Start()
    {
        titleMusic01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        titleMusic01.Play();
        titleMusic01.volume = 0.8f; //音の調整(音をシーンごとに調節したい時などに使う。)
    }
}
