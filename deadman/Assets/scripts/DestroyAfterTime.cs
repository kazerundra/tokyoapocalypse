using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour 
{
	public float maxTime = 1.0f;
	float currTime = 0.0f;
    public GameObject plyr;
    private int nowDir = 1;
    private int prevDir = 1;


    private void Start()
    {
        plyr = GameObject.FindGameObjectWithTag("Player").gameObject;
    }



    void Update () 
	{
        if (plyr.GetComponent<player>().rightface)
        {
           nowDir = 1;
        }
        else nowDir = -1;
        if (nowDir != prevDir)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (currTime < maxTime) 
		{
			currTime += Time.deltaTime;
			return;
		}
       
        prevDir = nowDir;
        Destroy(gameObject);
       
    }
}
