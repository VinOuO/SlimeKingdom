using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_con : MonoBehaviour {

    public bool opening = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            opening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            opening = false;
        }
    }
}
