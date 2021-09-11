using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_GUI : MonoBehaviour {

    /// <summary>
    /// 上個frame GUI是開(1)還是關(2)
    /// </summary>
    public int last_state = 2;

    void Start()
    {
        Hide();
    }

    void Update()
    {
        if (transform.parent.GetComponent<Food_con>().opening)
        {
            //if (last_state == 2)
            //{
                Show();
                //last_state = 1;
            //}
            //else 
            //{
               // Hide();
                //last_state = 2;
           // }
        }
        else
        {
            Hide();
        }
    }

    void Hide()
    {
        this.GetComponent<CanvasGroup>().alpha = 0f; //this makes everything transparent
        this.GetComponent<CanvasGroup>().blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
