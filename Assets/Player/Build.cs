using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    //要蓋的地方
    public GameObject floor;
    //要蓋的建築
    public GameObject[] building;
    //已有建築的地點
    public Vector3[] can_not_build;
    //建築數
    int bud_num = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            build(building[0]);
        }
    }

    void build(GameObject stuff_to_build)
    {
        //是否可蓋
        bool can_build = true;
        for(int n = 0; n < 100; n++)
        {
            if (Vector3.Distance(floor.transform.GetComponent<Map>().get_center(), can_not_build[n]) <= 0.15f)
            {
                can_build = false;
                break;
            }
        }
        //如果可以蓋就蓋，並且把此地點列為不可蓋
        if (can_build)
        {
            Instantiate(stuff_to_build, floor.transform.GetComponent<Map>().get_center(), stuff_to_build.transform.rotation);
            can_not_build[bud_num] = floor.transform.GetComponent<Map>().get_center();//new Vector3(floor.transform.GetComponent<Map>().get_center().x, floor.transform.GetComponent<Map>().get_center().y, floor.transform.GetComponent<Map>().get_center().z);
            bud_num++;
        }
    }
}
