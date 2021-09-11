using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_water : Monster_normal
{
    ISlime_info slime_info;
    public GameObject water_ball;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        if (skill_charge >= 15)
        {
            shoot_water_ball();
            skill_charge = 0;
        }
    }
    void shoot_water_ball()
    {
        Instantiate(water_ball, transform.position + new Vector3(0, 3, 0), water_ball.transform.rotation);
    }
}
