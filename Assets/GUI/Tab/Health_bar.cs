using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_bar : MonoBehaviour {

    ISlime_info slime_info;
    /// <summary>
    /// 背景長寬
    /// </summary>
    Vector2 Width_Height;
    /// <summary>
    /// Health_bar在螢幕的相對位置
    /// </summary>
    Vector3 Bar_position;
    /// <summary>
    /// 血條scale
    /// </summary>
    Vector3 Bar_scale;
    public int num;
    void Start () {
        num = transform.parent.GetComponent<Icon>().num;
        slime_info = GameObject.Find("Slime_" + num).GetComponent(typeof(ISlime_info)) as ISlime_info;
        Bar_position.Set(transform.parent.GetComponent<RectTransform>().localPosition.x, transform.parent.GetComponent<RectTransform>().localPosition.y - transform.parent.GetComponent<RectTransform>().sizeDelta.y/2, transform.parent.GetComponent<RectTransform>().localPosition.z);
    }
	
	void Update () {
        Bar_scale.Set(slime_info.Health_show("now")/ slime_info.Health_show("Max") , 1f, 1f);
        this.transform.GetChild(0).transform.localScale = Bar_scale;
        this.GetComponent<RectTransform>().localPosition = Bar_position;
    }
}
