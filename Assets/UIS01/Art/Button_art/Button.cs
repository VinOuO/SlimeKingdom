using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    bool press = false;
	void Start () {
		
	}
	
	void Update () {

	}

    public void OnPointerExit(PointerEventData eventData)
    {
        if (press)
        {
            this.GetComponent<RectTransform>().localScale /= 0.9f;
            press = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (press)
        {
            this.GetComponent<RectTransform>().localScale /= 0.9f;
            press = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!press)
        {
            this.GetComponent<RectTransform>().localScale *= 0.9f;
            press = true;
        }
    }
}
