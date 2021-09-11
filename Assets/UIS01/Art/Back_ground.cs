using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_ground : MonoBehaviour {

    Vector2 Width_Height;

    void Start () {
		
	}
	
	void Update () {
        Width_Height.Set(Screen.width, Screen.height);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

    }
}
