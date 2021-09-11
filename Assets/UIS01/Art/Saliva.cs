using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saliva : MonoBehaviour
{
    bool up = false;
    float time = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Time.time - time >= 0.1f)
        {
            if (!up)
            {
                this.GetComponent<RectTransform>().localScale += Vector3.up * 0.001f;
            }
            else
            {
                this.GetComponent<RectTransform>().localScale -= Vector3.up * 0.01f;
            }
        }
        if(this.GetComponent<RectTransform>().localScale.y>=0.15 && !up)
        {
            up = true;
        }
        if (this.GetComponent<RectTransform>().localScale.y <= 0.01 && up)
        {
            up = false;
        }
    }
}
