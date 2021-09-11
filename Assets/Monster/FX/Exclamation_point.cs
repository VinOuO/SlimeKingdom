using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation_point : MonoBehaviour {
    Vector3 face_cam;
    bool is_hide = false;
    IMonster_info monster_info;
    public bool AA=false;
	void Start () {
        Hide();
        monster_info = transform.parent.transform.parent.GetComponent(typeof(IMonster_info)) as IMonster_info;
        face_cam.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
    }
	
	void Update () {
        if (monster_info.Dectected_player)
        {
            if (is_hide)
            {
                Show();
                is_hide = false;
            }
            face_cam.Set(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            transform.rotation = Quaternion.LookRotation(face_cam);
        }
        else
        {
            if (!is_hide)
            {
                Hide();
                is_hide = true;
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
