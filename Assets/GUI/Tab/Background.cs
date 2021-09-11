using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    /// <summary>
    /// 背景長寬
    /// </summary>
    Vector2 Width_Height;

    void Start()
    {
        //this.GetComponent<RectTransform>().sizeDelta.Set(Screen.width, Screen.height);

    }

    void Update()
    {
        Width_Height.Set(Screen.width * 1f, Screen.height * 1f);
        this.GetComponent<RectTransform>().sizeDelta = Width_Height;
    }
}
