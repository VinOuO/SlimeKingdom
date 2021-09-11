using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_mud : Monster_normal
{
    ISlime_info slime_info;
    public GameObject mud_ball;

    void Start()
    {
        base.Start();
        type = "mud";
    }

    void Update()
    {
        base.Update();
        if (skill_charge >= 10)
        {
            shoot_mud_ball();
            skill_charge = 0;
        }
    }
    void shoot_mud_ball()
    {
        Instantiate(mud_ball, transform.position + new Vector3(0, 3, 0), mud_ball.transform.rotation);
    }
}
