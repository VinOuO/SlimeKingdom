using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{

    /// <summary>
    /// 背景長寬
    /// </summary>
    Vector2 Width_Height;
    /// <summary>
    /// Icon在螢幕的相對位置
    /// </summary>
    Vector3 Icon_position;
    /// <summary>
    /// Icon代表哪個slime in party
    /// </summary>
    public int num;
    /// <summary>
    /// 血條
    /// </summary>
    public GameObject Health_bar;
    void Start()
    {
    }

    void Update()
    {
        Width_Height.Set(Screen.width * 0.07f, Screen.height * 0.1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;

        Icon_position.Set(-Screen.width / 2 + Screen.width / 10 * ((num - 1) % 10) + (Screen.width + 5) / 20, Screen.height / 2 - (Screen.height * 2 / 10 * ((num - 1) / 10)) - Screen.height / 10, 0);
        this.GetComponent<RectTransform>().localPosition = Icon_position;

        if (GameObject.Find("Tab").GetComponent<Tab>().last_state == 2)
        {
            Destroy(this.gameObject);
        }
    }
}
