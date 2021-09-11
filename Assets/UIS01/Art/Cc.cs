using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cc : MonoBehaviour {
    float start_time;
	void Start () {
        start_time = Time.time;
        transform.SetSiblingIndex(0);
    }

	void Update () {
        if (Time.time - start_time >= 1)
        {
            Destroy(this.gameObject);
        }
	}
}
