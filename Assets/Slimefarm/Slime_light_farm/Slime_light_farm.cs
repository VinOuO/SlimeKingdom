using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_light_farm : Slime_normal_farm
{

    ISlime_farm_info slime_info;


    void Start()
    {
        base.Start();
        type = "light";
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
                if (Random.Range(0, 3) == 0)
                {
                    return 4;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }

            case "fire":
                if (Random.Range(0, 3) == 0)
                {
                    return 4;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }

            case "grass":
                if (Random.Range(0, 3) == 0)
                {
                    return 4;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            case "rock":
                if (Random.Range(0, 4) == 0)
                {
                    return 1;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }

            case "wind":
                if (Random.Range(0, 3) == 0)
                {
                    return 3;
                }
                else if (Random.Range(0, 3) == 1)
                {
                    return 4;
                }
                else
                {
                    return 2;
                }

            case "mud":
                if (Random.Range(0, 4) == 0)
                {
                    return 1;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }

            case "light":
                    return 6;

            case "dark":
                if (Random.Range(0, 4) == 0)
                {
                    return 1;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }

            case "tree":
                if (Random.Range(0, 4) == 0)
                {
                    return 1;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }

            case "lava":
                if (Random.Range(0, 4) == 0)
                {
                    return 1;
                }
                else if (Random.Range(0, 4) == 1)
                {
                    return 2;
                }
                else if (Random.Range(0, 4) == 2)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
        }
        return 6;
    }
}
