using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.GetComponent<CanvasGroup>().blocksRaycasts == false)
                Show();
            else
                Hide();
        }
	}

    void Hide()
    {
        this.GetComponent<CanvasGroup>().alpha = 0f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
