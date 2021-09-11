using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_Door : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Slime_InFarm")
        {
            Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider>());
        }
    }
   
}
