using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Info : MonoBehaviour {
    /// <summary>
    /// 滑鼠射線
    /// </summary>
    Ray ray;
    /// <summary>
    /// XD
    /// </summary>
    RaycastHit hit;
    /// <summary>
    /// 血量資訊位置
    /// </summary>
    Vector3 Health_info_position;
    IMonster_info monster_info;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //------------------------------------------------------------------------------UI
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                health_show(hit.transform.gameObject, hit.transform.gameObject.tag);
            }
            else if(hit.transform.gameObject.tag == "PPAP")
            {
                health_show(hit.transform.gameObject, hit.transform.gameObject.tag);
            }
            else
            {
                this.GetComponent<Text>().text = "";
            }
        }
        //------------------------------------------------------------------------------UI
    }

    /// <summary>
    /// 當滑鼠移到該單位時顯示他的血量
    /// </summary>
    void health_show(GameObject _target,string _tag)
    {
        Health_info_position.Set(Input.mousePosition.x-Screen.width/2, Input.mousePosition.y-Screen.height/2, 0);
        this.GetComponent<RectTransform>().localPosition = Health_info_position;
        if (_tag == "Enemy")
        {
            monster_info = _target.GetComponent(typeof(IMonster_info)) as IMonster_info;
            this.GetComponent<Text>().text = "Health:(" + monster_info.Health_show("now") + "/" + monster_info.Health_show("Max") + ")";
        }
        else if(_tag == "PPAP")
        {
            this.GetComponent<Text>().text = "Health:(" + _target.GetComponent<Player>().health_show("now") + "/" + _target.GetComponent<Player>().health_show("Max") + ")";
        }
        
    }
}
