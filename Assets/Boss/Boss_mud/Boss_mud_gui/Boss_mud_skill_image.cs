using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_mud_skill_image : MonoBehaviour {
    /// <summary>
    /// 背景長寬
    /// </summary>
    Vector2 Width_Height;
    /// <summary>
    /// mud在螢幕的相對位置
    /// </summary>
    Vector3 mud_position;
    public bool change_pos = false;
    float time = 0;
    void Start () {
		
	}
	
	void Update () {
        if (change_pos)
        {
            Width_Height.Set(Screen.width * 0.5f, Screen.width * 0.5f);
            this.GetComponent<RectTransform>().sizeDelta = Width_Height;
            mud_position.Set(Random.Range(-Screen.width / 2, Screen.width / 2), Random.Range(-Screen.height / 2, Screen.height / 2), 0);
            this.GetComponent<RectTransform>().localPosition = mud_position;
            change_pos = false;
            time = Time.time + 2;
        }
        if (Time.time - time >= 0.1f)
        {
            time = Time.time;
            Width_Height -= new Vector2(Screen.width * 0.5f / 30, Screen.width * 0.5f / 30);
            this.GetComponent<RectTransform>().sizeDelta = Width_Height;
        }
    }
}
