using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLOnMenu : MonoBehaviour {

    public SaveAndLoad MenuSL;
	// Use this for initialization
	void Start () {
        MenuSL = GameObject.Find("GameObject").GetComponent<SaveAndLoad>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void saveMwnu()
    {
        MenuSL.save();
    }

    

}
