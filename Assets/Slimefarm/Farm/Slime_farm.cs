using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_farm : MonoBehaviour
{
    GameObject player;
    public GameObject[] slime_farm;
    public GameObject[] slime;
    public string warning = "87";
    public int slime_num = 0;
    public int farm_num;
    public int slime_max_num = 0;
    void Start()
    {
        player = GameObject.Find("Player");
        farm_num = 1;
        produce_slime(1);
        produce_slime(2);
        produce_slime(3);
        produce_slime(4);
        produce_slime(5);
        produce_slime(6);
        produce_slime(7);
        produce_slime(8);
        produce_slime(9);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            slime_num++;
            slime_max_num++;
            produce_slime((int)Time.time % 10);
        }
        put_inout_slime();
    }

    void produce_slime(int _type)
    {
        Instantiate(slime_farm[_type], new Vector3(this.transform.GetChild(4).transform.position.x, this.transform.GetChild(4).transform.position.y + 5, this.transform.GetChild(4).transform.position.z), slime_farm[_type].transform.rotation, this.gameObject.transform);
    }

    void put_inout_slime()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //camRay代表滑鼠指向螢幕內的射線
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //沿著camRay打出射線，並計算與地板的交點(floorHit)
        //out為C＃特有的關鍵字，通常代表回傳值，在此會改寫floorHit的內容
        if (Physics.Raycast(ray, out hit, 1 << 8) && Input.GetKey(KeyCode.P))
        {
            if (hit.transform.gameObject.tag == "Slime_InFarm")
            {
                if (player.GetComponent<Player>().slime_partied_num < 30)
                {
                    ISlime_farm_info slime_info;
                    slime_info = hit.transform.gameObject.GetComponent(typeof(ISlime_farm_info)) as ISlime_farm_info;
                    if (slime_info.Mature == "adult")
                    {
                        put_slime_out(hit.transform.gameObject.name);
                        slime_max_num--;
                    }
                    else
                    {
                        warning = "This slime isn't big enough to fight!";
                    }
                }
                else
                {
                    warning = "Your party don't have more room!";
                }
            }

            if(hit.transform.gameObject.tag == "Slime")
            {
                if (slime_max_num < 50)
                {
                    put_slime_in(hit.transform.gameObject.name);
                    slime_num++;
                    slime_max_num++;
                }
                else
                {
                    warning = "This farm don't have more room!";
                }
            }
            
        }
    }
    /// <summary>
    /// 放入史萊姆
    /// </summary>
    /// <param name="_slime_name"></param>
    void put_slime_in(string _slime_name)
    {
        ISlime_info slime_info;
        slime_info = GameObject.Find(_slime_name).transform.GetComponent(typeof(ISlime_info)) as ISlime_info;

        player.GetComponent<Player>().slime_partied_num--;
        Instantiate(slime_farm[type_change(slime_info.Type)], new Vector3(this.transform.GetChild(4).transform.position.x, this.transform.GetChild(4).transform.position.y + 5, this.transform.GetChild(4).transform.position.z), slime_farm[type_change(slime_info.Type)].transform.rotation, this.gameObject.transform);
        Destroy(GameObject.Find(_slime_name));
    }
    /// <summary>
    /// 拿出史萊姆
    /// </summary>
    /// <param name="_slime_name"></param>
    void put_slime_out(string _slime_name)
    {
        ISlime_farm_info slime_info;
        slime_info = GameObject.Find(_slime_name).transform.GetComponent(typeof(ISlime_farm_info)) as ISlime_farm_info;

        Instantiate(slime[type_change(slime_info.Type)], new Vector3(this.transform.GetChild(4).transform.position.x, this.transform.GetChild(4).transform.position.y + 5, this.transform.GetChild(4).transform.position.z), slime[type_change(slime_info.Type)].transform.rotation);
        Destroy(GameObject.Find(_slime_name));
        player.GetComponent<Player>().slime_partied_num++;
    }
    public int type_change(string _type)
    {
        switch (_type)
        {
            case "tree":
                return 0;
            case "rock":
                return 1;
            case "fire":
                return 2;
            case "grass":
                return 3;
            case "water":
                return 4;
            case "wind":
                return 5;
            case "light":
                return 6;
            case "dark":
                return 7;
            case "mud":
                return 8;
            case "lava":
                return 9;
        }
        return 0;
    }
}
