using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_mud_skill : MonoBehaviour {

	void Start () {
        Hide();
	}
	
	void Update () {
		
	}

    public void Hide()
    {
        this.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        this.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    public void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        for(int i = 0; i <= 2; i++)
        {
            transform.GetChild(i).GetComponent<Boss_mud_skill_image>().change_pos = true;
        }
    }
}
