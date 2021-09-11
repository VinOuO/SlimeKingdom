using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDest : MonoBehaviour {

    
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
      /*  if(GameObject.Find("menu") != null )
        {
            //Destroy(gameObject);
        }
        else
        {*/
            DontDestroyOnLoad(gameObject);
       // }
        
    }
}
