using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_fire_farm : Slime_normal_farm
{

    ISlime_farm_info slime_info;


    void Start()
    {
        base.Start();
        type = "fire";
    }

    void Update()
    {
        base.Update();
        //每隔一段時間交配一次(女生懷孕)
        //如果懷用後一段時間生小孩
        if (in_love)
        {
            if (!set)
            {
                slime_info = better_half.transform.GetComponent(typeof(ISlime_farm_info)) as ISlime_farm_info;
            }
            pregnant(pregnanted, birth(slime_info.Type));
        }
    }
    int birth(string _type)
    {

        switch (_type)
        {
            case "water":
                if (Random.Range(0, 4) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 4;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 5;
                }
                else
                {
                    return 6;
                }

            case "fire":
                    return 2;

            case "grass":
                if (Random.Range(0, 4) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 3;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 6;
                }
                else
                {
                    return 7;
                }

            case "rock":
                if (Random.Range(0, 3) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 1;
                }
                else
                {
                    return 9;
                }

            case "wind":
                if (Random.Range(0, 2) == 0)
                {
                    return 2;
                }
                else
                {
                    return 4;
                }

            case "mud":
                if (Random.Range(0, 3) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 4;
                }
                else
                {
                    return 1;
                }

            case "light":
                if (Random.Range(0, 2) == 0)
                {
                    return 4;
                }
                else
                {
                    return 2;
                }

            case "dark":
                if (Random.Range(0, 3) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 4;
                }
                else
                {
                    return 1;
                }

            case "tree":
                if (Random.Range(0, 3) == 0)
                {
                    return 2;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 4;
                }
                else
                {
                    return 3;
                }

            case "lava":
                if (Random.Range(0, 2) == 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
        }
        return 2;
    }
}
