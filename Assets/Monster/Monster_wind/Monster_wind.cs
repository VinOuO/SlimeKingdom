using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_wind: Monster_normal
{
    ISlime_info slime_info;
    public GameObject tornado;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (skill_charge >= 5)
        {
            shoot_tornado();
            skill_charge = 0;
        }
    }
    void shoot_tornado()
    {
        Instantiate(tornado, transform.position + new Vector3(0, 0, 0), tornado.transform.rotation);
    }
}
