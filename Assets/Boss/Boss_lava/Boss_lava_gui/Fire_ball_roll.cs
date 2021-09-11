using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_ball_roll : MonoBehaviour {

    float roll_time = 0;

	void Start () {
		
	}
	
	void Update () {
        if (Time.time - roll_time >= 0.1f)
        {
            this.GetComponent<RectTransform>().eulerAngles += Vector3.back * 5;
        }
	}
}
