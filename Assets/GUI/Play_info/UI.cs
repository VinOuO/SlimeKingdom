using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    Vector2 Width_Height;

    void Start () {
        DontDestroyOnLoad(this.gameObject);
        Width_Height.Set(Screen.width * 0.07f, Screen.height * 0.1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;
    }
	
	// Update is called once per frame
	void Update () {
        Width_Height.Set(Screen.width, Screen.height);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;
    }
}
