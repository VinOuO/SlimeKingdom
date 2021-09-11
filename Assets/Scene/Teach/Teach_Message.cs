using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teach_Message : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gameObject);
        }
	}
}
