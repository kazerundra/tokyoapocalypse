using UnityEngine;

public class boss : MonoBehaviour {

	public GameObject vaccine;
    bool death = false;
    float droptime;
    bool spawned = false;

	// Update is called once per frame
	void Update () {
        if (spawned == true)   return;       
		if (this.GetComponent<Enemy> ().enemyHP == 0) {
            death = true;
		}
        if(death)
        {                     
            droptime += Time.deltaTime;
            if (droptime >= 3)
            {
                Instantiate(vaccine, transform.position, transform.rotation);
                spawned = true;
            }
        }
	}
}
