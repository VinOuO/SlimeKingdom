using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour {

    float time, start_time;
    bool is_ready = false;

	void Start () {
        time = 0;
        start_time = Time.time;
        transform.GetChild(1).gameObject.SetActive(false);
	}
	
	void Update () {
        if (!is_ready)
        {
            grow_up();
        }
        if(Time.time - time >= 20)
        {
            Destroy(this.gameObject);
        }
    }

    void grow_up()
    {
        if (this.transform.position.y <= 4)
        {
            if (Time.time - time >= 0.1f)
            {
                time = Time.time;
                transform.position += new Vector3(0, 0.1f, 0);
            }
        }
        else
        {
            is_ready = true;
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
