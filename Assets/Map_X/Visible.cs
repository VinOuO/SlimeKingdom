using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour {
    public static bool MeshisVisible = false;

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (MeshisVisible == true)
            {
                MeshisVisible = false;
            }
            else
            {
                MeshisVisible = true;
            }
        }
        if (!MeshisVisible)
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().enabled = true;
        }
    }
}
