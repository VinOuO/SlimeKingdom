using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_health_bar : MonoBehaviour {

    bool is_hide = true;
    IMonster_info monster_info;
    public bool AA = false;
    Vector3 child_scale;
    Vector3 face;
    void Start()
    {
        Hide();
        is_hide = true;
        monster_info = transform.parent.transform.parent.GetComponent(typeof(IMonster_info)) as IMonster_info;
    }

    void Update()
    {
        face= Camera.main.transform.position - transform.position;
        if (monster_info.Health_show("now")< monster_info.Health_show("Max"))
        {
            if (is_hide)
            {
                Show();
                is_hide = false;
            }
            //transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z));

            transform.LookAt(Camera.main.transform.position, -Vector3.up);
            child_scale.Set(monster_info.Health_show("now") / monster_info.Health_show("Max"), 1, 1);
            transform.GetChild(1).localScale = child_scale;
        }
        /*
        else
        {
            if (!is_hide)
            {
                Hide();
                is_hide = true;
            }
        }
        */
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
