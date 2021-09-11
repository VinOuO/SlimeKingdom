using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_bar_palyer : MonoBehaviour {

    Vector2 Width_Height;
    Vector3 FX_position, size;
    GameObject player;
    void Start () {
        player = GameObject.Find("Player");
        size.Set(1, 0.5f, 1);

    }
	
	void Update () {
        Width_Height.Set(Screen.height * 0.1f * 5, Screen.height * 0.1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        FX_position.Set(Screen.width * 0.15f - Screen.width / 2, -Screen.height * 0.08f + Screen.height / 2, 0);
        this.GetComponent<RectTransform>().localPosition = FX_position;

        size.x = player.GetComponent<Player>().health_show("%");
        transform.GetChild(1).GetComponent<RectTransform>().localScale = size;
    }
}
