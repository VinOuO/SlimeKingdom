using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetKey(KeyCode.Mouse1) && Physics.Raycast(ray, out hit))
            {
                //設定目的地
                if (hit.transform.gameObject.tag == "floor")
                {
                    gameObject.transform.GetComponent<Rigidbody>().AddForce(Vector3.up);
                }
            }
        }

    }
}
