using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    GameObject player;
    /// <summary>
    /// 上個frame Tab是開(1)還是關(2)
    /// </summary>
    public int last_state = 2;
    public GameObject[] slime_type_icon;
    public int icon_num = 0;
    int icon_type = 0;
    ISlime_info slime_info;
    void Start()
    {
        player = GameObject.Find("Player");
        Hide();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (last_state == 2)
            {
                Show();
                for (int i = 0; i < player.GetComponent<Player>().slime_partied_num; i++)
                {
                    slime_info = GameObject.Find("Slime_" + (i + 1)).GetComponent(typeof(ISlime_info)) as ISlime_info;
                    if (slime_info.Type == "tree")
                    {
                        icon_type = 0;
                    }
                    else if(slime_info.Type == "rock")
                    {
                        icon_type = 1;
                    }
                    else if (slime_info.Type == "fire")
                    {
                        icon_type = 2;
                    }
                    else if (slime_info.Type == "grass")
                    {
                        icon_type = 3;
                    }
                    else if (slime_info.Type == "water")
                    {
                        icon_type = 4;
                    }
                    else if (slime_info.Type == "wind")
                    {
                        icon_type = 5;
                    }
                    else if (slime_info.Type == "light")
                    {
                        icon_type = 6;
                    }
                    else if (slime_info.Type == "dark")
                    {
                        icon_type = 7;
                    }
                    else if (slime_info.Type == "mud")
                    {
                        icon_type = 8;
                    }
                    else
                    {
                        icon_type = 9;
                    }
                    Instantiate(slime_type_icon[icon_type], this.transform);
                    transform.GetChild(1 + i).GetComponent<Icon>().num = i + 1;
                }
                last_state = 1;
            }   
        }
        else
        {
            if (last_state == 1)
            {
                Hide();
                last_state = 2;
            }  
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
