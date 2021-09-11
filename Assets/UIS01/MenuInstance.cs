using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static MenuInstance Menuinstance;

    private void Awake()
    {
        if (Menuinstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Menuinstance = this;
        }
    }
}
